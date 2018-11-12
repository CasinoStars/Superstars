using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Superstars.WebApp
{
    public class SignalRHub : Hub
    {
        public async Task SendMessage()
        {
            if (Clients != null) await Clients.All.SendAsync("Send", "loltest");
        }
    }
}
