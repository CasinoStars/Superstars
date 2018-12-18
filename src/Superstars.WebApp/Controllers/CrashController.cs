using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrashGameMath;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Services;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class CrashController : Controller
    {
        private readonly CrashBuilder _crash;
        private readonly CrashGateway _crashGateway;

        public CrashController(CrashBuilder crash, CrashGateway crashGateway)
        {
            _crash = crash;
            _crashGateway = crashGateway;
        }


        [HttpGet("GetPlayersInGame")]
        public async Task<IEnumerable<CrashData>> GetPlayersInGame()
        {
            return (List<CrashData>)await _crashGateway.GetGamePlayers();
        }



        [HttpGet("{gameId}/GetPlayersInGame")]
        public async Task<IEnumerable<CrashData>> GetPlayersInGame(int gameId)
        {
            return (List<CrashData>) await _crashGateway.GetGamePlayers(gameId);
        }
        [HttpGet("{gameId}/{userId}/GetPlayerByGame")]
        public async Task<CrashData> GetPlayerByGame(int gameId, int userId)
        {
            return await _crashGateway.GetPlayerByGame(userId,gameId);
        }
        [HttpGet("HashList")]
        public IEnumerable GetHashList()
        {
            return _crash.CreateHashList(10, CrashBuilder.ActualHashString);
        }


    }
}
