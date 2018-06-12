using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Superstars.Wallet;
using NBitcoin;
using QBitNinja.Client;

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
        public async Task<IActionResult> AddFakeCoins([FromBody] WalletViewModel model)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);           
            Result result = await _walletGateway.AddCoins(userId, model.MoneyType, model.FakeCoins);                      
            return this.CreateResult(result);
        }

        [HttpGet("TrueBalance")]
        public async Task<decimal> GetTrueBalance()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result1 = await _walletGateway.GetPrivateKey(userId);
            BitcoinSecret privateKey = new BitcoinSecret(/*result1.Content.PrivateKey*/"cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup");
            return informationSeeker.HowMuchCoinInWallet(privateKey, new QBitNinjaClient(Network.TestNet));
        }

        [HttpPost("{pot}/creditPlayer")]
        public async Task<IActionResult> CreditPlayersYamsPot(int pot)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _walletGateway.AddCoins(userId, 2, pot);
            return this.CreateResult(result);
        }

        [HttpPost("{pot}/withdraw")]
        public async Task<IActionResult> WithdrawBankRoll(int pot)
        {
            Result result = await _walletGateway.InsertInBankRoll(0, -pot);
            return this.CreateResult(result);
        }

        //public async Task<IActionResult> GetTrueBalance()
        //{
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    Result<WalletData> result = await _walletGateway.GetTrueBalance(userId);
        //    return this.CreateResult(result);
        //}

        [HttpGet("FakeBalance")]
        public async Task<IActionResult> GetFakeBalance()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result = await _walletGateway.GetFakeBalance(userId);
            return this.CreateResult(result);
        }

        [HttpGet("GetAddress")]
        public async Task<string> GetAddress()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result = await _walletGateway.GetPrivateKey(userId);
            BitcoinSecret privateKey = new BitcoinSecret(result.Content.PrivateKey);
            string Address = privateKey.GetAddress().ToString();
            return Address;

        }

        [HttpPost("Withdraw")]

        public  async void Withdraw([FromBody] WalletViewModel WalletViewModel)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result1 = await _walletGateway.GetPrivateKey(userId);
            BitcoinSecret privateKey = new BitcoinSecret(/*result1.Content.PrivateKey*/"cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup");
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            BitcoinAddress destinationAddress = BitcoinAddress.Create(WalletViewModel.DestinationAddress,Network.TestNet);
            var transaction = TransactionMaker.MakeATransaction(privateKey,destinationAddress, WalletViewModel.AmountToSend, (decimal)0.05, 6, client);
            TransactionMaker.BroadCastTransaction(transaction,client);
        }

    }
}