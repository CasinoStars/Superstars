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
    public class BlackJackController : Controller
    {
        private readonly BlackJackGateway _blackJackGateway;
        private readonly BlackJackService _blackJackService;
        private readonly UserGateway _userGateway;
        private readonly GameGateway _gameGateway;

        public BlackJackController(BlackJackService blackJackService, BlackJackGateway blackJackGateway,
            UserGateway userGateway, GameGateway gameGateway)
        {
            _blackJackGateway = blackJackGateway;
            _userGateway = userGateway;
            _blackJackService = blackJackService;
            _gameGateway = gameGateway;
        }

        // Create a BlackJackPlayer in t.BlackJackPlayer
        [HttpPost("CreatePlayer")]
        public async Task<IActionResult> CreateJackPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _blackJackGateway.CreateJackPlayer(userId, 0);
            return this.CreateResult(result);
        }

        //[HttpDelete("deleteBlackJackPlayer")]
        //public async Task<Result> DeleteBlackJackPlayer()
        //{
        //    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    int gameId = await _gameGateway.GetGameIdToDeleteByPlayerId(userId, 1);
        //    return await _blackJackGateway.DeleteBlackJackPlayer(gameId);
        //}

        // Create a IA BlackJackPlayer in t.BlackJackPlayer
        [HttpPost("CreateAi")]
        public async Task<IActionResult> CreateJackAiPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _blackJackGateway.CreateJackAi(userId, 0);
            return this.CreateResult(result);
        }

        // Delete the IA BlackJackPlayer assigned to the current BlackJackPlayer in t.BlackJackPlayer 
        [HttpDelete("DeleteAi")]
        public async Task<IActionResult> DeleteJackAiPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _blackJackGateway.DeleteJackAi(userId);
            return this.CreateResult(result);
        }

        // Initializing only during first turn
        [HttpPost("InitPlayer")]
        public async Task<IActionResult> InitPlayer()
        {
            // Get data
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _blackJackGateway.GetPlayer(userId);

            // Init obj BlackJackService 
            _blackJackService.InitGame();

            //Draw 2 cards for player's hand
            _blackJackService._ennemyhand = _blackJackService.DrawCard(_blackJackService._ennemyhand);
            _blackJackService._ennemyhand = _blackJackService.DrawCard(_blackJackService._ennemyhand);

            var _cards = "";
            var i = 0;
            foreach (var item in _blackJackService._ennemyhand)
            {
                _cards += item.Value;
                _cards += item.Symbol;
                i++;

                if (i == 1)
                    _cards += ",";
            }

            data.PlayerCards = _cards;
            data.HandValue = _blackJackService.GetHandValue(_blackJackService._ennemyhand);

            //Update SQL
            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId,
                data.PlayerCards, data.NbTurn + 1, data.HandValue);
            return this.CreateResult(result);
        }

        // Initializing only during first turn or when player choose to stand
        [HttpPost("InitAI")]
        public async Task<IActionResult> InitAi()
        {
            // Get data
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindByName("#AI" + userId + "1");
            var data = await _blackJackGateway.GetPlayer(user.UserId);

            // Draw one card for IA hand
            _blackJackService._myhand = _blackJackService.DrawCard(_blackJackService._myhand);
            //_blackJackService._myhand = _blackJackService.DrawCard(_blackJackService._myhand);

            var _cards = "";
            var i = 0;
            foreach (var item in _blackJackService._myhand)
            {
                _cards += item.Value;
                _cards += item.Symbol;
                i++;

                if (i == 1)
                    _cards += ",";
            }

            data.PlayerCards = _cards;
            data.HandValue = _blackJackService.GetHandValue(_blackJackService._myhand);

            // Update SQL
            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId,
                data.PlayerCards, data.NbTurn + 1, data.HandValue);
            return this.CreateResult(result);
        }

        // A player choose to Hit
        [HttpPost("HitPlayer")]
        public async Task<IActionResult> HitPlayer()
        {
            // Get data
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _blackJackGateway.GetPlayer(userId);

            //Draw one card for player hand
            _blackJackService._ennemyhand = _blackJackService.DrawCard(_blackJackService._ennemyhand);

            var _cards = "";
            var i = 0;
            foreach (var item in _blackJackService._ennemyhand)
            {
                _cards += item.Value;
                _cards += item.Symbol;
                i++;

                if (i > 0)
                    _cards += ",";
            }

            data.PlayerCards = _cards;
            data.HandValue = _blackJackService.GetHandValue(_blackJackService._ennemyhand);

            //Update SQL
            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId,
                data.PlayerCards, data.NbTurn + 1, data.HandValue);
            return this.CreateResult(result);
        }

        //[HttpPost("HitAI")]
        //public async Task<IActionResult> HitAi()
        //{
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    UserData user = await _userGateway.FindByName("AI" + userId);
        //    BlackJackData data = await _blackJackGateway.GetPlayer(user.UserId);

        //    _blackJackService._ennemyhand = _blackJackService.DrawCard(_blackJackService._ennemyhand);
        //    string _cards = "";
        //    int i = 0;
        //    foreach (var item in _blackJackService._ennemyhand)
        //    {
        //        _cards += item.Value;
        //        _cards += item.Symbol;
        //        i++;

        //        if (i > 0)
        //            _cards += ",";
        //    }
        //    data.PlayerCards = _cards;
        //    data.HandValue = _blackJackService.GetHandValue(_blackJackService._ennemyhand);
        //    Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId, data.PlayerCards, data.NbTurn + 1, data.HandValue);
        //    return this.CreateResult(result);
        //}

        //[HttpPost("HitPlayerSecondCards")]
        //public async Task<IActionResult> HitPlayerSecondHand()
        //{
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    BlackJackData data = await _blackJackGateway.GetPlayer(userId);
        //    _blackJackService._ennemysecondhand = _blackJackService.DrawCard(_blackJackService._ennemysecondhand);
        //    string _cards = "";
        //    int i = 0;
        //    foreach (var item in _blackJackService._ennemysecondhand)
        //    {
        //        _cards += item.Value;
        //        _cards += item.Symbol;
        //        i++;

        //        if (i > 0)
        //            _cards += ",";
        //    }
        //    data.SecondPlayerCards = _cards;
        //    data.SecondHandValue = _blackJackService.GetHandValue(_blackJackService._ennemysecondhand);
        //    Result result = await _blackJackGateway.UpdatePlayerSecondPlayerCards(data.BlackJackPlayerID, data.BlackJackGameId, data.SecondPlayerCards);
        //    return this.CreateResult(result);
        //}

        // Return a player's hand as a string, each card separated by ","
        [HttpGet("GetPlayerCards")]
        public async Task<string> GetPlayerCards()
        {
            var cards = _blackJackService._ennemyhand;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _blackJackGateway.GetPlayer(userId);
            var playercards = await _blackJackGateway.GetPlayerCards(userId, data.BlackJackGameId);
            return playercards;
        }

        //[HttpGet("GetSecondPlayerCards")]
        //public async Task<string> GetSecondPlayerCards()
        //{
        //    List<Card> cards = _blackJackService._ennemyhand;
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    BlackJackData data = await _blackJackGateway.GetPlayer(userId);
        //    string playercards = await _blackJackGateway.GetPlayerSecondCards(userId, data.BlackJackGameId);
        //    return playercards;
        //}

        // Return an IA hand as a string, each card separated by ","
        [HttpGet("GetAiCards")]
        public async Task<string> GetAiCards()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindByName("#AI" + userId + "1");
            var data = await _blackJackGateway.GetPlayer(user.UserId);
            var playercards = await _blackJackGateway.GetPlayerCards(user.UserId, data.BlackJackGameId);
            return playercards;
        }

        // Return the turn number (int)
        [HttpGet("getTurn")]
        public async Task<IActionResult> GetTurn()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _blackJackGateway.GetGameId(userId);
            var result = await _blackJackGateway.GetTurn(data.BlackJackPlayerID, data.BlackJackGameId);
            return this.CreateResult(result);
        }

        // Return player hand value
        [HttpGet("getplayerHandValue")]
        public async Task<int> GetPlayerHandValue()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _blackJackGateway.GetGameId(userId);
            var handv = await _blackJackGateway.GetPlayerHandValue(userId, data.BlackJackGameId);
            return handv;
        }

        //[HttpGet("getplayerSecondHandValue")]
        //public async Task<int> GetPlayerSecondHandValue()
        //{
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    BlackJackData data = await _blackJackGateway.GetGameId(userId);
        //    int handv = await _blackJackGateway.GetPlayerSecondHandValue(userId, data.BlackJackGameId);
        //    return handv;
        //}

        // Return AI hand value
        [HttpGet("getAiHandValue")]
        public async Task<int> GetAiHandValue()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindByName("#AI" + userId + "1");
            var data = await _blackJackGateway.GetGameId(user.UserId);
            var handv = await _blackJackGateway.GetPlayerHandValue(user.UserId, data.BlackJackGameId);
            return handv;
        }

        // A player choose to Stand
        [HttpGet("StandPlayer")]
        public async Task<bool> StandPlayer()
        {
            var result = _blackJackService.FinishTurn();
            await InitAi();
            return result;
        }

        // true if it's dealer/AI turn, false otherwise
        [HttpGet("refreshAiturn")]
        public bool refreshAiturn()
        {
            var result = _blackJackService._dealerTurn;
            return result;
        }

        //[HttpGet("canSplitPlayer")]
        //public bool CanSplitPlayer()
        //{
        //    bool result = _blackJackService.CanSplit(_blackJackService._ennemyhand);
        //    return result;
        //}

        //[HttpPost("SplitPlayer")]
        //public async Task<bool> SplitPlayer()
        //{
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    bool result = _blackJackService.SplitHand();
        //    BlackJackData data = await _blackJackGateway.GetPlayer(userId);
        //    string _cards = "";
        //    string toreplace = ",";
        //    int i = 0;
        //    foreach (var item in _blackJackService._ennemysecondhand)
        //    {
        //        _cards += item.Value;
        //        _cards += item.Symbol;
        //        toreplace += item.Value;
        //        toreplace += item.Symbol;
        //        i++;

        //        if (i > 0)
        //            _cards += ",";
        //    }
        //    data.SecondPlayerCards = _cards;
        //    data.SecondHandValue = _blackJackService.GetHandValue(_blackJackService._ennemysecondhand);
        //    await _blackJackGateway.DeleteLastCard(toreplace, data.BlackJackPlayerID, data.BlackJackGameId);
        //    await _blackJackGateway.UpdatePlayerSecondPlayerCards(data.BlackJackPlayerID, data.BlackJackGameId, data.SecondPlayerCards);
        //    return result;
        //}

        //[HttpGet("HasSplit")]
        //public int HasSplit()
        //{
        //    if(_blackJackService.Hasplayersplit())
        //    {
        //        return 1;
        //    } else
        //    {
        //        return 0;
        //    }
        //   //return  _blackJackService.Hasplayersplit();
        //}

        [HttpPost("PlayAi")]
        public async Task<IActionResult> PlayAi()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindByName("#AI" + userId + "1");
            var data = await _blackJackGateway.GetPlayer(user.UserId);

            if (_blackJackService._dealerTurn)
                _blackJackService._myhand =
                    _blackJackService.PlayIA(_blackJackService._myhand, _blackJackService._ennemyhand);

            var _cards = "";
            var i = 0;
            foreach (var item in _blackJackService._myhand)
            {
                _cards += item.Value;
                _cards += item.Symbol;
                i++;

                if (i > 0)
                    _cards += ",";
            }

            data.PlayerCards = _cards;
            data.HandValue = _blackJackService.GetHandValue(_blackJackService._myhand);
            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId,
                data.PlayerCards, data.NbTurn + 1, data.HandValue);
            return this.CreateResult(result);
        }
    }
}