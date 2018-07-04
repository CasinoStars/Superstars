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
using System.Collections.Generic;

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
		[HttpPost("AddCoins")]
		public async Task<IActionResult> AddFakeCoins([FromBody] WalletViewModel model)
		{
			int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			Result result = await _walletGateway.AddCoins(userId, model.MoneyType, model.FakeCoins,0);
			return this.CreateResult(result);
		}

		[HttpPost("UpdateRealProfit")]
		public async Task<IActionResult> UpdateRealProfit([FromBody] int profit)
		{
			int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			Result result = await _walletGateway.AddCoins(userId, 1, 0, profit);
			return this.CreateResult(result);
		}

		[HttpPost("UpdateFakeProfit")]
		public async Task<IActionResult> UpdateFakeProfit([FromBody] int profit)
		{
			int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			Result result = await _walletGateway.AddCoins(userId, 2, 0, profit);
			return this.CreateResult(result);
		}

		[HttpPost("{pot}/creditFakePlayer")]
        public async Task<IActionResult> CreditPlayerFake(int pot)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _walletGateway.AddCoins(userId, 2,0,0, pot);
            return this.CreateResult(result);
        }

        [HttpPost("{pot}/creditBTCPlayer")]
        public async Task<IActionResult> CreditPlayerBTC(decimal pot)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _walletGateway.AddCoins(userId, 1, 0,0, pot);
            return this.CreateResult(result);
        }

        [HttpPost("{pot}/withdrawFakeBank")]
        public async Task<IActionResult> WithdrawFakeBankRoll(int pot)
        {
            Result result = await _walletGateway.InsertInBankRoll(0, -pot);
            return this.CreateResult(result);
        }

        [HttpPost("{pot}/withdrawBtcBank")]
        public async Task<IActionResult> WithdrawBTCBankRoll(decimal pot)
        {
            Result result = await _walletGateway.InsertInBankRoll(-pot, 0);
            return this.CreateResult(result);
        }

        [HttpGet("BTCBankRoll")]
        public async Task<decimal> GetBTCBankRoll()
        {
            Result<int> result = await _walletGateway.GetBTCBankRoll();
            BitcoinSecret privateKey = new BitcoinSecret("cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup"); //PRIVATE KEY OF ALL'IN BANKROLL
            decimal onBlockchain = informationSeeker.HowMuchCoinInWallet(privateKey, new QBitNinjaClient(Network.TestNet)); //AMOUNT BTC ON BLOCKCHAIN
            Result<decimal> allCredit = await _walletGateway.GetAllCredit();
            decimal BTCBank = onBlockchain + allCredit.Content;
            return BTCBank;
        }

        [HttpGet("FakeBankRoll")]
        public async Task<IActionResult> GetFakeBankRoll()
        {
            Result<int> result = await _walletGateway.GetFakeBankRoll();
            return this.CreateResult(result);
        }

        [HttpGet("TrueBalance")]
        public async Task<decimal> GetTrueBalance()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result1 = await _walletGateway.GetPrivateKey(userId);
            BitcoinSecret privateKey = new BitcoinSecret(/*result1.Content.PrivateKey*/"cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup");
            decimal onBlockchain = informationSeeker.HowMuchCoinInWallet(privateKey, new QBitNinjaClient(Network.TestNet));
            Result<decimal> credit = await _walletGateway.GetCredit(userId);
            decimal realBalance = onBlockchain + credit.Content;
            return realBalance;
        }

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
        public  async Task<List<string>> WithdrawPlayer([FromBody] WalletViewModel WalletViewModel)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result1 = await _walletGateway.GetPrivateKey(userId);
            BitcoinSecret privateKey = new BitcoinSecret(/*result1.Content.PrivateKey*/"cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup");
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            BitcoinAddress destinationAddress = BitcoinAddress.Create(WalletViewModel.DestinationAddress,Network.TestNet);
            var transaction = TransactionMaker.MakeATransaction(privateKey,destinationAddress, WalletViewModel.AmountToSend, (decimal)0.05, 6, client);
            List<string> response =  TransactionMaker.BroadCastTransaction(transaction,client);

            return response;
        }
    }
}