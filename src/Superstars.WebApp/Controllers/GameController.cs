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
        readonly YamsGateway _yamsGateway;
        readonly UserGateway _userGateway;
        readonly WalletGateway _walletGateway;
        readonly PasswordHasher _passwordHasher;

        public GameController(GameGateway gameGateway, YamsGateway yamsGateway, UserGateway userGateway, WalletGateway walletGateway, PasswordHasher passwordHasher)
        {
            _gameGateway = gameGateway;
            _yamsGateway = yamsGateway;
            _userGateway = userGateway;
            _walletGateway = walletGateway;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("{gametype}")]
        public async Task<IActionResult> CreateGame(string gametype)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _gameGateway.CreateGame(gametype);
            return this.CreateResult(result);
        }

        [HttpPost("{bet}/bet")]
        public async Task<IActionResult> BetYams(int bet)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _gameGateway.CreateYamsGame(bet * 2);
            Result result2 = await _walletGateway.AddCoins(userId, 2, -(bet));
            Result result3 = await _walletGateway.InsertInBankRoll(0, bet);
            return this.CreateResult(result3);
        }

        [HttpPost("{bet}/betBlackJack")]
        public async Task<IActionResult> BetBlackJack(int bet)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _gameGateway.CreateBlackJackGame(bet * 2);
            Result result2 = await _walletGateway.AddCoins(userId, 2, -(bet));
            Result result3 = await _walletGateway.InsertInBankRoll(0, bet);
            return this.CreateResult(result3);
        }

        [HttpGet("getYamsPot")]
        public async Task<IActionResult> GetYamsPot()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            YamsData yams = await _yamsGateway.GetPlayer(userId);
            Result<int> result = await _gameGateway.GetYamsPot(yams.YamsGameId);
            return this.CreateResult(result);
        }

        [HttpPost("createAiUser")]
        public async Task<IActionResult> CreateAiUser()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _userGateway.CreateUser("AI" + userId, _passwordHasher.HashPassword("azertyuiop" + userId), "", "");
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

        [HttpPost("{gametype}/UpdateStats")]
        public async Task<Result> UpdateStats(string gametype, [FromBody] bool win)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<int> result1 = await _gameGateway.GetWins(userId, gametype);
            Result<int> result2 = await _gameGateway.GetLosses(userId, gametype);
            int wins = result1.Content;
            int losses = result2.Content;
            if (win)
            {
                wins = wins + 1;
            }
            else
            {
                losses = losses + 1;
            }
            Result<int> result3 = await _gameGateway.UpdateStats(userId, gametype, wins, losses);
            return Result.Success(result3);
        }


        [HttpGet("getwinsBlackJackPlayer")]
        public async Task<IActionResult> GetWinsBlackJackPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<int> result = await _gameGateway.GetWins(userId, "BlackJack");
            return this.CreateResult(result);
        }

        [HttpGet("getlossesBlackJackPlayer")]
        public async Task<IActionResult> GetLossesBlackJackPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<int> result = await _gameGateway.GetLosses(userId, "BlackJack");
            return this.CreateResult(result);
        }

        [HttpGet("getwinsYamsPlayer")]
        public async Task<IActionResult> GetWinsYamsPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<int> result = await _gameGateway.GetWins(userId, "Yams");
            return this.CreateResult(result);
        }

        [HttpGet("getlossesYamsPlayer")]
        public async Task<IActionResult> GetLossesYamsPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<int> result = await _gameGateway.GetLosses(userId, "Yams");
            return this.CreateResult(result);
        }

        [HttpGet("gettrueprofitplayer")]
        public async Task<IActionResult> GetTrueProfitPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<int> result = await _gameGateway.GetTrueProfit(userId);
            return this.CreateResult(result);
        }

        [HttpGet("getfakeprofitplayer")]
        public async Task<IActionResult> GetFakeProfitPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<int> result = await _gameGateway.GetFakeProfit(userId);
            return this.CreateResult(result);
        }
    }
}