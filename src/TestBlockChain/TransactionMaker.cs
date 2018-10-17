using System;
using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System.Collections.Generic;
using System.Linq;

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
            Money amount = 0;

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
                amount = (Money)utxo.Amount;
                valueOfInputs += (int)amount.Satoshi;
                transaction.Inputs[p].ScriptSig = senderScriptPubKey;
                if (valueOfInputs > totalToSend*100) break;
                p++;                
            }

            TxOut destinationTxOut = new TxOut()
            {
                Value = new Money(amountToSend*100, MoneyUnit.Satoshi),
                ScriptPubKey = destinationAdress.ScriptPubKey
            };

            TxOut changeBackTxOut = new TxOut()
            {
                Value = new Money(valueOfInputs - totalToSend*100, MoneyUnit.Satoshi),
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

