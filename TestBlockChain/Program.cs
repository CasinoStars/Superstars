using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using NBitcoin;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using System.Threading;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System.Linq;
using System.Globalization;

namespace TestBlockChain
{
    class Program
    {
        static void Main(string[] args)
        {
            var bitcoinPrivateKey = new BitcoinSecret("cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup", Network.TestNet);
            var publicAddress = BitcoinAddress.Create("mzRnZHJodRUmE6cSPvGrhtcsgvhdVFYroa", Network.TestNet);

            var bitcoinPrivateKey2 = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm", Network.TestNet);
            var publicAddress2 = bitcoinPrivateKey2.GetAddress();

            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);

             List<GetTransactionResponse> alltrx = informationSeeker.SeekAllTransaction(bitcoinPrivateKey2, client);
             decimal totalCoinInWallet = informationSeeker.HowMuchCoinInWallet(bitcoinPrivateKey2,client);
             List<GetTransactionResponse> alltrxPending = informationSeeker.SeekPendingTrx(bitcoinPrivateKey2, client);

             Dictionary<OutPoint,double> UTXOS =  informationSeeker.FindUtxo(bitcoinPrivateKey2, client, 3);

            foreach (var item in UTXOS)
            {
                Console.WriteLine(item.Value +  "  TEST");
                Console.WriteLine(item.Key + " TEST");
            }

            Transaction trx1 = TransactionMaker.MakeATransaction(bitcoinPrivateKey2,publicAddress, 3.7m, 0.05m, 6, client);

            TransactionMaker.BroadCastTransaction(trx1,client);

            Console.WriteLine(totalCoinInWallet + " Btc In Wallet");


            foreach (var item in alltrx)
            {
                Console.WriteLine(item.TransactionId + " AllTrx");
            }

            foreach (var item in alltrxPending)
            {
                Console.WriteLine(item.TransactionId + " Pending");
            }       
            Console.ReadKey();
        }
    }
}
