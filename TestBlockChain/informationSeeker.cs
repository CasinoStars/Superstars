using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System.Collections.Generic;
using System.Linq;

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
        /// <summary>
        /// Get the pending transactino linkind to a wallet address
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="client"></param>
        /// <returns></returns>
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

        /// <summary>
        /// get all transaction associate to a wallet address
        /// </summary>
        /// <param name="key"></param>
        /// <param name="client"></param>
        /// <returns></returns>
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

        /// <summary>
        /// return the amount of spendable coin in the wallet
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static int HowMuchCoinInWallet(BitcoinSecret privateKey, QBitNinjaClient client)
        {
            ICoin[] Coins = FindUtxo(privateKey, client);
            int total = 0;
            foreach (var coin in Coins)
            {
                Money amount = (Money) coin.Amount;
                total += (int)amount.Satoshi;
            }
            return total/100;
        }
        /// <summary>
        /// Get Pending Transactin ID with Amount of confirmation and the Value of the coin that the wallet with receive 
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="client"></param>
        /// <returns></returns>

        /// <summary>
        /// Get all the Unspend transaction outpoint associated to an address
        /// </summary>
        /// <param name="bitcoinPrivateKey"></param>
        /// <param name="client"></param>
        /// <param name="nbOfConfirmationReq"></param>
        /// <returns></returns>
        public static ICoin[] FindUtxo(BitcoinSecret bitcoinPrivateKey, QBitNinjaClient client)
        {
            var coins = client.GetBalance(bitcoinPrivateKey.GetAddress(), true).Result.Operations.SelectMany(op => op.ReceivedCoins).ToArray();
            return coins;
        }
    }
}
