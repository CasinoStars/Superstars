using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using QBitNinja.Client;
using Superstars.DAL;
using Superstars.Wallet;
using Superstars.WebApp.Authentication;
using Superstars.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    //NO AUTHORIZE FOR RESFRESH PUBLIC BANKROLL
    public class WalletController : Controller
    {
        private readonly WalletGateway _walletGateway;
        private readonly UserGateway _userGateway;
        private readonly TransferGateway _transferGateway;

        public WalletController(WalletGateway walletGateway, UserGateway userGateway, TransferGateway transferGateway)
        {
            _walletGateway = walletGateway;
            _userGateway = userGateway;
            _transferGateway = transferGateway;
        }

        /// <summary>
        /// Add FakeCoin to user
        /// </summary>
        /// <param name="model">
        ///     - use model.AmountToSend to add amount
        ///     - use model.MoneyTypeId to set moneyTypeId in your front-end (0=> Fakecoin; 1=> Bitcoin)
        /// </param>
        /// <returns></returns>
        [HttpPost("AddCoins")]
        public async Task<IActionResult> AddFakeCoins([FromBody] WalletViewModel model)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _walletGateway.AddCoins(userId, model.MoneyType, model.AmountToSend, 0);
            return this.CreateResult(result);
        }

        [HttpPost("{pot}/creditFakePlayer")]
        public async Task<IActionResult> CreditPlayerFake(int pot)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _walletGateway.AddCoins(userId, 0, pot, pot);
            return this.CreateResult(result);
        }

        [HttpPost("TransferToPlayer")]

        public async void TransferToPlayer([FromBody] TransferViewModel model)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var receiverData = await _userGateway.FindByName(model.DestinationAccount);
            var userData = await _userGateway.FindById(userId);
           // var userName = await _userGateway.findb
            await _walletGateway.AddCoins(receiverData.UserId, 1, 0, model.AmountToSend);
            await _walletGateway.AddCoins(userId, 1, 0, -model.AmountToSend);
            await _transferGateway.CreateTransfer(userId, model.AmountToSend, receiverData.UserId);
            await _userGateway.ActionTransfer(userId, userData.UserName,receiverData.UserId,receiverData.UserName,model.AmountToSend,DateTime.UtcNow);
        }


        [HttpPost("isPseudoExist")]

        public async Task<bool> IsPseudoExist([FromBody] TransferViewModel model)
        {
            UserData result = await _userGateway.FindByName(model.DestinationAccount);
            bool isPseudoExist = false;
            isPseudoExist = (result == null) ? false : true;

            return isPseudoExist;
        }

        [HttpGet("{maxConfirmation}/GetTransaction")]

        public async Task<List<string>> GetTransaction(int maxConfirmation) {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result1 = await _walletGateway.GetPrivateKey(userId);
            BitcoinSecret privateKey = new BitcoinSecret(/*result1.Content.PrivateKey*/"cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm");
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            var response = await informationSeeker.SeekTrx(privateKey, client,maxConfirmation);

            return response;
        }

        [HttpGet("GetTransfer")]

        public async Task<List<TransferData>> GetTransfer()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var transferData = await _transferGateway.GetTransferData(userId);
            var transferList = transferData.ToList();
            return transferList;
        }



        [HttpPost("{pot}/creditBTCPlayer")]
        public async Task<IActionResult> CreditPlayerBTC(int pot)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result result = await _walletGateway.AddCoins(userId, 1, 0, pot);
            return this.CreateResult(result);
        }

        [HttpPost("{pot}/withdrawFakeBank")]
        public async Task<IActionResult> WithdrawFakeBankRoll(int pot)
        {
            var result = await _walletGateway.InsertInBankRoll(0, -pot);
            return this.CreateResult(result);
        }

        [HttpPost("{pot}/withdrawBtcBank")]
        public async Task<IActionResult> WithdrawBTCBankRoll(int pot)
        {
            var result = await _walletGateway.InsertInBankRoll(-pot, 0);
            return this.CreateResult(result);
        }

        [HttpGet("BTCBankRoll")]
        [AllowAnonymous]
        public async Task<decimal> GetBTCBankRoll()
        {
            var result = await _walletGateway.GetBTCBankRoll();
            var privateKey =
                new BitcoinSecret(
                    "cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup"); //PRIVATE KEY OF ALL'IN BANKROLL
            var onBlockchain = await informationSeeker.HowMuchCoinInWallet(privateKey,
                    new QBitNinjaClient(Network.TestNet)); //AMOUNT BTC ON BLOCKCHAIN
            var allCredit = await _walletGateway.GetAllCredit();
            decimal BTCBank;
            if (allCredit.Content <= 0)
                BTCBank = onBlockchain - allCredit.Content;
            else
                BTCBank = onBlockchain + allCredit.Content;
            return BTCBank;
        }

        [HttpGet("FakeBankRoll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFakeBankRoll()
        {
            var result = await _walletGateway.GetFakeBankRoll();
            return this.CreateResult(result);
        }

        [HttpGet("TrueBalance")]
        public async Task<int> GetTrueBalance()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result1 = await _walletGateway.GetPrivateKey(userId);
            var privateKey = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm");
            if (userId == 0)
                privateKey = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm");
            else
                privateKey =
                    new BitcoinSecret(
                        result1.Content.PrivateKey /*"cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup"*/);
            var onBlockchain = await informationSeeker.HowMuchCoinInWallet(privateKey, new QBitNinjaClient(Network.TestNet));
            var credit = await _walletGateway.GetCredit(userId);
            var realBalance = onBlockchain + credit.Content;
            return realBalance;
        }

        [HttpPost("AddressValidator")]
        public async Task<bool> AddressValidator([FromBody] WalletViewModel model)
        {
            return Validator.IsValidAddress(model.DestinationAddress);
        }

        [HttpGet("FakeBalance")]
        public async Task<IActionResult> GetFakeBalance()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _walletGateway.GetFakeBalance(userId);
            return this.CreateResult(result);
        }

        [HttpGet("GetAddress")]
        public async Task<string> GetAddress()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _walletGateway.GetPrivateKey(userId);
            var privateKey = new BitcoinSecret(result.Content.PrivateKey);
            var Address = privateKey.GetAddress().ToString();
            return Address;
        }

        [HttpPost("Withdraw")]
        public async Task<List<string>> WithdrawPlayer([FromBody] WalletViewModel WalletViewModel)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Result<WalletData> result1 = await _walletGateway.GetPrivateKey(userId);
            BitcoinSecret privateKey = new BitcoinSecret(/*result1.Content.PrivateKey*/"cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm");
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            BitcoinAddress destinationAddress = BitcoinAddress.Create(WalletViewModel.DestinationAddress,Network.TestNet);
            var transaction = await TransactionMaker.MakeATransaction(privateKey,destinationAddress, WalletViewModel.AmountToSend, 5000, 6, client);
            List<string> response =  TransactionMaker.BroadCastTransaction(transaction,client);

            return response;
        }
    }
}