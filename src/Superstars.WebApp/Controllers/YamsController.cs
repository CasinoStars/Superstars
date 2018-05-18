using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System.Threading.Tasks;
using Superstars.WebApp.Services;
using Superstars.WebApp.Models;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class YamsController : Controller
    {
        readonly YamsGateway _yamsGateway;

        public YamsController(YamsGateway yamsGateway)
        {
            _yamsGateway = yamsGateway;
        }

        [HttpPost]
        public async Task<IActionResult> RollDices(YamsViewModel model, int[] mydices, int[] selectedDices = null)
        {
            mydices = _yamsGateway.IndexChange(mydices, selectedDices);
            mydices = _yamsGateway.Reroll(mydices);
            Result result = await _yamsGateway.UpdateYamsPlayer(model.YamsGameId, model.NbrRevives, model.Dices, model.DicesValue);
            return this.CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateYamsPlayer([FromBody] YamsViewModel model)
        {
            Result result = await _yamsGateway.CreateYamsPlayer(model.YamsGameId, model.NbrRevives, model.Dices, model.DicesValue);
            return this.CreateResult(result);
        }
    }
}
