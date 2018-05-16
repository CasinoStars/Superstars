using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;
using Superstars.WebApp.Services;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class YamsController : Controller
    {
        readonly _yamsService;
        public YamsController(YamsService yamsService)
        {
            _yamsService = yamsService;
        }

        public async Task<IActionResult> RollDices(List<int> selectedDices = null)
        {
            Result result = await _yamsService.RollDices(selectedDices);
            return this.CreateResult(result);
        }
    }
}
