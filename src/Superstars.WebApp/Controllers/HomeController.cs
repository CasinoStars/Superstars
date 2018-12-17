using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Services;

namespace Superstars.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TokenService _tokenService;

        public HomeController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public IActionResult Index()
        {
            var identity =
                User.Identities.SingleOrDefault(i => i.AuthenticationType == CookieAuthentication.AuthenticationType);
            if (identity != null)
            {
                var userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var pseudo = identity.FindFirst(ClaimTypes.Name).Value;
                var role = identity.FindFirst(ClaimTypes.Role).Value;

                var token = _tokenService.GenerateToken(userId, pseudo,role);
                ViewData["Token"] = token;
                ViewData["Pseudo"] = pseudo;
            }
            else
            {
                ViewData["Token"] = null;
                ViewData["Pseudo"] = null;
            }

            ViewData["NoLayout"] = true;
            return View();
        }
    }
}