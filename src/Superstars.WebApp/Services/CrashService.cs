using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrashGameMath;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using NBitcoin;
using Superstars.DAL;
using Superstars.WebApp.Controllers;

namespace Superstars.WebApp.Services
{
    public class CrashService : IHostedService, IDisposable
    {
        private readonly IHubContext<SignalRHub> _signalR;
        private double _crashValue;
        private readonly CrashBuilder _crashBuilder;
        private readonly RankGateway _rankGateway;
        private readonly GameGateway _gameGateway;
        private readonly CrashGateway _crashGateway;
        private readonly WalletGateway _walletGateway;

        public CrashService(IHubContext<SignalRHub> signalR, RankGateway rankGateway, CrashBuilder crashBuilder, GameGateway gameGateway, CrashGateway crashGateway, WalletGateway walletGateway)
        {
            _signalR = signalR;
            _crashValue = crashBuilder.NextCrashValue();
            _crashBuilder = crashBuilder;
            _gameGateway = gameGateway;
            _crashGateway = crashGateway;
            _walletGateway = walletGateway;
            _rankGateway = rankGateway;
        }

        private async Task LaunchNewGame()
        {
             await _signalR.Clients.All.SendAsync("NewGame");
        }
        private async Task LaunchStep(double step, double i)
        {
            await _signalR.Clients.All.SendAsync("Step", step, i);
        }

        private async Task LaunchEndGame()
        {
            await _signalR.Clients.All.SendAsync("EndGame", _crashValue);
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
                
                multi = Math.Exp(i/100);
                multi = Math.Round(multi * 100) / 100;
                i++;
                await LaunchStep(multi, i);
                await Task.Delay(100);
            }
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

        private async Task SetWins()
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
                var pot = (int) potDouble;

                await _gameGateway.UpdateStats(player.UserId, 2, player.MoneyTypeId, 1, 0, 0, -player.Bet, player.Bet,
                    avgTime.Milliseconds);

                if (player.MoneyTypeId == 0)
                    await _walletGateway.AddCoins(player.UserId, 0, pot, pot / 2);
                else
                    await _walletGateway.AddCoins(player.UserId, 1, 0, pot / 2);
            }
        }


        private async Task GameLoop()
        {
            for (var i = 0; i < 1000; i++)
            {
                var gameId = await _gameGateway.CreateGame(2);
                await WaitingForBets();
                await Task.WhenAll(LaunchNewGame(), PlayTime());
                await LaunchEndGame();
                await _gameGateway.UpdateGameEnd(gameId.Content, 2, "");
                await SetWins();
                _crashValue = _crashBuilder.NextCrashValue();
            }       
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            new Task(async()=>await GameLoop()).Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<List<CrashData>> GetPlayersInGame()
        {
            var players = (List<CrashData>)await _crashGateway.GetGamePlayers();
            return players;
        }

        public void Dispose()
        {
            GameLoop().Dispose();
        }
    }
}
