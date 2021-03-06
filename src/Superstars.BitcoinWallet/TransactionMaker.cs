﻿using System;
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

namespace bitcointest
{
    class TransactionMaker
    {
        public static List<GetTransactionResponse> FindCoinInAWallet(BitcoinSecret key, QBitNinjaClient client)
        {
            List<GetTransactionResponse> transactionsResponses = new List<GetTransactionResponse>();
            var historyTransaction = client.GetBalance(key);
            int i = 0;

            foreach (var tx in historyTransaction.Result.Operations)
            {
                transactionsResponses.Add(client.GetTransaction(tx.TransactionId).Result);
                i++;
            }
            return transactionsResponses;
        }

        public static Dictionary<OutPoint, double> FindUtxo(List<GetTransactionResponse> responses, BitcoinSecret bitcoinPrivateKey, QBitNinjaClient client, int nbOfConfirmationReq)
        {
            List<OutPoint> trxSignedWithOurSpk = new List<OutPoint>();

            for (int i = 0; i < responses.Count; i++)
            {
                var receivedCoins = responses[i].ReceivedCoins;
                OutPoint outPointToSpend = null;
                foreach (var coin in receivedCoins)
                {
                    if (coin.TxOut.ScriptPubKey == bitcoinPrivateKey.ScriptPubKey)
                    {
                        trxSignedWithOurSpk.Add(coin.Outpoint);
                        outPointToSpend = coin.Outpoint;

                    }
                }
            }
            List<GetTransactionResponse> ResponseSignWithOurPrivateKey = new List<GetTransactionResponse>();
            List<OutPoint> prevouts = new List<OutPoint>();


            foreach (var item in responses)
            {
                foreach (var intput in item.Transaction.Inputs)
                {
                    prevouts.Add(intput.PrevOut);
                }
            }
            List<OutPoint> utxo = new List<OutPoint>();

            foreach (var item in trxSignedWithOurSpk)
            {
                if (!prevouts.Contains(item)) utxo.Add(item);
            }

            Dictionary<OutPoint, double> UTXOs = new Dictionary<OutPoint, double>();

            for (int i = 0; i < utxo.Count; i++)
            {
                var trx = uint256.Parse(utxo[i].Hash.ToString());
                GetTransactionResponse trxResponse = client.GetTransaction(trx).Result;
                if (trxResponse.Block.Confirmations < nbOfConfirmationReq) continue;
               // Console.WriteLine(trxResponse.TransactionId + " Transaction ID" + trxResponse.Block.Confirmations + "  Confirmation");
                double value = double.Parse(trxResponse.Transaction.Outputs[utxo[i].N].Value.ToString().Replace(".", ","));
                UTXOs.Add(utxo[i], value);
            }
            return UTXOs;
        }
        public static void MakeATransaction(BitcoinSecret privateKey, BitcoinAddress destinationAdress, decimal amountToSend, decimal minerFee, int nbOfConfimationReq)
        {
            var client = new QBitNinjaClient(Network.TestNet);
            List<GetTransactionResponse> responses = FindCoinInAWallet(privateKey, client);
            Dictionary<OutPoint, double> UTXOS = FindUtxo(responses, privateKey, client, nbOfConfimationReq);
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

