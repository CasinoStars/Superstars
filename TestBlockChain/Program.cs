using System;
using NBitcoin;
using QBitNinja.Client;
using System.Linq;


namespace Superstars.Wallet
{
    class Program
    {
        static void Main(string[] args)
        {

            //  Console.WriteLine("test");

            var network = Network.TestNet;
            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            // BitcoinSecret key = new Key().GetBitcoinSecret(Network.TestNet);
            BitcoinSecret btcS = new BitcoinSecret("cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup");
            BitcoinSecret btcS2 = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm");

            //  decimal coinInWallet2 = informationSeeker.HowMuchCoinInWallet(btcS2, client);

            //  Transaction trxx = TransactionMaker.MakeATransaction(btcS2, btcS.GetAddress(), (decimal)coinInWallet2-(decimal)0.05,(decimal)0.05, 6,client);
            //  TransactionMaker.BroadCastTransaction(trxx,client);

            //  decimal coinInWallet1 = informationSeeker.HowMuchCoinInWallet(btcS, client);

            //  Transaction trxx2 = TransactionMaker.MakeATransaction(btcS, btcS2.GetAddress(), (decimal)coinInWallet1-(decimal)0.05, (decimal)0.05, 6, client);
            //  TransactionMaker.BroadCastTransaction(trxx2, client);

            ////  List<GetTransactionResponse> pendingTrx = informationSeeker.SeekPendingTrx(btcS, client);



            //  Dictionary<OutPoint, double> utxos = informationSeeker.FindUtxo(btcS, client, 0);

            //  Dictionary<OutPoint, double> utxos2 = informationSeeker.FindUtxo(btcS2, client, 0);


            //  List<string> PendingTrxWithAmountAndNbOfConf = informationSeeker.GetPendingTrxWithAmntAndNbOfConf(btcS,client);

            //  foreach (var item in PendingTrxWithAmountAndNbOfConf)
            //  {
            //      Console.WriteLine(item + "  TEST PENDING TRX WITH CONF AND AMOUNT");
            //  }

            //  double totalUtxo = 0;
            //  double totalUtxo2 = 0;

            //  foreach (var item in utxos)
            //  {
            //      Console.WriteLine(item.Key +  " WALLET 1" );
            //      Console.WriteLine(item.Value + "  WALLET 1");
            //      totalUtxo += item.Value;
            //  }

            //  foreach (var item in utxos2)
            //  {
            //      Console.WriteLine(item.Key +  "  WALLET 2");
            //      Console.WriteLine(item.Value + " WALLET 2");
            //      totalUtxo2 += item.Value;
            //  }


            //  decimal coinInWallet = informationSeeker.HowMuchCoinInWallet(btcS2, client);

            //  Console.WriteLine(totalUtxo2 + " Coin in wallet WALLET 2");
            //  Console.WriteLine(totalUtxo + " Total  WALLET1");

            var coins = client.GetBalance(btcS2.GetAddress(), true).Result.Operations.SelectMany(op => op.ReceivedCoins).ToArray();

            foreach (var item in coins)
            {
                Console.WriteLine(item.Amount);
                Console.WriteLine(item.Outpoint);
            }

            Console.ReadKey();
        }
    }
}
