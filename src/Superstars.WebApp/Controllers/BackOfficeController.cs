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
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme,Policy = "IsAdmin")]
    public class BackOfficeController : Controller
    {
        private readonly UserGateway _userGateway;
        private readonly RankGateway _rankGateway;
        public BackOfficeController(UserGateway userGateway, RankGateway rankGateway)
        {
            _userGateway = userGateway;
            _rankGateway = rankGateway;
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

        [HttpGet("getUsers")]
        public async Task<IEnumerable<string>> GetUsers()
        {
            var result = await _rankGateway.PseudoList();
            return result;
        }

        [HttpDelete("{UserPseudo}/deleteUser")]
        public async Task<Result> DeleteUser(string UserPseudo)
        {
            var user = await _userGateway.FindByName(UserPseudo);
            return await _userGateway.Delete(user.UserId);
        }
    }
}
