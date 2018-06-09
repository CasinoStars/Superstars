using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
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

        public BlackJackController(BlackJackGateway blackJackGateway)
        {
            _blackJackGateway = blackJackGateway;
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

        [HttpPost("PlayerDrawCard")]
        public async Task<IActionResult> DrawCard()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BlackJackData data = await _blackJackGateway.GetPlayer(userId);
            _blackJackService._ennemyhand = _blackJackService.DrawCard(_blackJackService._ennemyhand);
            _blackJackService._ennemyhand = _blackJackService.DrawCard(_blackJackService._ennemyhand);
            string _cards = "";
            foreach (var item in _blackJackService._ennemyhand)
            {
                _cards += item.Value;
                _cards += item.Symbol;
            }
            data.PlayerCards = _cards;

            Result result = await _blackJackGateway.UpdateBlackJackPlayer(data.BlackJackPlayerID, data.BlackJackGameId, data.PlayerCards);
            return this.CreateResult(result);
        }
    }
}

