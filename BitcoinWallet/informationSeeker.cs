using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bitcointest
{
    class informationSeeker
    {
        public static void SeekUnconfirmedTrxs(BitcoinSecret privateKey){
            var client = new QBitNinjaClient(Network.TestNet);
            List<GetTransactionResponse> responses = TransactionMaker.FindCoinInAWallet(privateKey, client);

            foreach (var response in responses)
            {
                if (response.Block.Confirmations == 0) Console.WriteLine(response.TransactionId + " Transaction ID " );
            }
            


        }
    }
}
