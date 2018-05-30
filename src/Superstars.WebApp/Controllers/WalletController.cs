using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
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
        /// <param name="pseudo">Name of player</param>
        /// <param name="coins">Number of coins want players</param>
        /// <param name="moneyType">Set 1 for RealCoins, 2 for FakeCoins</param>
        /// <returns></returns>
        [HttpPost("{pseudo}/{coins}/{moneyType}/AddCoins")]
        public async Task<IActionResult> AddCoins(string pseudo, int coins, int moneyType)
        {
            Result result = await _walletGateway.AddCoins(pseudo, coins, moneyType);
            return this.CreateResult(result);
        }
    }
}