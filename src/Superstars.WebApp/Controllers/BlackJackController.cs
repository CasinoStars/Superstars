using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;



namespace Superstars.WebApp.Controllers
{

    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class BlackJackController : Controller
    {
        BlackJackGateway _blackJackGateway;
        BlackJackService _blackJackService;
        UserGateway _userGateway;

        public BlackJackController(BlackJackService blackJackService, BlackJackGateway blackJackGateway, UserGateway userGateway)
        {
            _blackJackGateway = blackJackGateway;
            _userGateway = userGateway;
            _blackJackService = blackJackService;
        }

        [HttpPost("CreatePlayer")]
        public async Task<IActionResult> CreateJackPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _blackJackGateway.CreateJackPlayer(userId,0);
            return this.CreateResult(result);
        }

        [HttpPost("CreateAi")]
        public async Task<IActionResult> CreateJackAiPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _blackJackGateway.CreateJackAi(userId,0);
            return this.CreateResult(result);
        }

        [HttpDelete("DeleteAi")]
        public async Task<IActionResult> DeleteJackAiPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _blackJackGateway.DeleteJackAi(userId);
            return this.CreateResult(result);
        }


        [HttpPost("InitPlayer")]
        public async Task<IActionResult> InitPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BlackJackData data = await _blackJackGateway.GetPlayer(userId);
            _blackJackService.InitGame();
            _blackJackService._ennemyhand = _blackJackService.DrawCard(_blackJackService._ennemyhand);
            _blackJackService._ennemyhand = _blackJackService.DrawCard(_blackJackService._ennemyhand);

            string _cards = "";
            int i = 0;
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
            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId, data.PlayerCards, data.NbTurn + 1, data.HandValue);
            return this.CreateResult(result); 
        }

        [HttpPost("InitAI")]
        public async Task<IActionResult> InitAi()
        {   
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserData user = await _userGateway.FindByName("AI" + userId);
            BlackJackData data = await _blackJackGateway.GetPlayer(user.UserId);
            _blackJackService._myhand = _blackJackService.DrawCard(_blackJackService._myhand);
            //_blackJackService._myhand = _blackJackService.DrawCard(_blackJackService._myhand);
            string _cards = "";
            int i = 0;
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
            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId, data.PlayerCards, data.NbTurn + 1,data.HandValue);
            return this.CreateResult(result);
        }

        [HttpPost("HitPlayer")]
        public async Task<IActionResult> HitPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BlackJackData data = await _blackJackGateway.GetPlayer(userId);
            _blackJackService._ennemyhand = _blackJackService.DrawCard(_blackJackService._ennemyhand);
            string _cards = "";
            int i = 0;
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
            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId, data.PlayerCards, data.NbTurn + 1, data.HandValue);
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

        [HttpGet("GetPlayerCards")]
        public async Task<string> GetPlayerCards()
        {
            List<Card> cards = _blackJackService._ennemyhand;
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BlackJackData data = await _blackJackGateway.GetPlayer(userId);
            string playercards = await _blackJackGateway.GetPlayerCards(userId, data.BlackJackGameId);
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

        [HttpGet("GetAiCards")]
        public async Task<string> GetAiCards()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserData user = await _userGateway.FindByName("AI" + userId);
            BlackJackData data = await _blackJackGateway.GetPlayer(user.UserId);
            string playercards = await _blackJackGateway.GetPlayerCards(user.UserId, data.BlackJackGameId);
            return playercards;
        }

        [HttpGet("getTurn")]
        public async Task<IActionResult> GetTurn()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BlackJackData data = await _blackJackGateway.GetGameId(userId);
            Result<int> result = await _blackJackGateway.GetTurn(data.BlackJackPlayerID, data.BlackJackGameId);
            return this.CreateResult(result);
        }

        [HttpGet("getplayerHandValue")]
        public async Task<int> GetPlayerHandValue()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BlackJackData data = await _blackJackGateway.GetGameId(userId);
            int handv = await _blackJackGateway.GetPlayerHandValue(userId, data.BlackJackGameId);
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

        [HttpGet("getAiHandValue")]
        public async Task<int> GetAiHandValue()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserData user = await _userGateway.FindByName("AI" + userId);
            BlackJackData data = await _blackJackGateway.GetGameId(user.UserId);
            int handv = await _blackJackGateway.GetPlayerHandValue(user.UserId, data.BlackJackGameId);
            return handv;
        }

        [HttpGet("StandPlayer")]
        public async Task<bool> StandPlayer()
        {
            bool result = _blackJackService.FinishTurn();
            await InitAi();
            return result;
        }

        [HttpGet("refreshAiturn")]
        public bool refreshAiturn()
        {
            bool result = _blackJackService._dealerTurn;
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
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserData user = await _userGateway.FindByName("AI" + userId);
            BlackJackData data = await _blackJackGateway.GetPlayer(user.UserId);

            if (_blackJackService._dealerTurn)
            {
                _blackJackService._myhand = _blackJackService.PlayIA(_blackJackService._myhand, _blackJackService._ennemyhand);
            }

            string _cards = "";
            int i = 0;
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
            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId, data.PlayerCards, data.NbTurn + 1, data.HandValue);
            return this.CreateResult(result);
        }
    }
}

