using System.Threading.Tasks;
using Superstars.DAL;
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
        readonly GameGateway _gameGateway;

        public GameController(GameGateway gameGateway)
        {
            _gameGateway = gameGateway;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame(GameViewModel model)
        {
            Result result = await _gameGateway.CreateGame(model.GameType);
            return this.CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateYamsGame(YamsViewModel model)
        {
            Result result = await _gameGateway.CreateYamsGame(model.Pot);
            return this.CreateResult(result);
        }

        [HttpGet]
        public async Task<Result<GameData>> FindGameById(int GameID)
        {
            GameData game = await _gameGateway.FindGameById(GameID);
            if (game == null) return Result.Failure<GameData>(Status.NotFound, "Game not found.");
            return Result.Success(game);
        }
    }
}