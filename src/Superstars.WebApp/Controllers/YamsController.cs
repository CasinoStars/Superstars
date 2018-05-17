using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System.Threading.Tasks;
using Superstars.WebApp.Services;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class YamsController : Controller
    {
        readonly YamsService _yamsService;

        public YamsController(YamsService yamsService)
        {
            _yamsService = yamsService;
        }

        [HttpPost]
        public int[] RollDices(int[] mysdices, int[] selectedDices = null)
        {
            return _yamsService.RollDices(mysdices, selectedDices);
        }

        [HttpPost]
        public async Task<IActionResult> CreateYamsPlayer(int gameid, int nbturn, string dices, int dicesvalue)
        {
            Result result = await _yamsService.CreateYamsPlayer(gameid, nbturn, dices, dicesvalue);
            return this.CreateResult(result);
        }
    }
}
