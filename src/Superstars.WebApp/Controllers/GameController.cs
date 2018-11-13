using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")] 
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class GameController : Controller
    {
        private readonly BlackJackGateway _blackJackGateWay;
        private readonly GameGateway _gameGateway;
        private readonly PasswordHasher _passwordHasher;
        private readonly RankGateway _rankGateway;
        private readonly UserGateway _userGateway;
        private readonly WalletGateway _walletGateway;
        private readonly YamsGateway _yamsGateway;

        public GameController(GameGateway gameGateway, YamsGateway yamsGateway, BlackJackGateway blackJackGateway,
            UserGateway userGateway, WalletGateway walletGateway, PasswordHasher passwordHasher,
            RankGateway rankGateway)
        {
            _gameGateway = gameGateway;
            _yamsGateway = yamsGateway;
            _blackJackGateWay = blackJackGateway;
            _userGateway = userGateway;
            _walletGateway = walletGateway;
            _passwordHasher = passwordHasher;
            _rankGateway = rankGateway;
        }

        [HttpPost("{gametype}")]
        public async Task<IActionResult> CreateGame(string gametype)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _gameGateway.CreateGame(userId, gametype);
            return this.CreateResult(result);
        }

        [HttpPost("{bet}/{gameType}/betBTC")]
        public async Task<IActionResult> BetBTC(int bet, string gameType) // gameType = 'Yams' or 'BlackJack'
        {
            var stringBet = Convert.ToString(bet * 2);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (gameType == "Yams")
            {
                Result result = await _gameGateway.CreateYamsGame(stringBet);
            }
            else
            {
                Result result = await _gameGateway.CreateBlackJackGame(stringBet);
            }

            Result result2 = await _walletGateway.AddCoins(userId, 1, 0, -bet, -bet);
            var result3 = await _walletGateway.InsertInBankRoll(bet, 0); //insert in true coin bet
            return this.CreateResult(result2);
        }

        [HttpPost("{bet}/{gameType}/betFake")]
        public async Task<IActionResult> FakeBet(int bet, string gameType) // gameType = 'Yams' or 'BlackJack'
        {
            var stringBet = Convert.ToString(bet * 2);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (gameType == "Yams")
            {
                Result result = await _gameGateway.CreateYamsGame(stringBet);
            }
            else
            {
                Result result = await _gameGateway.CreateBlackJackGame(stringBet);
            }

            Result result2 = await _walletGateway.AddCoins(userId, 0, -bet, -bet, 0);
            var result3 = await _walletGateway.InsertInBankRoll(0, bet);
            return this.CreateResult(result3);
        }

        [HttpGet("getYamsPot")]
        public async Task<decimal> GetYamsPot()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _yamsGateway.GetPlayer(userId);
            var result = await _gameGateway.GetYamsPot(data.YamsGameId);
            return Convert.ToDecimal(result.Content);
        }

        [HttpGet("getBlackJackPot")]
        public async Task<decimal> GetBlackJackPot()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _blackJackGateWay.GetPlayer(userId);
            var result = await _gameGateway.GetBlackJackPot(data.BlackJackGameId);
            return Convert.ToDecimal(result.Content);
        }

        [HttpPost("createAiUser")]
        public async Task<IActionResult> CreateAiUser()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _userGateway.CreateUser("#AI" + userId,
                _passwordHasher.HashPassword("azertyuiop" + userId), "", "");
            return this.CreateResult(result);
        }

        [HttpDelete("DeleteAis")]
        public async Task<IActionResult> DeleteAI()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _gameGateway.DeleteAis(userId);
            return this.CreateResult(result);
        }

        [HttpGet]
        public async Task<Result<GameData>> FindGameById(int GameID)
        {
            var game = await _gameGateway.FindGameById(GameID);
            if (game == null) return Result.Failure<GameData>(Status.NotFound, "Game not found.");
            return Result.Success(game);
        }

        [HttpPost("{gametype}/UpdateStats")]
        public async Task<Result> UpdateStats(string gametype, [FromBody] bool win)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result1 = await _gameGateway.GetWins(userId, gametype);
            var result2 = await _gameGateway.GetLosses(userId, gametype);
            //int averagebet = 0;

            //WIP
            /*if (gametype == "Yams")
            {
                averagebet = await GetAverageBetYams();
            }
            else if (gametype == "BlackJack")
            {
                averagebet = await GetAverageBetBJ();
            }*/

            var wins = result1.Content;
            var losses = result2.Content;
            if (win)
                wins = wins + 1;
            else
                losses = losses + 1;
            var result3 = await _gameGateway.UpdateStats(userId, gametype, wins, losses);
            return Result.Success(result3);
        }

        public async Task<int> GetAverageBetYams()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var betsYams = await _rankGateway.GetPlayerYamsBets(userId);
            var allbetsyams = betsYams.ToList();

            var sommeYams = 0;
            foreach (var item in allbetsyams) sommeYams += item;
            sommeYams = sommeYams / allbetsyams.Count();

            return sommeYams;
        }

        public async Task<int> GetAverageBetBJ()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var betsBJ = await _rankGateway.GetPlayerBJBets(userId);
            var allbetsBJ = betsBJ.ToList();

            var sommeBJ = 0;
            foreach (var item in allbetsBJ) sommeBJ += item;
            sommeBJ = sommeBJ / allbetsBJ.Count();

            return sommeBJ;
        }

        [HttpGet("getwinsBlackJackPlayer")]
        public async Task<IActionResult> GetWinsBlackJackPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _gameGateway.GetWins(userId, "BlackJack");
            return this.CreateResult(result);
        }

        [HttpGet("getlossesBlackJackPlayer")]
        public async Task<IActionResult> GetLossesBlackJackPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _gameGateway.GetLosses(userId, "BlackJack");
            return this.CreateResult(result);
        }

        [HttpGet("getwinsYamsPlayer")]
        public async Task<IActionResult> GetWinsYamsPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _gameGateway.GetWins(userId, "Yams");
            return this.CreateResult(result);
        }

        [HttpGet("getlossesYamsPlayer")]
        public async Task<IActionResult> GetLossesYamsPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _gameGateway.GetLosses(userId, "Yams");
            return this.CreateResult(result);
        }

        [HttpGet("gettrueprofitplayer")]
        public async Task<IActionResult> GetTrueProfitPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _gameGateway.GetTrueProfit(userId);
            return this.CreateResult(result);
        }

        [HttpGet("getfakeprofitplayer")]
        public async Task<IActionResult> GetFakeProfitPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _gameGateway.GetFakeProfit(userId);
            return this.CreateResult(result);
        }

    }
}