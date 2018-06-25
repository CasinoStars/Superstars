using System;
using NBitcoin;
using QBitNinja.Client;
using System.Linq;
using System.Collections.Generic;

namespace Superstars.Wallet
{
    class Program
    {
        static void Main(string[] args)
        {

            //  Console.WriteLine("test");

            var network = Network.TestNet;
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            // BitcoinSecret key = new Key().GetBitcoinSecret(Network.TestNet);
            BitcoinSecret btcS = new BitcoinSecret("cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup");
            BitcoinSecret btcS2 = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm");

              decimal coinInWallet2 = informationSeeker.HowMuchCoinInWallet(btcS2, client);
            Console.WriteLine(coinInWallet2);

            if (coinInWallet2 > (decimal)0.1)
            {
                Transaction trxx = TransactionMaker.MakeATransaction(btcS2, btcS.GetAddress(), coinInWallet2 - (decimal)0.05, (decimal)0.05, 6, client);
                TransactionMaker.BroadCastTransaction(trxx, client);
            }

            decimal coinInWallet1 = informationSeeker.HowMuchCoinInWallet(btcS, client);
            Console.WriteLine(coinInWallet1);
           
            if (coinInWallet1 > (decimal)0.1)
            {
                Transaction trxx2 = TransactionMaker.MakeATransaction(btcS, btcS2.GetAddress(), coinInWallet1 - (decimal)0.05, (decimal)0.05, 6, client);
                TransactionMaker.BroadCastTransaction(trxx2, client);
            }

            Console.ReadKey();


        }
    }
}
