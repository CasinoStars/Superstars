﻿using System;
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
            var me = BitcoinAddress.Create("mzRnZHJodRUmE6cSPvGrhtcsgvhdVFYroa", Network.TestNet);

            var bitcoinPrivateKey2 = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm", Network.TestNet);
            var me2 = bitcoinPrivateKey2.GetAddress();

            List<GetTransactionResponse> responses = TransactionMaker.FindCoinInAWallet(bitcoinPrivateKey, new QBitNinjaClient(Network.TestNet));


          //  Blockchain myCoinBlockchain = new Blockchain();
           Blockchain myCoinBlockchain = TestSeria.ReadMyBlockChain("test4");
            myCoinBlockchain.Difficulty = 1;

            // Received a block from the P2P network.
            // Validate 300 coins transfer.
            Console.WriteLine("Mining a block...");

            
            

            myCoinBlockchain.AddBlock(new Block(1, "03/12/2017", responses));
      


            // this line below will cause the chain to be invalid.
          //  myCoinBlockchain.GetLatestBlock().PreviousHash = "";

            // Validate the chain
            myCoinBlockchain.ValidateChain();




            foreach (var item in myCoinBlockchain._chain)
            {
                foreach (var transaction in item.Data)
                {
                    Console.WriteLine(transaction + " Transaction !" );

                }
            }

            TestSeria.SerializeMyBlockChain(myCoinBlockchain, "test4");

            informationSeeker.HowMuchCoinInWallet(bitcoinPrivateKey);

            Console.WriteLine("Done");

           
            Console.ReadKey();
        }
    }
}
