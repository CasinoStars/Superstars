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

namespace Superstars.Wallet
{
    public class TransactionMaker
    {
        /// <summary>
        /// Cette classe a la responsabilité de crée des transaction et de les envoyer
        /// </summary>
        /// <param name="key"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static Transaction MakeATransaction(BitcoinSecret senderPrivateKey, BitcoinAddress destinationAdress, int amountToSend, int minerFee, int nbOfConfimationReq, QBitNinjaClient client)
        {
            int total = informationSeeker.HowMuchCoinInWallet(senderPrivateKey, client);
            if (amountToSend + minerFee > total) throw new ArgumentException(" AmountToSend + MinerFee should not be greater than the balance");

            ICoin[] UTXOS = informationSeeker.FindUtxo(senderPrivateKey, client);
            var transaction = new Transaction();
            var senderScriptPubKey = senderPrivateKey.GetAddress().ScriptPubKey;
            var sortedDict = from entry in UTXOS orderby entry.Amount descending select entry;
            int totalToSend = amountToSend + minerFee;

            int p = 0;
            int valueOfInputs = 0;
            foreach (var utxo in sortedDict)
            {
                transaction.Inputs.Add(new TxIn()
                {
                    PrevOut = utxo.Outpoint
                });
                valueOfInputs += int.Parse(utxo.Amount.ToString());
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

        public static List<string> BroadCastTransaction(Transaction transaction, QBitNinjaClient client)
        {
            BroadcastResponse broadcastResponse = client.Broadcast(transaction).Result;

            List<string> broadCastResponseString = new List<string>();

            if (!broadcastResponse.Success)
            {
                broadCastResponseString.Add("ErrorCode: " + broadcastResponse.Error.ErrorCode);
                broadCastResponseString.Add("Error message: " + broadcastResponse.Error.Reason);
            }
            else
            {
                broadCastResponseString.Add("Success! You can check out the hash of the transaciton in any block explorer:");
                broadCastResponseString.Add(transaction.GetHash().ToString());
            }
            return broadCastResponseString;
        }       
    }
}

