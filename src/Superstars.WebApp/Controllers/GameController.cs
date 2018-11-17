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

        [HttpPost("{gameTypeId}")]
        public async Task<IActionResult> CreateGame(int gameTypeId)
        {
            Result result = await _gameGateway.CreateGame(gameTypeId);
            return this.CreateResult(result);
        }

        [HttpPost("{bet}/{gameTypeId}/betBTC")]
        public async Task<IActionResult> BetBTC(int bet, int gameTypeId) // gameTypeId: 0=>Yams - 1=>BlackJack
        {
            var stringBet = Convert.ToString(bet * 2);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (gameTypeId == 0)
            {
                Result result = await _gameGateway.CreateYamsGame(stringBet);
                var data = await _yamsGateway.GetPlayer(userId);
                await _gameGateway.ActionStartGameBTC(user.UserId, user.UserName, DateTime.UtcNow, gameType, data.YamsGameId);
            }
            else
            {
                Result result = await _gameGateway.CreateBlackJackGame(stringBet);
                var data = await _blackJackGateWay.GetPlayer(userId);
                await _gameGateway.ActionStartGameBTC(user.UserId, user.UserName, DateTime.UtcNow, gameType, data.BlackJackGameId);
            }

            Result result2 = await _walletGateway.AddCoins(userId, 1, 0, -bet, -bet);
            var result3 = await _walletGateway.InsertInBankRoll(bet, 0); //insert in true coin bet
            return this.CreateResult(result2);
        }

        [HttpPost("{bet}/{gameTypeId}/betFake")]
        public async Task<IActionResult> FakeBet(int bet, int gameTypeId) // gameTypeId: 0=>Yams - 1=>BlackJack
        {
            var stringBet = Convert.ToString(bet * 2);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (gameTypeId == 0)
            {
                Result result = await _gameGateway.CreateYamsGame(stringBet);
                var data = await _yamsGateway.GetPlayer(userId);
                await _gameGateway.ActionStartGameFake(user.UserId, user.UserName, DateTime.UtcNow, gameType, data.YamsGameId);
            }
            else
            {
                Result result = await _gameGateway.CreateBlackJackGame(stringBet);
                var data = await _blackJackGateWay.GetPlayer(userId);
                await _gameGateway.ActionStartGameFake(user.UserId, user.UserName, DateTime.UtcNow, gameType, data.BlackJackGameId);
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

        [HttpPost("{gameTypeId}/UpdateStats")]
        public async Task<Result> UpdateStats(int gameTypeId, [FromBody] bool win)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result1 = await _gameGateway.GetWins(userId, gameTypeId);
            var result2 = await _gameGateway.GetLosses(userId, gameTypeId);
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
            var result3 = await _gameGateway.UpdateStats(userId, gameTypeId, wins, losses);
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
            var result = await _gameGateway.GetWins(userId, 1);
            return this.CreateResult(result);
        }

        [HttpGet("getlossesBlackJackPlayer")]
        public async Task<IActionResult> GetLossesBlackJackPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _gameGateway.GetLosses(userId, 1);
            return this.CreateResult(result);
        }

        [HttpGet("getwinsYamsPlayer")]
        public async Task<IActionResult> GetWinsYamsPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _gameGateway.GetWins(userId, 0);
            return this.CreateResult(result);
        }

        [HttpGet("getlossesYamsPlayer")]
        public async Task<IActionResult> GetLossesYamsPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _gameGateway.GetLosses(userId, 0);
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


        //[HttpDelete("cancelBlackJack")]
        //public async Task<Result> DeleteBlackJackPlayer()
        //{
        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    int gameId = await _gameGateway.GetGameIdToDeleteByPlayerId(userId, 1);
        //    return await _blackJackGateway.DeleteBlackJackPlayer(gameId);
        //}

        [HttpDelete("deleteGame/{gametype}")]
        public async Task<Result> deleteGame(int gametype)
        {
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _gameGateway.DeleteGameByPlayerId(userid, gametype);                   
        }

        [HttpDelete("deleteYamsGame")]
        public async Task<Result> deleteYamsGame()
        {
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int gameId = await _gameGateway.GetGameIdToDeleteByPlayerId(userid, 0);
            return await _gameGateway.DeleteYamsGameByGameId(gameId);           
        }

        [HttpDelete("deleteBlackJackGame")]
        public async Task<Result> deleteBlackJackGame()
        {
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int gameId = await _gameGateway.GetGameIdToDeleteByPlayerId(userid, 1);
            return await _gameGateway.DeleteBlackJackGameByGameId(gameId);         
        }

        [HttpGet("isInGame/{gametype}")]
        public async Task<IActionResult> isInGame(int gametype)
        {
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GameData data = await _gameGateway.GetGameByPlayerId(userid,gametype);

            if (data == null) return this.Ok(false);
            //var result = await _gameGateway.IsGameEndDefined(data.GameId, gametype);
            if (data.EndDate == DateTime.MinValue)
                return Ok(true);
            else
                return Ok(false);
        }

        [HttpPost("GameEndUpdate/{gametype}/{win}")]
        public async Task<IActionResult> GameEndUpdate(int gametype, string win)
        {
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GameData data = await _gameGateway.GetGameByPlayerId(userid,gametype);
            UserData udata = await _userGateway.FindById(userid);
            Result result;
            if (win == "Player")
            {
                result = await _gameGateway.UpdateGameEnd(data.GameId, gametype, udata.UserName);
            }
            else if (win == "Draw")
            {
                result = await _gameGateway.UpdateGameEnd(data.GameId, gametype, "Draw");

            } else
            {
                var IA = await _userGateway.FindByName("#AI" + userid);
                result = await _gameGateway.UpdateGameEnd(data.GameId, gametype, "#AI" + userid);
            }

            return this.CreateResult(result);
        }

    }
}