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

        public BlackJackController(BlackJackGateway blackJackGateway, BlackJackService blackJackService)
        {
            _blackJackGateway = blackJackGateway;
            _blackJackService = blackJackService;
        }

        [HttpPost("CreatePlayer")]
        public async Task<IActionResult> CreateJackPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _blackJackGateway.CreateJackPlayer(userId);
            return this.CreateResult(result);
        }

        [HttpPost("CreateAi")]
        public async Task<IActionResult> CreateJackAiPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _blackJackGateway.CreateJackAi(userId);
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
            _blackJackService = new BlackJackService();
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BlackJackData data = await _blackJackGateway.GetPlayer(userId);
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
            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId, data.PlayerCards);
            string playercards = await _blackJackGateway.GetPlayerCards(userId, data.BlackJackGameId);
            return this.CreateResult(result); 
        }

        [HttpGet("GetPlayerCards")]
        public List<Card> GetPlayerCards()
        {
            return _blackJackService._ennemyhand;
        }
    }
}

