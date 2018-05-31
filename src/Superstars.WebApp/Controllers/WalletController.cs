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
        UserGateway _userGateway;

        public WalletController(WalletGateway walletGateway, UserGateway userGateway)
        {
            _walletGateway = walletGateway;
            _userGateway = userGateway;
        }

        /// <summary>
        /// Add Real or Fake Coins for player
        /// </summary>
        /// <param name="pseudo">Name of player</param>
        /// <param name="coins">Number of coins want players</param>
        /// <param name="moneyType">Set 1 for RealCoins, 2 for FakeCoins</param>
        /// <returns></returns>
        [HttpPost("{pseudo}")]
        public async Task<IActionResult> AddCoins(string pseudo, [FromBody] WalletViewModel model)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            if (model.FakeCoins != 0)
            {
                Result result1 = await _walletGateway.AddCoins(user.UserId, model.MoneyType, model.FakeCoins);
                return this.CreateResult(result1);
            }
            Result result = await _walletGateway.AddCoins(user.UserId, model.MoneyType, model.RealCoins );
            return this.CreateResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrueBalance(string pseudo)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            Result<WalletData> result = await _walletGateway.GetTrueBalance(user.UserId);
            return this.CreateResult(result);
        }

        [HttpGet("{pseudo}/FakeBalance")]
        public async Task<IActionResult> GetFakeBalance(string pseudo)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            Result<WalletData> result = await _walletGateway.GetFakeBalance(user.UserId);
            return this.CreateResult(result);
        }
    }
}