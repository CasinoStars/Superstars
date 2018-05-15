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

            var bitcoinPrivateKey2 = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm", Network.TestNet);
            var me2 = bitcoinPrivateKey2.GetAddress();

            TransactionMaker.MakeATransaction(bitcoinPrivateKey2, me, 1.22m, 0.05m,3);
            informationSeeker.SeekUnconfirmedTrxs(bitcoinPrivateKey);
            Console.ReadKey();
        }


    }
}
