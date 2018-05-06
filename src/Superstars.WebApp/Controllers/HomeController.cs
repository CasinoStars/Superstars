using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models;
using Superstars.WebApp.Services;

namespace Superstars.WebApp.Controllers
{
    public class HomeController : Controller
    {
        readonly TokenService _tokenService;
        public HomeController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public IActionResult Index()
        {
            ClaimsIdentity identity = User.Identities.SingleOrDefault(i => i.AuthenticationType == CookieAuthentication.AuthenticationType);
            if (identity != null)
            {
                string userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                string pseudo = identity.FindFirst(ClaimTypes.Name).Value;
                Token token = _tokenService.GenerateToken(userId, pseudo);
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
