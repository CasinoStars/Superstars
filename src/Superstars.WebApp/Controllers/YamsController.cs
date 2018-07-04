using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Services;
using System.Diagnostics;
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
        YamsService _yamsService;
        YamsIAService _yamsIAService;
        PasswordHasher _passwordHasher;


        public YamsController(YamsGateway yamsGateway, YamsService yamsService, YamsIAService yamsIAService, UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _yamsGateway = yamsGateway;
            _userGateway = userGateway;
            _yamsService = yamsService;
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
            int playerPts = _yamsService.PointCount(playerHand);
            int IaPts = _yamsService.PointCount(IaHand);
            int[] IaFinalDices;
            if (data.NbrRevives != 0)
            {
                int[] IaDicesForReRoll = _yamsIAService.GiveRerollHand(IaHand, playerPts);
                IaFinalDices = await _yamsService.Reroll(IaDicesForReRoll,userId);
                data.NbrRevives = data.NbrRevives + 1;
            }
            else
            {
                IaFinalDices = await _yamsService.Reroll(new int[] { 0, 0, 0, 0, 0 }, userId);
                data.NbrRevives = data.NbrRevives + 1;
            }
            string IaStringDices = null;
            for (int i = 0; i < IaHand.Length; i++)
            {
                IaStringDices += IaFinalDices[i];
            }
            Result result = await _yamsGateway.UpdateYamsPlayer(IA.UserId, data.YamsGameId, data.NbrRevives, IaStringDices, IaPts);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (timer.Elapsed.TotalSeconds < 3)
            {
                //Waitting for Ia RollDices
            }
            return this.CreateResult(result);
        }

        [HttpPost("Roll")]
        public async Task<IActionResult> RollDices([FromBody] int[] selectedDices = null)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            YamsData data = await _yamsGateway.GetPlayer(userId);
            data.NbrRevives = data.NbrRevives + 1;
            int[] playerDices = new int[5];
            string stringDices = data.Dices;

            for (int i = 0; i < 5; i++)
            {
                playerDices[i] = (int)char.GetNumericValue(stringDices[i]);
            }

            int[] newDices = playerDices;
            newDices = _yamsService.IndexChange(newDices, selectedDices);
            newDices = await _yamsService.Reroll(newDices, userId);
            int playerPts = _yamsService.PointCount(newDices);

            string playerStringDices = null;
            for (int i = 0; i < newDices.Length; i++)
            {
                playerStringDices += newDices[i];
            }

            Result result = await _yamsGateway.UpdateYamsPlayer(userId, data.YamsGameId, data.NbrRevives, playerStringDices, playerPts);
            return this.CreateResult(result);
        }

        [HttpGet("GetFinalResult")]
        public async Task<string[]> GetResultGame()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserData Ia = await _userGateway.FindByName("AI" + userId);
            YamsData playerData = await _yamsGateway.GetPlayer(userId);
            YamsData IaData = await _yamsGateway.GetPlayer(Ia.UserId);


            int[] IaDices = new int[5];
            int[] playerDices = new int[5];
            string stringIaDices = IaData.Dices;
            string stringPlayerDices = playerData.Dices;

            for (int i = 0; i < 5; i++)
            {
                IaDices[i] = (int)char.GetNumericValue(stringIaDices[i]);
                playerDices[i] = (int)char.GetNumericValue(stringPlayerDices[i]);
            }
            string[] result = _yamsService.TabFiguresAndWinner(IaDices, playerDices);
            return result;
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