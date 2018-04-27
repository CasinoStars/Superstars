using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models.AccountViewModels;
using Superstars.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace Superstars.WebApp.Controllers
{
    [Route("api/user")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class UserController : Controller
    {
        readonly UserGateway _userGateway;
        readonly UserService _userService;
        readonly TokenService _tokenService;
        readonly Random _random;

        public UserController(UserGateway userGateway, UserService userService, TokenService tokenService)
        {
            _userGateway = userGateway;
            _userService = userService;
            _tokenService = tokenService;
            _random = new Random();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost(Name = "Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserData user = await _userService.FindUser(model.Pseudo, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
                await SignIn(user.Pseudo, user.UserId.ToString());
                return RedirectToAction(nameof(Authenticated));
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost(Name = "Register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Result<int> result = await _userService.CreateUser(model.Pseudo, model.Password);
                if (result.HasError)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(model);
                }
                await SignIn(model.Pseudo, result.Content.ToString());
                return RedirectToAction(nameof(Authenticated));
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthentication.AuthenticationScheme);
            ViewData["NoLayout"] = true;
            return View();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public IActionResult Authenticated()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string pseudo = User.FindFirst(ClaimTypes.Name).Value;
            Token token = _tokenService.GenerateToken(userId, pseudo);
            ViewData["BreachPadding"] = GetBreachPadding(); // Mitigate BREACH attack. See http://www.breachattack.com/
            ViewData["Token"] = token;
            ViewData["Pseudo"] = pseudo;
            ViewData["NoLayout"] = true;
            ViewData["Providers"] = "Superstars";
            return View();
        }

        async Task SignIn(string pseudo, string userId)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim( ClaimTypes.Name, pseudo, ClaimValueTypes.String ),
                new Claim( ClaimTypes.NameIdentifier, userId.ToString(), ClaimValueTypes.String )
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthentication.AuthenticationType, ClaimTypes.Name, string.Empty);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthentication.AuthenticationScheme, principal);
        }

        string GetBreachPadding()
        {
            byte[] data = new byte[_random.Next(64, 256)];
            _random.NextBytes(data);
            return Convert.ToBase64String(data);
        }
    }
}