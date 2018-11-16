using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Services;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class YamsController : Controller
    {
        private PasswordHasher _passwordHasher;
        private readonly UserGateway _userGateway;

        private readonly YamsGateway _yamsGateway;
        private readonly YamsIAService _yamsIAService;
        private readonly YamsService _yamsService;
        private readonly GameGateway _gameGateway;


        public YamsController(YamsGateway yamsGateway, YamsService yamsService, YamsIAService yamsIAService,
            UserGateway userGateway, GameGateway gameGateway, PasswordHasher passwordHasher)
        {
            _yamsGateway = yamsGateway;
            _userGateway = userGateway;
            _yamsService = yamsService;
            _yamsIAService = yamsIAService;
            _passwordHasher = passwordHasher;
            _gameGateway = gameGateway;
        }


        //Roll player dices
        [HttpPost("RollIa")]
        public async Task<IActionResult> RollIaDices([FromBody] int[][] dices)
        {
            // Get IA data
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var IA = await _userGateway.FindByName("#AI" + userId);
            var data = await _yamsGateway.GetPlayer(IA.UserId);

            // Roll new dices
            var playerHand = dices[1];
            var IaHand = dices[0];
            var playerPts = _yamsService.PointCount(playerHand);
            var IaPts = _yamsService.PointCount(IaHand);
            int[] IaFinalDices;
            if (data.NbrRevives != 0)
            {
                var IaDicesForReRoll = _yamsIAService.GiveRerollHand(IaHand, playerPts);
                IaFinalDices = await _yamsService.Reroll(IaDicesForReRoll, userId);
                data.NbrRevives = data.NbrRevives + 1;
            }
            else
            {
                IaFinalDices = await _yamsService.Reroll(new[] {0, 0, 0, 0, 0}, userId);
                data.NbrRevives = data.NbrRevives + 1;
            }

            string IaStringDices = null;
            for (var i = 0; i < IaHand.Length; i++) IaStringDices += IaFinalDices[i];

            //Update SQL
            Result result =
                await _yamsGateway.UpdateYamsPlayer(IA.UserId, data.YamsGameId, data.NbrRevives, IaStringDices, IaPts);
            var timer = new Stopwatch();
            timer.Start();
            while (timer.Elapsed.TotalSeconds < 3)
            {
                //Waitting for Ia RollDices
            }

            return this.CreateResult(result);
        }

        //Roll player dices
        [HttpPost("Roll")]
        public async Task<IActionResult> RollDices([FromBody] int[] selectedDices = null)
        {
            //Get the player data
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _yamsGateway.GetPlayer(userId);
            var playerDices = new int[5];
            var stringDices = data.Dices;
            for (var i = 0; i < 5; i++) playerDices[i] = (int) char.GetNumericValue(stringDices[i]);

            //Roll new dices
            var newDices = playerDices;
            newDices = _yamsService.IndexChange(newDices, selectedDices);
            newDices = await _yamsService.Reroll(newDices, userId);
            var playerPts = _yamsService.PointCount(newDices);
            string playerStringDices = null;
            for (var i = 0; i < newDices.Length; i++) playerStringDices += newDices[i];

            //Update number of throws
            data.NbrRevives = data.NbrRevives + 1;

            //Update SQL
            Result result = await _yamsGateway.UpdateYamsPlayer(userId, data.YamsGameId, data.NbrRevives,
                playerStringDices, playerPts);
            return this.CreateResult(result);
        }

        // Return the result of a game
        [HttpGet("GetFinalResult")]
        public async Task<string[]> GetResultGame()
        {
            // Get data
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var Ia = await _userGateway.FindByName("#AI" + userId);
            var playerData = await _yamsGateway.GetPlayer(userId);
            var IaData = await _yamsGateway.GetPlayer(Ia.UserId);

            //Convert dices from string to int
            var IaDices = new int[5];
            var playerDices = new int[5];
            var stringIaDices = IaData.Dices;
            var stringPlayerDices = playerData.Dices;

            for (var i = 0; i < 5; i++)
            {
                IaDices[i] = (int) char.GetNumericValue(stringIaDices[i]);
                playerDices[i] = (int) char.GetNumericValue(stringPlayerDices[i]);
            }

            // Return the result of the game
            string[] result = _yamsService.TabFiguresAndWinner(IaDices, playerDices);
            return result;
        }

        //Return player dices
        [HttpGet("getPlayerDices")]
        public async Task<IActionResult> GetPlayerDices()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _yamsGateway.GetGameId(userId);
            var result = await _yamsGateway.GetPlayerDices(userId, data.YamsGameId);
            return this.CreateResult(result);
        }

        //Return IA dices
        [HttpGet("getIaDices")]
        public async Task<IActionResult> GetIaDices()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindByName("#AI" + userId);
            var data = await _yamsGateway.GetGameId(userId);

            var result = await _yamsGateway.GetIaDices(user.UserId, data.YamsGameId);
            return this.CreateResult(result);
        }

        // Return the turn 
        [HttpGet("getTurn")]
        public async Task<IActionResult> GetTurn()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var data = await _yamsGateway.GetGameId(userId);

            var result = await _yamsGateway.GetTurn(userId, data.YamsGameId);
            return this.CreateResult(result);
        }

        // Create a YamsPlayer in t.YamsPlayer
        [HttpPost("createPlayer")]
        public async Task<IActionResult> CreateYamsPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _yamsGateway.CreateYamsPlayer(userId, 0, "12345", 0);
            return this.CreateResult(result);
        }

        [HttpDelete("deleteYamsPlayer")]
        public async Task<Result> DeleteYamsPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int gameid = await _gameGateway.GetGameIdToDeleteByPlayerId(userId, 0);
            return await _yamsGateway.DeleteYamsPlayer(gameid);           
        }

        // Create a IA YamsPlayer in t.YamsPlayer
        [HttpPost("createAI")]
        public async Task<IActionResult> CreateYamsAiPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _yamsGateway.CreateYamsAI(userId, 0, "12345", 0);
            return this.CreateResult(result);
        }

        // Delete the IA YamsPlayer assigned to the current YamsPlayer in t.YamsPlayer 
        [HttpDelete("deleteAI")]
        public async Task<IActionResult> DeleteYamsAiPlayer()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _yamsGateway.DeleteYamsAi(userId);
            return this.CreateResult(result);
        }

    }
}