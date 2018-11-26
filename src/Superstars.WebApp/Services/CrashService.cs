using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CrashGameMath;
using Microsoft.AspNetCore.SignalR;

namespace Superstars.WebApp.Services
{
    public class CrashService
    {
        private readonly IHubContext<SignalRHub> _signalR;
        private double _crashValue;
        private readonly CrashBuilder _crashBuilder;


        public CrashService(IHubContext<SignalRHub> signalR, CrashBuilder crashBuilder)
        {
            _signalR = signalR;
            _crashValue = crashBuilder.NextCrashValue();
            _crashBuilder = crashBuilder;
            GameLoop();
        }

        private async void LaunchNewGame()
        {
             await _signalR.Clients.All.SendAsync("Newgame", _crashValue);
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

        private async Task GameLoop()
        {
            var stopWatch = new Stopwatch();
            double playTime = PlayTime();
            for (int i = 0; i < 1000; i++)
            {
                playTime = PlayTime();
                LaunchNewGame();
                _crashValue = _crashBuilder.NextCrashValue();
                stopWatch.Start();
                await Task.Delay((int) (playTime * 1000));
                stopWatch.Stop();
                stopWatch.Reset();
            }       
        }
    }
}
