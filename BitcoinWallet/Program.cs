using System;
using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using System.Threading;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitcointest
{
    class Program
    {
        static void Main(string[] args)
        {


            var bitcoinPrivateKey = new BitcoinSecret("cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup", Network.TestNet);
            var me = BitcoinAddress.Create("mzRnZHJodRUmE6cSPvGrhtcsgvhdVFYroa", Network.TestNet);
            var destinationAdress = BitcoinAddress.Create("mh2e7YHio7fTjLXHZ3KRXDfU52RbwQbhtK", Network.TestNet);

            var bitcoinPrivateKey2 = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm", Network.TestNet);
            var me2 = bitcoinPrivateKey2.GetAddress();

            //  TransactionMaker.MakeATransaction(bitcoinPrivateKey2,me2, 0.93m, 0.05m);
            var client = new QBitNinjaClient(Network.TestNet);

            List<GetTransactionResponse> responses = TransactionMaker.FindCoinInAWallet(bitcoinPrivateKey2, client);
            Dictionary<OutPoint, double> findutxo = TransactionMaker.FindUtxo(responses, bitcoinPrivateKey2, client = new QBitNinjaClient(Network.TestNet));
            TransactionMaker.MakeATransaction(bitcoinPrivateKey2, me, 0.37m, 0.05m);

            Console.ReadKey();
        }


    }
}
