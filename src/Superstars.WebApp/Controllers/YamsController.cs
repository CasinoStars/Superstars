using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class YamsController : Controller
    {

        YamsGateway _yamsGateway;
        UserGateway _userGateway;
        PasswordHasher _passwordHasher;


        public YamsController(YamsGateway yamsGateway, UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _yamsGateway = yamsGateway;
            _userGateway = userGateway;
            _passwordHasher = passwordHasher;

        }

        [HttpPost("{pseudo}/Roll")]
        public async Task<IActionResult> RollDices(string pseudo, [FromBody] int[] selectedDices = null)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            YamsData data = await _yamsGateway.GetPlayer(user.UserId);
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
            Result result = await _yamsGateway.UpdateYamsPlayer(user.UserId, data.YamsGameId, data.NbrRevives, des, data.DicesValue);
            return this.CreateResult(result);
        }

        [HttpGet("{pseudo}")]
        public async Task<IActionResult> GetPlayerDices(string pseudo)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            YamsData data = await _yamsGateway.GetGameId(user.UserId);

            Result<string> result = await _yamsGateway.GetPlayerDices(user.UserId, data.YamsGameId);
            return this.CreateResult(result);
        }

        [HttpGet("{pseudo}/getTurn")]
        public async Task<IActionResult> GetTurn(string pseudo)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            YamsData data = await _yamsGateway.GetGameId(user.UserId);

            Result<int> result = await _yamsGateway.GetTurn(user.UserId, data.YamsGameId);
            return this.CreateResult(result);
        }

        [HttpPost("{pseudo}/createPlayer")]
        public async Task<IActionResult> CreateYamsPlayer(string pseudo)
        {
            Result result = await _yamsGateway.CreateYamsPlayer(pseudo, 0, "12345", 0);
            return this.CreateResult(result);
        }

        [HttpPost("{pseudo}/createAiUser")]
        public async Task<IActionResult> createAiUser(string pseudo)
        {   
            UserData user = await _userGateway.FindByName(pseudo);


            Result result = await _userGateway.CreateUser("AI" + pseudo, _passwordHasher.HashPassword(user.UserName), "");
            return this.CreateResult(result);
        }

        [HttpPost("{pseudo}/createAI")]
        public async Task<IActionResult> CreateYamsAiPlayer(string pseudo)
        {
            Result result = await _yamsGateway.CreateYamsAI(pseudo, 0, "12345", 0);
            return this.CreateResult(result);
        }

        [HttpDelete("{pseudo}/deleteAI")]
        public async Task<IActionResult> DeleteYamsAiPlayer(string pseudo) 
        {
            Result result = await _yamsGateway.DeleteYamsAi(pseudo);
            return this.CreateResult(result);
        }
    }
	
}