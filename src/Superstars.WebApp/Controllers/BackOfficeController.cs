using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class BackOfficeController : Controller
    {
        private readonly UserGateway _userGateway;
        public BackOfficeController(UserGateway userGateway)
        {
            _userGateway = userGateway;
        }

        [HttpGet("isAdmin")]
        async Task<bool> IsAdmin()
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
    }
}
