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
        readonly BlackJackGateway _blackJackGateWay;
        readonly UserGateway _userGateway;
        readonly WalletGateway _walletGateway;
        readonly PasswordHasher _passwordHasher;

        public GameController(GameGateway gameGateway, YamsGateway yamsGateway, BlackJackGateway blackJackGateway, UserGateway userGateway, WalletGateway walletGateway, PasswordHasher passwordHasher)
        {
            _gameGateway = gameGateway;
            _yamsGateway = yamsGateway;
            _blackJackGateWay = blackJackGateway;
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

        [HttpPost("{bet}/betBTC")]
        public async Task<IActionResult> BetBTC(decimal bet, string gameType) // gameType = 'Yams' or 'BlackJack'
        {
            string stringBet = System.Convert.ToString(bet*2);
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (gameType == "Yams") {
                Result result = await _gameGateway.CreateYamsGame(stringBet);
            }
            else {
                Result result = await _gameGateway.CreateBlackJackGame(stringBet);
            }
            Result result2 = await _walletGateway.AddCoins(userId, 1, 0, -(bet));
            Result result3 = await _walletGateway.InsertInBankRoll(bet, 0); //insert in true coin bet
            return this.CreateResult(result2);
        }

        [HttpPost("{bet}/betFake")]
        public async Task<IActionResult> FakeBet(int bet, string gameType) // gameType = 'Yams' or 'BlackJack'
        {
            string stringBet = System.Convert.ToString(bet * 2);
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (gameType == "Yams")
            {
                Result result = await _gameGateway.CreateYamsGame(stringBet);
            }
            else
            {
                Result result = await _gameGateway.CreateBlackJackGame(stringBet);
            }
            Result result2 = await _walletGateway.AddCoins(userId, 2, -(bet));
            Result result3 = await _walletGateway.InsertInBankRoll(0, bet);
            return this.CreateResult(result3);
        }

        [HttpGet("getYamsPot")]
        public async Task<decimal> GetYamsPot()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            YamsData data = await _yamsGateway.GetPlayer(userId);
            Result<string> result = await _gameGateway.GetYamsPot(data.YamsGameId);
            return System.Convert.ToDecimal(result.Content);
        }

        [HttpGet("getBlackJackPot")]
        public async Task<decimal> GetBlackJackPot()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BlackJackData data = await _blackJackGateWay.GetPlayer(userId);
            Result<string> result = await _gameGateway.GetYamsPot(data.BlackJackGameId);
            return System.Convert.ToDecimal(result.Content);
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