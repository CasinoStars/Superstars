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
        public static List<GetTransactionResponse> SeekPendingTrx(BitcoinSecret privateKey)
        {
            var client = new QBitNinjaClient(Network.TestNet);
            List<GetTransactionResponse> responses = TransactionMaker.FindCoinInAWallet(privateKey, client);
            List<GetTransactionResponse> unconfirmedTrx = new List<GetTransactionResponse>(); 

            foreach (var response in responses)
            {
                if (response.Block.Confirmations == 0) unconfirmedTrx.Add(response);
            }
            return unconfirmedTrx;
        }


        public static decimal HowMuchCoinInWallet(BitcoinSecret privateKey)
        {
            var client = new QBitNinjaClient(Network.TestNet);
            List<GetTransactionResponse> responses = TransactionMaker.FindCoinInAWallet(privateKey, client);
            Dictionary<OutPoint, double> UTXOS = TransactionMaker.FindUtxo(responses, privateKey, client, 3);
            decimal total = 0;
            foreach (var item in UTXOS)
            {
                total += (decimal)item.Value;
            }
            return total;
        }
    }
}
