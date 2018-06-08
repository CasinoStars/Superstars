using System.Threading.Tasks;
using Superstars.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Superstars.WebApp.Authentication;
using System.Security.Claims;

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

        [HttpPost("createAiUser")]
        public async Task<IActionResult> CreateAiUser()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _userGateway.CreateUser("AI" + userId, _passwordHasher.HashPassword("azertyuiop"+userId), "","");
            return this.CreateResult(result);
        }

        [HttpDelete("DeleteAis")]
        public async Task<IActionResult> DeleteAI()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _gameGateway.DeleteAis(userId);
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