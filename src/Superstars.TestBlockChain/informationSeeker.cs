using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;

namespace Superstars.Wallet
{
    public class informationSeeker
    {
        /// <summary>
        ///     cette classe a la responsabilité de trouver des information dans la blockchain
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public BitcoinSecret GetBitcoinSecretFromKey(Key key, Network network)
        {
            var wallet = key.GetBitcoinSecret(network);
            return wallet;
        }

        /// <summary>
        ///     Get the pending transactino linkind to a wallet address
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task<List<string>> SeekTrx(BitcoinSecret privateKey, QBitNinjaClient client, int maxConfirmation)
        {
            var unconfirmedTrxs = new List<string>();
            var historyTransaction = await client.GetBalance(privateKey);
            var transactionsResponses = new List<GetTransactionResponse>();
            bool isSpend;
            string info;

            foreach (var item in historyTransaction.Operations)
            {
                //Money
                decimal d = item.Amount.ToDecimal(MoneyUnit.BTC);
                if (item.Confirmations < maxConfirmation)
                {

                    isSpend = ( d < 0);
                
                   
                    if(isSpend)
                    {
                        info = d.ToString();
                    }
                    else
                    {
                        info = d.ToString();
                    }
                    unconfirmedTrxs.Add(item.TransactionId.ToString() + " " + info + "BTC " + item.Confirmations.ToString() + " confirmations");                  
                }
            }

            return unconfirmedTrxs;
        }

        ///if (spend and received coin ) spendcoin - received coin - fee = montant de la transaction envoyer  

        ///if only received just add the amount of received coin 

        ///if received false and spend true , value il spend

        /// <summary>
        ///     get all transaction associate to a wallet address
        /// </summary>
        /// <param name="key"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static List<GetTransactionResponse> SeekAllTransaction(BitcoinSecret key, QBitNinjaClient client)
        {
            var transactionsResponses = new List<GetTransactionResponse>();
            var historyTransaction = client.GetBalance(key);
            foreach (var tx in historyTransaction.Result.Operations)
            {
                transactionsResponses.Add(client.GetTransaction(tx.TransactionId).Result);
            }

            return transactionsResponses;
        }

        /// <summary>
        ///     return the amount of spendable coin in the wallet
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static async Task<int> HowMuchCoinInWallet(BitcoinSecret privateKey, QBitNinjaClient client)
        {
            var Coins = await FindUtxo(privateKey, client);
            var total = 0;
            foreach (var coin in Coins)
            {
                var amount = (Money) coin.Amount;
                total += (int) amount.Satoshi;
            }

            return total / 100;
        }

        /// <summary>
        ///     Get Pending Transactin ID with Amount of confirmation and the Value of the coin that the wallet with receive
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        /// <summary>
        ///     Get all the Unspend transaction outpoint associated to an address
        /// </summary>
        /// <param name="bitcoinPrivateKey"></param>
        /// <param name="client"></param>
        /// <param name="nbOfConfirmationReq"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<ICoin>> FindUtxo(BitcoinSecret bitcoinPrivateKey, QBitNinjaClient client)
        {
            var coinsTask = new BalanceModel();

            try
            {
                coinsTask = await client.GetBalance(bitcoinPrivateKey.GetAddress(), true);
            } catch
            {
                coinsTask = await new QBitNinjaClient(Network.TestNet).GetBalance(bitcoinPrivateKey.GetAddress(), true);
            }
 

            var coins = coinsTask;
            return coins.Operations.SelectMany(op => op.ReceivedCoins).ToArray();
        }
    }
}