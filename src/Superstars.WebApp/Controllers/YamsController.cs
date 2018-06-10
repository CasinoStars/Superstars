using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class YamsController : Controller
    {

        YamsGateway _yamsGateway;
        UserGateway _userGateway;
        YamsIAService _yamsIAService;
        PasswordHasher _passwordHasher;


        public YamsController(YamsGateway yamsGateway, YamsIAService yamsIAService, UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _yamsGateway = yamsGateway;
            _userGateway = userGateway;
            _yamsIAService = yamsIAService;
            _passwordHasher = passwordHasher;
        }


        [HttpPost("RollIa")]
        public async Task<IActionResult> RollIaDices([FromBody] int[][] dices)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserData IA = await _userGateway.FindByName("AI" + userId);
            YamsData data = await _yamsGateway.GetPlayer(IA.UserId);

            int[] playerHand = dices[1];
            int[] IaHand = dices[0];
            int playerPts = _yamsGateway.PointCount(playerHand);
            int IaPts = _yamsGateway.PointCount(IaHand);
            int[] IaDicesForReRoll = _yamsIAService.GiveRerollHand(IaHand, playerPts);
            int[] IaFinalDices = _yamsGateway.Reroll(IaDicesForReRoll);
            string IaStringDices = null;
            for(int i = 0; i < IaHand.Length; i++)
            {
                IaStringDices += IaFinalDices[i];
            }
            Result result = await _yamsGateway.UpdateYamsPlayer(IA.UserId, data.YamsGameId, data.NbrRevives, IaStringDices, IaPts);
            return this.CreateResult(result);
        }

        [HttpPost("Roll")]
        public async Task<IActionResult> RollDices([FromBody] int[] selectedDices = null)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            YamsData data = await _yamsGateway.GetPlayer(userId);
            data.NbrRevives = data.NbrRevives + 1;
            int[] secondarray = new int[5];
            string dices = data.Dices;
            for (int i = 0; i < 5; i++)
            {
                secondarray[i] = (int)char.GetNumericValue(dices[i]);
            }
            var myDices = secondarray;
            myDices = _yamsGateway.IndexChange(myDices, selectedDices);
            myDices = _yamsGateway.Reroll(myDices);
            string des = null;
            for (int i = 0; i < myDices.Length; i++)
            {
                des += myDices[i];
            }
            Result result = await _yamsGateway.UpdateYamsPlayer(userId, data.YamsGameId, data.NbrRevives, des, data.DicesValue);
            return this.CreateResult(result);
        }

        [HttpGet("getPlayerDices")]
        public async Task<IActionResult> GetPlayerDices()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            YamsData data = await _yamsGateway.GetGameId(userId);
            Result<string> result = await _yamsGateway.GetPlayerDices(userId, data.YamsGameId);
            return this.CreateResult(result);
        }

        [HttpGet("getIaDices")]
        public async Task<IActionResult> GetIaDices()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserData user = await _userGateway.FindByName("AI" + userId);
            YamsData data = await _yamsGateway.GetGameId(userId);

            Result<string> result = await _yamsGateway.GetIaDices(user.UserId, data.YamsGameId);
            return this.CreateResult(result);
        }

        [HttpGet("getTurn")]
        public async Task<IActionResult> GetTurn()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            YamsData data = await _yamsGateway.GetGameId(userId);

            Result<int> result = await _yamsGateway.GetTurn(userId, data.YamsGameId);
            return this.CreateResult(result);
        }

        [HttpPost("createPlayer")]
        public async Task<IActionResult> CreateYamsPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _yamsGateway.CreateYamsPlayer(userId, 0, "12345", 0);
            return this.CreateResult(result);
        }

        [HttpPost("createAI")]
        public async Task<IActionResult> CreateYamsAiPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _yamsGateway.CreateYamsAI(userId, 0, "12345", 0);
            return this.CreateResult(result);
        }

        [HttpDelete("deleteAI")]
        public async Task<IActionResult> DeleteYamsAiPlayer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _yamsGateway.DeleteYamsAi(userId);
            return this.CreateResult(result);
        }
    }
}