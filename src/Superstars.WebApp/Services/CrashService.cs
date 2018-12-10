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

namespace Superstars.WebApp.Services
{
    public class CrashService : IHostedService
    {
        private readonly IHubContext<SignalRHub> _signalR;
        private double _crashValue;
        private readonly CrashBuilder _crashBuilder;
        private readonly GameGateway _gameGateway;
        private readonly CrashGateway _crashGateway;
        private readonly WalletGateway _walletGateway;

        public CrashService(IHubContext<SignalRHub> signalR, CrashBuilder crashBuilder, GameGateway gameGateway, CrashGateway crashGateway, WalletGateway walletGateway)
        {
            _signalR = signalR;
            _crashValue = crashBuilder.NextCrashValue();
            _crashBuilder = crashBuilder;
            _gameGateway = gameGateway;
            _crashGateway = crashGateway;
            _walletGateway = walletGateway;
        }

        private async Task LaunchNewGame()
        {
             await _signalR.Clients.All.SendAsync("Newgame", _crashValue);
        }

        private async Task LaunchPause()
        {
            await _signalR.Clients.All.SendAsync("Wait");
        }

        private double PlayTime()
        {
            var i = 0;
            while (Math.Exp(i / 100) <= _crashValue)
            {
                i++;
            }

            return i /10 + 1;
        }

        private async Task WaitingForBets()
        {
            await LaunchPause();
            await Task.Delay(5000);

        }

        private async Task SetWins()
        {
            var players = await GetPlayersInGame();
            foreach (var player in players)
            {
                if (!(player.Multi <= _crashValue)) continue;
                var potDouble = player.Multi * player.Bet;
                var pot = (int) potDouble;
                                                                
                await _walletGateway.AddCoins(player.UserId, 0, pot, pot/2, 0);
            }
        }


        private async Task GameLoop()
        {
            var stopWatch = new Stopwatch();
            double playTime = PlayTime();
            for (int i = 0; i < 1000; i++)
            {
                var gameId = await _gameGateway.CreateGame(2);
                await WaitingForBets();
                playTime = PlayTime();
                await LaunchNewGame();
                stopWatch.Start();
                await Task.Delay((int) (playTime * 1000));
                stopWatch.Stop();
                stopWatch.Reset();
                await _gameGateway.UpdateGameEnd(gameId.Content, 2, "");
                await SetWins();
                _crashValue = _crashBuilder.NextCrashValue();
            }       
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await GameLoop();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            
        }

        public async Task<List<CrashData>> GetPlayersInGame()
        {
            List<CrashData> players = (List<CrashData>)await _crashGateway.GetGamePlayers();
            return players;
        }
    }
}
