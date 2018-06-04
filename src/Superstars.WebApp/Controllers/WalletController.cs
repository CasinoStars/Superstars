using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class WalletController : Controller
    {
        WalletGateway _walletGateway;

        public WalletController(WalletGateway walletGateway)
        {
            _walletGateway = walletGateway;
        }

        /// <summary>
        /// Add Real or Fake Coins for player
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCoins([FromBody] WalletViewModel model)
        {
            Result result;
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (model.FakeCoins == 2)
            {
                result = await _walletGateway.AddCoins(userId, model.MoneyType, model.FakeCoins);
            }
            else
            {
                result = await _walletGateway.AddCoins(userId, model.MoneyType, model.RealCoins);
            }
            return this.CreateResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrueBalance()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result = await _walletGateway.GetTrueBalance(userId);
            return this.CreateResult(result);
        }

        [HttpGet("FakeBalance")]
        public async Task<IActionResult> GetFakeBalance()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result = await _walletGateway.GetFakeBalance(userId);
            return this.CreateResult(result);
        }
    }
}