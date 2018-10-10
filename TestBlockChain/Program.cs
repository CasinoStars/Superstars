using System;
using NBitcoin;
using QBitNinja.Client;

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

              int coinInWallet2 = informationSeeker.HowMuchCoinInWallet(btcS, client);
            Console.WriteLine(coinInWallet2);

            //if (coinInWallet2 > 100000)
            //{
            //    Transaction trxx = TransactionMaker.MakeATransaction(btcS2, btcS.GetAddress(), coinInWallet2 - 50000, 50000, 6, client);
            //    TransactionMaker.BroadCastTransaction(trxx, client);
            //}

            //int coinInWallet1 = informationSeeker.HowMuchCoinInWallet(btcS, client);
            //Console.WriteLine(coinInWallet1);
           
            //if (coinInWallet1 > 100000)
            //{
            //    Transaction trxx2 = TransactionMaker.MakeATransaction(btcS, btcS2.GetAddress(), coinInWallet1 - 50000, 50000, 6, client);
            //    TransactionMaker.BroadCastTransaction(trxx2, client);
            //}

            Console.ReadKey();


        }
    }
}
