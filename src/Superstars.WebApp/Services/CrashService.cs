﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrashGameMath;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using Superstars.DAL;

namespace Superstars.WebApp.Services
{
    public class CrashService : IHostedService
    {
        private readonly IHubContext<SignalRHub> _signalR;
        private double _crashValue;
        private CrashBuilder _crashBuilder;
        private readonly RankGateway _rankGateway;
        private readonly GameGateway _gameGateway;
        private readonly CrashGateway _crashGateway;
        private readonly WalletGateway _walletGateway;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _runningTask;
        private CancellationToken RunToken => _cancellationTokenSource.Token;
        public CrashService(IHubContext<SignalRHub> signalR, RankGateway rankGateway, GameGateway gameGateway, CrashGateway crashGateway, WalletGateway walletGateway)
        {
            _signalR = signalR;
            _crashBuilder = new CrashBuilder(1000, "0000000000000000004d6ec16dafe9d8370958664c1dc422f452892264c59526" + new Random().Next(int.MaxValue));
            _crashValue = _crashBuilder.NextCrashValue();
            _gameGateway = gameGateway;
            _crashGateway = crashGateway;
            _walletGateway = walletGateway;
            _rankGateway = rankGateway;
        }

        private async Task LaunchNewGame(int gameId)
        {
            await _gameGateway.ActionStartGameCrash(DateTime.UtcNow, gameId);
            await _signalR.Clients.All.SendAsync("NewGame");
        }
        private async Task LaunchStep(double step, double i)
        {
            await _signalR.Clients.All.SendAsync("Step", step, i);
        }

        private async Task LaunchEndGame(int gameId)
        {
            await _gameGateway.ActionEndGameCrash(DateTime.UtcNow, gameId, _crashValue);
            await _signalR.Clients.All.SendAsync("EndGame", gameId, CrashBuilder.ActualHashString, _crashValue);
            await Task.Delay(2000);
        }

        private async Task LaunchPause()
        {
            await _signalR.Clients.All.SendAsync("Wait");
        }

        private async Task PlayTime()
        {
            double multi = 1;
            double i = 0;
            while (multi < _crashValue)
            {

                multi = Math.Exp(i / 100);
                multi = Math.Round(multi * 100) / 100;
                i++;
                await LaunchStep(multi, i);
                await Task.Delay(100);
            }
            await LaunchStep(_crashValue, i);
        }

        private async Task WaitingForBets()
        {
            await LaunchPause();
            await Task.Delay(5000);

        }
        public async Task<TimeSpan> GetAverageTime(int userId, int gameTypeId)
        {
            var result = await _rankGateway.GetGames(userId, gameTypeId);
            var games = result.ToList();

            List<TimeSpan> averageTimes = new List<TimeSpan>();

            foreach (var item in games)
            {
                averageTimes.Add(item.EndDate - item.StartDate);
            }

            if (averageTimes.Count == 0)
            {
                return new TimeSpan();
            }
            var k = averageTimes.Average(TimeSpan => TimeSpan.TotalMilliseconds);
            TimeSpan avg = TimeSpan.FromMilliseconds(k);
            return avg;
        }

        private async Task SetWins(int gameId)
        {
            var players = await GetPlayersInGame();
            foreach (var player in players)
            {
                var avgTime = await GetAverageTime(player.UserId, 2);
                if (!(player.Multi <= _crashValue))
                {
                    await _gameGateway.UpdateStats(player.UserId, 2, player.MoneyTypeId, 0, 1, 0, -player.Bet, player.Bet,
                        avgTime.Milliseconds);
                    continue;
                }
                var potDouble = player.Multi * player.Bet;
                var pot = (int)potDouble;

                await _gameGateway.UpdateStats(player.UserId, 2, player.MoneyTypeId, 1, 0, 0, player.Bet, player.Bet,
                    avgTime.Milliseconds);
                await _gameGateway.ActionPlayerWinCrash(player.UserId, DateTime.UtcNow, gameId, player.Multi, player.Bet,
                    player.MoneyTypeId, pot);
                if (player.MoneyTypeId == 0)
                    await _walletGateway.AddCoins(player.UserId, 0, pot, 0);
                else
                    await _walletGateway.AddCoins(player.UserId, 1, 0, pot);
            }
        }


        private async Task Process(CancellationToken cancellationToken)
        {
            while (true)
            {
                for (var i = 0; i < 999; i++)
                {
                    await GameLoop(cancellationToken);
                    if (RunToken.IsCancellationRequested) return;
                }
                _crashBuilder = new CrashBuilder(1000, "0000000000000000004d6ec16dafe9d8370958664c1dc422f452892264c59526" + new Random().Next(int.MaxValue));
                if (RunToken.IsCancellationRequested) return;
            }
        }

        private async Task GameLoop(CancellationToken cancellationToken)
        {
            var gameId = await _gameGateway.CreateGame(2);
            await _crashGateway.CreateCrashGame("", 0);
            await WaitingForBets();
            await Task.WhenAll(LaunchNewGame(gameId.Content), PlayTime());
            await LaunchEndGame(gameId.Content);
            await _gameGateway.UpdateGameEnd(gameId.Content, 2, "");
            await _crashGateway.UpdateCrashGame(CrashBuilder.ActualHashString, CrashBuilder.ActualHashValue);
            await SetWins(gameId.Content);
            _crashValue = _crashBuilder.NextCrashValue();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _runningTask = Process(cancellationToken);
            _cancellationTokenSource = new CancellationTokenSource();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            return _runningTask;
        }

        public async Task<List<CrashData>> GetPlayersInGame()
        {
            var players = (List<CrashData>)await _crashGateway.GetGamePlayers();
            return players;
        }
    }
}
