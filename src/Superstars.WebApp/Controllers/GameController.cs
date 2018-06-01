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
        readonly UserGateway _userGateway;
        readonly PasswordHasher _passwordHasher;

        public GameController(GameGateway gameGateway, UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _gameGateway = gameGateway;
            _userGateway = userGateway;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("{gametype}")]
        public async Task<IActionResult> CreateGame(string gametype)
        {
            Result result = await _gameGateway.CreateGame(gametype);
            Result result2 = await _gameGateway.CreateYamsGame(500);
            return this.CreateResult(result);
        }

        [HttpPost("{pseudo}/createAiUser")]
        public async Task<IActionResult> createAiUser(string pseudo)
        {   
            UserData user = await _userGateway.FindByName(pseudo);
            Result result = await _userGateway.CreateUser("AI" + pseudo, _passwordHasher.HashPassword(user.UserName), "");
            return this.CreateResult(result);
        }

        [HttpDelete("{pseudo}/DeleteAis")]
        public async Task<IActionResult> DeleteAI(string pseudo) 
        {
             Result result = await _gameGateway.DeleteAis(pseudo);
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