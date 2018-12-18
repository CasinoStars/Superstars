using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models.AccountViewModels;
using Superstars.WebApp.Services;
using System.Xml;

namespace Superstars.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ProvablyFairGateway _provablyFairGateway;
        private readonly Random _random;
        private readonly TokenService _tokenService;
        private readonly UserService _userService;
        private readonly UserGateway _userGateway;

        public UserController(UserService userService, TokenService tokenService, ProvablyFairGateway provablyFairGateway)
        {
            _provablyFairGateway = provablyFairGateway;
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
                if (Regex.Match(model.Pseudo, @"\W").Success) // \W => 	Characters who are not alphanumeric
                {
                    ModelState.AddModelError(string.Empty, "Characters must be alphanumeric");
                    return View(model);
                }

                var user = await _userService.FindUser(model.Pseudo, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
                await SignIn(user.UserName, user.UserId.ToString(),true);
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
                if (Regex.Match(model.Pseudo, @"\W").Success) // \W => 	Characters who are not alphanumeric
                {
                    ModelState.AddModelError(string.Empty, "Characters must be alphanumeric");
                    return View(model);
                }

                var privateKey = new Key().GetBitcoinSecret(Network.TestNet);
                var privateKeyString = privateKey.ToString();

                var result = await _userService.CreateUser(model.Pseudo, model.Password, model.Email, privateKeyString,0);
                if (result.HasError)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    return View(model);
                }

                var privateKeyAi = new Key().GetBitcoinSecret(Network.TestNet);

                var user = await _userService.FindUser(model.Pseudo, model.Password);
                var userAI = await _userService.CreateUser("#AI" + user.UserId, "azertyuiop" + user.UserId, "", privateKeyAi.ToString(),0);
                await SignIn(model.Pseudo, user.UserId.ToString(),true);
                await _provablyFairGateway.AddSeeds(user.UserId);
                return RedirectToAction(nameof(Authenticated));
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public async Task<IActionResult> LogOff()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            UserData data = await _userService.FindByUserId(userId);
            //FOR DEVELOPEMENT
            try
            {
                await _userService.ActionDeconnexion(data.UserId, data.UserName, DateTime.UtcNow);
            } catch
            {
                
            }
            await HttpContext.SignOutAsync(CookieAuthentication.AuthenticationScheme);
            ViewData["NoLayout"] = true;
            return View();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthentication.AuthenticationScheme)]
        public IActionResult Authenticated()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var pseudo = User.FindFirst(ClaimTypes.Name).Value;
            var role = User.FindFirst(ClaimTypes.Role).Value;
            var token = _tokenService.GenerateToken(userId, pseudo/*, role*/);
            ViewData["BreachPadding"] = GetBreachPadding(); // Mitigate BREACH attack. See http://www.breachattack.com/
            ViewData["Token"] = token;
            ViewData["Pseudo"] = pseudo;
            ViewData["NoLayout"] = true;
            return View();
        }

        private async Task SignIn(string pseudo, string userId, bool isAdmin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, isAdmin? "Admin" : "User",ClaimValueTypes.String),
                new Claim(ClaimTypes.Name, pseudo, ClaimValueTypes.String),
                new Claim(ClaimTypes.NameIdentifier, userId, ClaimValueTypes.String)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthentication.AuthenticationType, ClaimTypes.Name,
                string.Empty);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthentication.AuthenticationScheme, principal);
            await _userService.ActionConnexion(int.Parse(userId), pseudo, DateTime.UtcNow);
        }

        private string GetBreachPadding()
        {
            var data = new byte[_random.Next(64, 256)];
            _random.NextBytes(data);
            return Convert.ToBase64String(data);
        }
    }
}