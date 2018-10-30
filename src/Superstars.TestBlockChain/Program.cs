using System;
using NBitcoin;
using QBitNinja.Client;

namespace Superstars.Wallet
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //  Console.WriteLine("test");

            var network = Network.TestNet;
            var client = new QBitNinjaClient(Network.TestNet);
            // BitcoinSecret key = new Key().GetBitcoinSecret(Network.TestNet);
            var btcS = new BitcoinSecret("cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup");
            var btcS2 = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm");

            var coinInWallet2 = informationSeeker.HowMuchCoinInWallet(btcS, client);
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

            bool hugo = Validator.IsValidAddress("mzRnZHJodRUmE6cSPvGrhtcsgvhdVFYroa");
            bool loiseau = Validator.IsValidAddress("mh2e7YHio7fTjLXHZ3KRXDfU52RbwQbhtK");
            bool marceau = Validator.IsValidAddress("21232e7YHio7fTjLXHZ3KRXDfU52RbwQbhtK");
        }
    }
}