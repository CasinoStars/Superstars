using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestBlockChain
{
    class informationSeeker
    {
        public static void SeekPendingTrx(BitcoinSecret privateKey)
        {
            var client = new QBitNinjaClient(Network.TestNet);
            List<GetTransactionResponse> responses = TransactionMaker.FindCoinInAWallet(privateKey, client);

            foreach (var response in responses)
            {
                if (response.Block.Confirmations == 0) Console.WriteLine(response.TransactionId + " Transaction ID ");
            }
        }

        public static void HowMuchCoinInWallet(BitcoinSecret privateKey)
        {
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);

            Money spentAmount = Money.Zero;
            foreach (var spentCoin in spentCoins)
            {
                spentAmount = (Money)spentCoin.Amount.Add(spentAmount);
            }
            Console.WriteLine(spentAmount.ToDecimal(MoneyUnit.BTC));
        }
    }
}
