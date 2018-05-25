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
using System.Globalization;

namespace TestBlockChain
{
    class TransactionMaker
    {
        /// <summary>
        /// Cette classe a la responsabilité de crée des transaction et de les envoyer
        /// </summary>
        /// <param name="key"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static Transaction MakeATransaction(BitcoinSecret privateKey, BitcoinAddress destinationAdress, decimal amountToSend, decimal minerFee, int nbOfConfimationReq, QBitNinjaClient client)
        {
            Dictionary<OutPoint, double> UTXOS = informationSeeker.FindUtxo(privateKey, client, nbOfConfimationReq);
            var transaction = new Transaction();
            var me = privateKey.GetAddress();
            decimal total = 0;

            List<decimal> dif = new List<decimal>();
            foreach (var item in UTXOS)
            {
               if((decimal)item.Value - (amountToSend - minerFee) > 0) dif.Add((decimal)item.Value - (amountToSend - minerFee));           
            }

            var sortedDict = from entry in UTXOS orderby entry.Value descending select entry;
            int index = 0;
            double value = 0;

            foreach (var item in sortedDict)
            {
                value += item.Value;
                if ((decimal)value > amountToSend) break;
                index++;
            }
            int p = 0;
            foreach (var item in sortedDict)
            {
                transaction.Inputs.Add(new TxIn()
                {
                    PrevOut = item.Key
                });
                transaction.Inputs[p].ScriptSig = me.ScriptPubKey;
                total += (decimal)item.Value;
                if (p == index) break;
                if (total > amountToSend + minerFee) break;
                p++;                
            }

            if (amountToSend + minerFee > total) throw new ArgumentException(" AmountToSend + MinerFee should not be greater than the balance");

            TxOut destinationTxOut = new TxOut()
            {
                Value = new Money(amountToSend, MoneyUnit.BTC),
                ScriptPubKey = destinationAdress.ScriptPubKey
            };

            TxOut changeBackTxOut = new TxOut()
            {
                Value = new Money(total - amountToSend - minerFee, MoneyUnit.BTC),
                ScriptPubKey = me.ScriptPubKey
            };

            transaction.Outputs.Add(destinationTxOut);
            transaction.Outputs.Add(changeBackTxOut);
            transaction.Sign(privateKey, false);

            return transaction;         
        }

        public static void BroadCastTransaction(Transaction transaction, QBitNinjaClient client)
        {
            BroadcastResponse broadcastResponse = client.Broadcast(transaction).Result;

            if (!broadcastResponse.Success)
            {
                Console.Error.WriteLine("ErrorCode: " + broadcastResponse.Error.ErrorCode);
                Console.Error.WriteLine("Error message: " + broadcastResponse.Error.Reason);
            }
            else
            {
                Console.WriteLine("Success! You can check out the hash of the transaciton in any block explorer:");
                Console.WriteLine(transaction.GetHash());
            }
        }
       
    }
}

