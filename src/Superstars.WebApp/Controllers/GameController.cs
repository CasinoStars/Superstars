using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NBitcoin;
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
        private readonly CrashGateway _crashGateway;
        private readonly IHubContext<SignalRHub> _hubContext;


        public GameController(GameGateway gameGateway, YamsGateway yamsGateway, BlackJackGateway blackJackGateway,
            UserGateway userGateway, WalletGateway walletGateway, PasswordHasher passwordHasher,
            RankGateway rankGateway, CrashGateway crashGateway, IHubContext<SignalRHub> hubContext)
        {
            _gameGateway = gameGateway;
            _yamsGateway = yamsGateway;
            _blackJackGateWay = blackJackGateway;
            _userGateway = userGateway;
            _walletGateway = walletGateway;
            _passwordHasher = passwordHasher;
            _rankGateway = rankGateway;
            _crashGateway = crashGateway;
            _hubContext = hubContext;
        }

        [HttpPost("{gameTypeId}")]
        public async Task<IActionResult> CreateGame(int gameTypeId)
        {
            Result result = await _gameGateway.CreateGame(gameTypeId);
            if (gameTypeId == 0)
            {
                Result yamsPot = await _gameGateway.CreateYamsGame("0");
            }
            else if (gameTypeId == 1)
            {
                Result blackJackpot = await _gameGateway.CreateBlackJackGame("0");
            }
            return this.CreateResult(result);
        }

        [HttpPost("{bet}/{gameTypeId}/betBTC")]
        public async Task<IActionResult> BetBTC(int bet, int gameTypeId) // gameTypeId: 0=>Yams - 1=>BlackJack
        {
            var stringBet = Convert.ToString(bet * 2);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindById(userId);
            if (gameTypeId == 0)
            {
                var data = await _yamsGateway.GetPlayer(userId);
                Result result = await _gameGateway.UpdateYamsGame(stringBet, data.YamsGameId);
                await _gameGateway.ActionStartGameBTC(user.UserId, user.UserName, DateTime.UtcNow, gameTypeId, data.YamsGameId);
            }
            else
            {
                var data = await _blackJackGateWay.GetPlayer(userId);
                Result result = await _gameGateway.UpdateBlackJackGame(stringBet, data.BlackJackGameId);
                await _gameGateway.ActionStartGameBTC(user.UserId, user.UserName, DateTime.UtcNow, gameTypeId, data.BlackJackGameId);
            }

            Result result2 = await _walletGateway.AddCoins(userId, 1, 0, -bet);
            var result3 = await _walletGateway.InsertInBankRoll(bet, 0); //insert in true coin bet
            return this.CreateResult(result2);
        }

        [HttpPost("{bet}/{gameTypeId}/betFake")]
        public async Task<IActionResult> FakeBet(int bet, int gameTypeId) // gameTypeId: 0=>Yams - 1=>BlackJack
        {
            var stringBet = Convert.ToString(bet * 2);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindById(userId);
            if (gameTypeId == 0)
            {
                var data = await _yamsGateway.GetPlayer(userId);
                Result result = await _gameGateway.UpdateYamsGame(stringBet, data.YamsGameId);
                await _gameGateway.ActionStartGameFake(user.UserId, user.UserName, DateTime.UtcNow, gameTypeId, data.YamsGameId);
            }
            else
            {
                var data = await _blackJackGateWay.GetPlayer(userId);
                Result result = await _gameGateway.UpdateBlackJackGame(stringBet,data.BlackJackGameId);
                await _gameGateway.ActionStartGameFake(user.UserId, user.UserName, DateTime.UtcNow, gameTypeId, data.BlackJackGameId);
            }

            Result result2 = await _walletGateway.AddCoins(userId, 0, -bet, -bet);
            var result3 = await _walletGateway.InsertInBankRoll(0, bet);
            return this.CreateResult(result3);
        }

        [HttpPost("{bet}/{crash}/{moneyTypeId}/betCrash")]
        public async Task<IActionResult> BetCrash(int bet, float crash, int moneyTypeId ) 
        {
            var stringBet = Convert.ToString(bet * 2);
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindById(userId);

            await _crashGateway.CreateCrashPlayer(userId, bet, crash, moneyTypeId);
            var data = new CrashData {UserName = user.UserName, Bet = bet, Multi = crash};
            await _hubContext.Clients.All.SendAsync("Bet", data);

            if (moneyTypeId == 0)
            {
                Result result2 = await _walletGateway.AddCoins(userId, 0, -bet, -bet, 0);
                var result3 = await _walletGateway.InsertInBankRoll(0, bet);
                return this.CreateResult(result3);
            }
            else
            {
                Result result2 = await _walletGateway.AddCoins(userId, 1, 0, -bet, -bet);
                var result3 = await _walletGateway.InsertInBankRoll(bet, 0); //insert in true coin bet
                return this.CreateResult(result2);
            }
            
        }

        [HttpPost("{crash}/updateCrash")]
        public async Task<IActionResult> UpdateCrash(float crash)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindById(userId);

            var result = await _crashGateway.UpdateCrashPlayer(userId, crash);
            var data = new CrashData { UserName = user.UserName, Multi = crash };
            await _hubContext.Clients.All.SendAsync("Update", data);

            return this.CreateResult(result);

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
        public async Task<IActionResult> CreateAiUser([FromBody]int gametypeid)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var privateKey = new Key().GetBitcoinSecret(Network.TestNet);
            var privateKeyString = privateKey.ToString();
            var result = await _userGateway.CreateUser("#AI" + userId + gametypeid.ToString(),
                _passwordHasher.HashPassword("azertyuiop" + userId), "", privateKeyString);
            return this.CreateResult(result);
        }

        [HttpDelete("{gametypeid}/DeleteAis")]
        public async Task<IActionResult> DeleteAI(int gametypeid)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _gameGateway.DeleteAis(userId, gametypeid);
            return this.CreateResult(result);
        }

        [HttpGet]
        public async Task<Result<GameData>> FindGameById(int GameID)
        {
            var game = await _gameGateway.FindGameById(GameID);
            if (game == null) return Result.Failure<GameData>(Status.NotFound, "Game not found.");
            return Result.Success(game);
        }

        [HttpPost("{gameTypeId}/{moneyTypeId}/{bet}/UpdateStats")]
        public async Task<Result> UpdateStats(int gameTypeId, int moneyTypeId, int bet, [FromBody] string win)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = null;
            var avgTime = await GetAverageTime(userId, gameTypeId);
           
            if (win == "Player")
            {
                result = await _gameGateway.UpdateStats(userId, gameTypeId, moneyTypeId, 1, 0, 0, bet, bet, avgTime.Milliseconds);
            }
            else if(win == "AI")
            {
                result = await _gameGateway.UpdateStats(userId, gameTypeId, moneyTypeId, 0, 1, 0, -bet, bet, avgTime.Milliseconds);
            }
            else if(win == "Equality")
            {
                result = await _gameGateway.UpdateStats(userId, gameTypeId, moneyTypeId, 0, 0, 1, 0, bet, avgTime.Milliseconds);
            }
            return Result.Success(result);
        }

        public async Task<TimeSpan> GetAverageTime(int userId, int gameTypeId)
        {
            var result = await _rankGateway.GetGames(userId, gameTypeId);
            var games = result.ToList();

            List<TimeSpan> averageTimes = new List<TimeSpan>();

            foreach (var item in games)
            {
                averageTimes.Add(item.EndDate - item.StartDate);
            }

            if (averageTimes.Count == 0)
            {
                return new TimeSpan();
            }
            var k = averageTimes.Average(TimeSpan => TimeSpan.TotalMilliseconds);
            TimeSpan avg = TimeSpan.FromMilliseconds(k);
            return avg;
        }

        public async Task<int> GetAverageBetYams()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //Get all Pots from played games
            var betsYams = await _rankGateway.GetPlayerYamsBets(userId);
            var allbetsyams = betsYams.ToList();

            //Divid pot by 2 to get player's bet
            List<int> items = new List<int>();
            foreach (var item in allbetsyams)
            {
                items.Add(item / 2);
            }

            //Sum up bets
            var sommeYams = 0;
            foreach (var item in items) sommeYams += item;
            sommeYams = sommeYams / allbetsyams.Count();

            return sommeYams;
        }

        public async Task<int> GetAverageBetBJ()
        {
            //Get all pots from played games
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var betsBJ = await _rankGateway.GetPlayerBJBets(userId);
            var allbetsBJ = betsBJ.ToList();
             
            //Divide pot by 2 to get player's bet
            List<int> items = new List<int>();
            foreach (var item in allbetsBJ)
            {
                items.Add(item / 2);
            }

            //Sum up bets
            var sommeBJ = 0;
            foreach (var item in items) sommeBJ += item;
            sommeBJ = sommeBJ / allbetsBJ.Count();

            return sommeBJ;
        }

        [HttpGet("{pseudo}/getwinsBlackJackPlayer")]
        public async Task<IActionResult> GetWinsBlackJackPlayer(string pseudo="")
        {
            int userId;
            if (pseudo == "")
            {
                userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                var userData = await _userGateway.FindByName(pseudo);
                userId = userData.UserId;
            }
            var result = await _gameGateway.GetWins(userId, 1);
            return this.CreateResult(result);
        }

        [HttpGet("{pseudo}/getlossesBlackJackPlayer")]
        public async Task<IActionResult> GetLossesBlackJackPlayer(string pseudo)
        {
            int userId;
            if (pseudo == "")
            {
                userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                var userData = await _userGateway.FindByName(pseudo);
                userId = userData.UserId;
            }
            var result = await _gameGateway.GetLosses(userId, 1);
            return this.CreateResult(result);
        }

        [HttpGet("{pseudo}/getwinsYamsPlayer")]
        public async Task<IActionResult> GetWinsYamsPlayer(string pseudo)
        {
            int userId;
            if (pseudo == "")
            {
                userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                var userData = await _userGateway.FindByName(pseudo);
                userId = userData.UserId;
            }
            var result = await _gameGateway.GetWins(userId, 0);
            return this.CreateResult(result);
        }

        [HttpGet("{pseudo}/getlossesYamsPlayer")]
        public async Task<IActionResult> GetLossesYamsPlayer(string pseudo)
        {
            int userId;
            if (pseudo == "")
            {
                userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                var userData = await _userGateway.FindByName(pseudo);
                userId = userData.UserId;
            }
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

        [HttpPost("GameEndUpdate/{gametype}/{win}/{trueOrFake}")]
        public async Task<IActionResult> GameEndUpdate(int gametype, string win, string trueOrFake)
        {
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            GameData data = await _gameGateway.GetGameByPlayerId(userid,gametype);
            UserData udata = await _userGateway.FindById(userid);
            Result result;

            if (win == "Player")
            {
                result = await _gameGateway.UpdateGameEnd(data.GameId, gametype, udata.UserName);
                await _gameGateway.ActionEndGame(udata.UserId, udata.UserName, DateTime.UtcNow, gametype, data.GameId, "win", trueOrFake);
            }
            else if (win == "Draw")
            {
                result = await _gameGateway.UpdateGameEnd(data.GameId, gametype, "Draw");
                await _gameGateway.ActionEndGame(udata.UserId, udata.UserName, DateTime.UtcNow, gametype, data.GameId, "draw", trueOrFake);

            } else
            {
                var IA = await _userGateway.FindByName("#AI" + userid);
                result = await _gameGateway.UpdateGameEnd(data.GameId, gametype, "#AI" + userid);
                await _gameGateway.ActionEndGame(udata.UserId, udata.UserName, DateTime.UtcNow, gametype, data.GameId, "lost", trueOrFake);
            }

            return this.CreateResult(result);
        }

    }
}