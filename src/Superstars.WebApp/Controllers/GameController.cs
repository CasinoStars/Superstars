using System.Threading.Tasks;
using Superstars.DAL;
using Superstars.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Superstars.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Superstars.WebApp.Authentication;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class GameController : Controller
    {
        readonly GameService _gameservice;
        //readonly GameGateway _gameGateway;

        public GameController(GameService gameservice)
        {
            _gameservice = gameservice;
           //_gameGateway = gameGateway;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame(GameViewModel model)
        {
            Result result = await _gameservice.CreateGame(model.GameType);
            //GameData game = await _gameservice.FindGameById(model.GameID);
            //return View(model);
            return this.CreateResult(result);
        }
    }
}