using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class SettingsController : Controller
    {
        private readonly UserService _userService;
        
        public SettingsController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("{oldPassword}/{newPassword}")]
        public async Task<bool> UpdatePassword(string oldPassword, string newPassword)
        {
            var pseudo = User.FindFirst(ClaimTypes.Name).Value;
            var user = await _userService.FindUser(pseudo, oldPassword);
            if (user != null)
            {
                await _userService.UpdatePassword(user.UserId, newPassword);
                return true;
            }
            else
                return false;
        }

        [HttpPost("{newMail}")]
        public async Task<bool> UpdateMail(string newMail)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(newMail);
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _userService.UpdateMail(userId, newMail);
                return addr.Address == newMail;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet("getMail")]
        public async Task<string> GetMail()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userService.FindByUserId(userId);
            return user.Email;
        }
    }
}