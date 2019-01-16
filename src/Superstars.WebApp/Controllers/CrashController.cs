using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
       
        private readonly CrashGateway _crashGateway;

        public CrashController(CrashGateway crashGateway)
        {
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
        public async Task<IEnumerable> GetHashList()
        {
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var list = await _crashGateway.GetTenLastGame(userid);
            return list;
        }

        [HttpGet("{gameId}/CrashMeta")]
        public async Task<CrashMeta> GetCrashMeta(int gameId)
        {
            return await _crashGateway.GetGameCrashMeta(gameId);
        }

    }
}
