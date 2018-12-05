using System;
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
    //[Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class CrashController : Controller
    {
        private readonly CrashBuilder _crash;
        private readonly CrashService _crashService;

        public CrashController(CrashBuilder crash)
        {
            _crash = crash;
            
        }
        [HttpGet("getNextCrash")]
        public async Task<IActionResult> GetNextCrash()
        {
             return Ok();
        }

        [HttpGet("GetPlayersInGame")]
        public async Task<IEnumerable<CrashData>> GetPlayersInGame()
        {
            return await _crashService.GetPlayersInGame();
        }
    }
}
