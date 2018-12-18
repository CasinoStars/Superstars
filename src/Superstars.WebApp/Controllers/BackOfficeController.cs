using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System;
using System.Linq;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class BackOfficeController : Controller
    {
        private readonly UserGateway _userGateway;
        private readonly RankGateway _rankGateway;
        public BackOfficeController(UserGateway userGateway, RankGateway rankGateway)
        {
            _userGateway = userGateway;
            _rankGateway = rankGateway;
        }

        [HttpGet("isAdmin")]
        public async Task<bool> IsAdmin()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindById(userId);
            if(user.Role == "Admin")
            {
                return true;
            } else {
                return false;
            }        
        }

        [HttpGet("getLogs")]
        public async Task<IEnumerable<LogData>> GetLogs()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userGateway.FindById(userId);

            var result = await _rankGateway.GetLogs();
            var logsList = result.ToList();
            return logsList;
        }
    }
}
