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
            _blackJackService._myhand = _blackJackService.DrawCard(_blackJackService._myhand);
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

        [HttpGet("GetPlayerCards")]
        public async Task<string> GetPlayerCards()
        {
            List<Card> cards = _blackJackService._ennemyhand;
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BlackJackData data = await _blackJackGateway.GetPlayer(userId);
            string playercards = await _blackJackGateway.GetPlayerCards(userId, data.BlackJackGameId);
            return playercards;
        }

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
        public IActionResult StandPlayer()
        {
            bool result = _blackJackService.FinishTurn();
            return Ok(result);
        }

        [HttpGet("refreshAiturn")]
        public IActionResult refreshAiturn()
        {
            bool result = _blackJackService._dealerTurn;
            return Ok(result);
        }

        [HttpGet("canSplitPlayer")]
        public IActionResult CanSplitPlayer()
        {
            bool result = _blackJackService.CanSplit(_blackJackService._ennemyhand);
            return Ok(result);
        }
        
        [HttpPost("SplitPlayer")]
        public IActionResult SplitPlayer()
        {
            bool result = _blackJackService.SplitHand();
            return Ok(result);
        }
        //[HttpGet("canSplitAi")]
        //public IActionResult CanSplitAi()
        //{
        //    bool result = _blackJackService.CanSplit(_blackJackService._myhand);
        //    return Ok(result);
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

