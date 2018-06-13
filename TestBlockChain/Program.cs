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
using System.Runtime.Serialization;

namespace Superstars.Wallet
{
    class Program
    {
        static void Main(string[] args)
        {

            var network = Network.TestNet;
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            // BitcoinSecret key = new Key().GetBitcoinSecret(Network.TestNet);
            BitcoinSecret btcS = new BitcoinSecret("cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup");
            BitcoinSecret btcS2 = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm");


           //  Transaction trxx = TransactionMaker.MakeATransaction(btcS, btcS2.GetAddress(), (decimal)0.3,(decimal)0.05, 6,client);
            //TransactionMaker.BroadCastTransaction(trxx,client);

            List<GetTransactionResponse> pendingTrx = informationSeeker.SeekPendingTrx(btcS, client);


            
            List<OutPoint> Outpoints = new List<OutPoint>();
            foreach (var trx in pendingTrx)
             {
                foreach (var input in trx.Transaction.Inputs)
                {
                    Console.WriteLine(input.ScriptSig.GetSignerAddress(network) + "   TEST");
                    if (input.ScriptSig.GetSignerAddress(network) == btcS.GetAddress())
                    {
                        for (int i = 0; i < trx.Transaction.Outputs.Count; i++)
                        {
                            if (trx.Transaction.Outputs[i].ScriptPubKey.GetDestinationAddress(network) == btcS.GetAddress())
                                if(!Outpoints.Contains(new OutPoint(trx.TransactionId, i)))
                                 Outpoints.Add(new OutPoint(trx.TransactionId, i));
                        }
                       
                    }
                }
            }

            foreach (var outpoint in Outpoints)
            {
                Console.WriteLine(outpoint);
            }
            Console.ReadKey();
        }
    }
}
