using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Superstars.DAL;
using Superstars.Wallet;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models.AccountViewModels;
using Superstars.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using NBitcoin;


namespace Superstars.WebApp.Controllers
{
    public class UserController : Controller
    {
        readonly UserService _userService;
        readonly TokenService _tokenService;
        readonly Random _random;
        public UserController(UserService userService, TokenService tokenService)
        {
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

        [HttpPost]
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
                await SignIn(user.UserName, user.UserId.ToString());
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BitcoinSecret privateKey = new Key().GetBitcoinSecret(Network.TestNet);
                string privateKeyString = privateKey.ToString();
                Result result = await _userService.CreateUser(model.Pseudo, model.Password, model.Email, privateKeyString);
                if (result.HasError)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(model);
                }
                UserData user = await _userService.FindUser(model.Pseudo, model.Password);
                await SignIn(model.Pseudo, user.UserId.ToString());
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