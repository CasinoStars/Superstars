using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System.Threading.Tasks;
using Superstars.WebApp.Models;
using System.Collections.Generic;
using System;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class YamsController : Controller
    {
        readonly YamsGateway _yamsGateway;
        readonly UserGateway _userGateway;

        public YamsController(YamsGateway yamsGateway, UserGateway userGateway)
        {
            _yamsGateway = yamsGateway;
            _userGateway = userGateway;

        }

        [HttpPost("{pseudo}/{myDices}/{selectedDices}")]
        public async Task<IActionResult> RollDices(string pseudo, int[] myDices, int[] selectedDices = null)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            YamsData data = await _yamsGateway.GetPlayer(user.UserId);
            data.NbrRevives = data.NbrRevives + 1;
            int[] secondarray = new int[5];
            string dices = data.Dices;
            for (int i = 0; i < 5; i++)
            {
                secondarray[i] = (int)char.GetNumericValue(dices[i]);
            }
            myDices = secondarray;
            myDices = _yamsGateway.IndexChange(myDices, selectedDices);
            myDices = _yamsGateway.Reroll(myDices);
            string des = null;
            for (int i = 0; i < myDices.Length; i++)
            {
                des += myDices[i];
            }
            Result result = await _yamsGateway.UpdateYamsPlayer(user.UserId, data.YamsGameId, data.NbrRevives, des, data.DicesValue);
            return this.CreateResult(result);
        }

        [HttpGet("{pseudo}")]
        public async Task<IActionResult> GetPlayerDices(string pseudo)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            YamsData data = await _yamsGateway.GetGameId(user.UserId);

            Result<string> result = await _yamsGateway.GetPlayerDices(user.UserId, data.YamsGameId);
            return this.CreateResult(result);
        }

        [HttpPost("{pseudo}")]
        public async Task<IActionResult> CreateYamsPlayer(string pseudo)
        {
            Result result = await _yamsGateway.CreateYamsPlayer(pseudo, 0, "12345", 0);
            return this.CreateResult(result);
        }
    }
}
