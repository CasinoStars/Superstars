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
        public static Transaction MakeATransaction(BitcoinSecret senderPrivateKey, BitcoinAddress destinationAdress, decimal amountToSend, decimal minerFee, int nbOfConfimationReq, QBitNinjaClient client)
        {
            decimal total = informationSeeker.HowMuchCoinInWallet(senderPrivateKey, client);
            if (amountToSend + minerFee > total) throw new ArgumentException(" AmountToSend + MinerFee should not be greater than the balance");

            Dictionary<OutPoint, double> UTXOS = informationSeeker.FindUtxo(senderPrivateKey, client, nbOfConfimationReq);
            var transaction = new Transaction();
            var senderScriptPubKey = senderPrivateKey.GetAddress().ScriptPubKey;
            var sortedDict = from entry in UTXOS orderby entry.Value descending select entry;
            decimal totalToSend = amountToSend + minerFee;

            int p = 0;
            decimal valueOfInputs = 0;
            foreach (var utxo in sortedDict)
            {
                transaction.Inputs.Add(new TxIn()
                {
                    PrevOut = utxo.Key
                });
                valueOfInputs += (decimal)utxo.Value;
                transaction.Inputs[p].ScriptSig = senderScriptPubKey;
                if (valueOfInputs > totalToSend) break;
                p++;                
            }

            TxOut destinationTxOut = new TxOut()
            {
                Value = new Money(amountToSend, MoneyUnit.BTC),
                ScriptPubKey = destinationAdress.ScriptPubKey
            };

            TxOut changeBackTxOut = new TxOut()
            {
                Value = new Money(valueOfInputs - totalToSend, MoneyUnit.BTC),
                ScriptPubKey = senderScriptPubKey
            };

            transaction.Outputs.Add(destinationTxOut);
            transaction.Outputs.Add(changeBackTxOut);
            transaction.Sign(senderPrivateKey, false);

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

