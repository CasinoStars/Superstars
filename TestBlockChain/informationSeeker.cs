using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.Wallet
{
    public class informationSeeker
    {
        /// <summary>
        /// cette classe a la responsabilité de trouver des information dans la blockchain
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        /// 


        public BitcoinSecret GetBitcoinSecretFromKey(Key key, Network network)
        {
            BitcoinSecret wallet = key.GetBitcoinSecret(network);
            return wallet;
        }

        public static List<GetTransactionResponse> SeekPendingTrx(BitcoinSecret privateKey, QBitNinjaClient client)
        {
            List<GetTransactionResponse> responses = SeekAllTransaction(privateKey, client);
            List<GetTransactionResponse> unconfirmedTrxs = new List<GetTransactionResponse>(); 

            foreach (var response in responses)
            {
                if (response.Block == null || response.Block.Confirmations < 6) unconfirmedTrxs.Add(response);
            }
            return unconfirmedTrxs;
        }

        public static List<GetTransactionResponse> SeekAllTransaction(BitcoinSecret key, QBitNinjaClient client)
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

        public static decimal HowMuchCoinInWallet(BitcoinSecret privateKey, QBitNinjaClient client)
        {
            List<GetTransactionResponse> responses = SeekAllTransaction(privateKey, client);
            Dictionary<OutPoint, double> UTXOS = FindUtxo(privateKey, client, 3);
            decimal total = 0;
            foreach (var item in UTXOS)
            {
                total += (decimal)item.Value;
            }
            return total;
        }

        public static List<string> GetPendingTrxWithAmntAndNbOfConf(BitcoinSecret privateKey,QBitNinjaClient client)
        {
            List<string> PendingTrxWithAmountAndNbOfConf = new List<string>();

           List<GetTransactionResponse> pendingTrx = SeekPendingTrx(privateKey, client);
            foreach (var trxResp in pendingTrx)
            {
                foreach (var input in trxResp.Transaction.Inputs)
                {
                    if (input.ScriptSig.GetSignerAddress(Network.TestNet) != privateKey.GetAddress())
                        foreach (var output in trxResp.Transaction.Outputs)
                        {
                            if (output.ScriptPubKey == privateKey.ScriptPubKey)
                            {
                                PendingTrxWithAmountAndNbOfConf.Add(trxResp.TransactionId.ToString());
                                PendingTrxWithAmountAndNbOfConf.Add("   "  + output.Value.ToString());
                                if (trxResp.Block == null) PendingTrxWithAmountAndNbOfConf.Add(" 0 ");

                                else
                                    PendingTrxWithAmountAndNbOfConf.Add(" " + trxResp.Block.Confirmations.ToString() + " ");
                            }
                        }
                }
            }
            return PendingTrxWithAmountAndNbOfConf;
        }
       


        public static Dictionary<OutPoint, double> FindUtxo(BitcoinSecret bitcoinPrivateKey, QBitNinjaClient client, int nbOfConfirmationReq)
        {


            List<GetTransactionResponse> responses = SeekAllTransaction(bitcoinPrivateKey, client);
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

            foreach (var trxResponse in responses)
            {
                foreach (var intput in trxResponse.Transaction.Inputs)
                {
                    prevouts.Add(intput.PrevOut);
                }
            }
            List<OutPoint> utxo = new List<OutPoint>();

            foreach (var trx in trxSignedWithOurSpk)
            {
                if (!prevouts.Contains(trx)) utxo.Add(trx);
            }

            Dictionary<OutPoint, double> UTXOs = new Dictionary<OutPoint, double>();

            for (int i = 0; i < utxo.Count; i++)
            {
                var trx = uint256.Parse(utxo[i].Hash.ToString());
                GetTransactionResponse trxResponse = client.GetTransaction(trx).Result;
                if (trxResponse.Block == null ||  trxResponse.Block.Confirmations < nbOfConfirmationReq) continue;
                double value = double.Parse(trxResponse.Transaction.Outputs[utxo[i].N].Value.ToString().Replace(".", ","));
                if(value != 0)
                UTXOs.Add(utxo[i], value);
            }

            List<GetTransactionResponse> pendingTrx = informationSeeker.SeekPendingTrx(bitcoinPrivateKey, client);


            foreach (var trx in pendingTrx)
            {
                foreach (var input in trx.Transaction.Inputs)
                {
                    if (input.ScriptSig.GetSignerAddress(Network.TestNet) == bitcoinPrivateKey.GetAddress())
                    {
                        for (int i = 0; i < trx.Transaction.Outputs.Count; i++)
                        {
                            if (trx.Transaction.Outputs[i].ScriptPubKey.GetDestinationAddress(Network.TestNet) == bitcoinPrivateKey.GetAddress())
                                if (!UTXOs.ContainsKey(new OutPoint(trx.TransactionId, i)))
                                    UTXOs.Add(new OutPoint(trx.TransactionId, i),(double)trx.Transaction.Outputs[i].Value.Satoshi/100000000);
                        }
                    }
                }
            }


            return UTXOs;
        }
    }
}
