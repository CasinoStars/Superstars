using System;
using System.Threading.Tasks;
using NBitcoin;
using NBitcoin.RPC;
using Newtonsoft.Json.Linq;
using QBitNinja.Client;

namespace Superstars.Wallet
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Main().Wait();
        }

        static async Task Main()
        {
            //  Console.WriteLine("test");

            var network = Network.TestNet;
            var client = new QBitNinjaClient(Network.TestNet);
            // BitcoinSecret key = new Key().GetBitcoinSecret(Network.TestNet);
            var btcS = new BitcoinSecret("cTSNviQWYnSDZKHvkjwE2a7sFW47sNoGhR8wjqVPb6RbwqH1pzup");
            var btcS2 = new BitcoinSecret("cP8jukfzUjzQonsfG4ySwkJF1xbpyn6EPhNhbD4yK8ZR2529cbzm");

            //  static HttpClient client = new HttpClient();
            var coinInWallet2 = await informationSeeker.SeekPendingTrx(btcS, client);



            Console.WriteLine("testnumero2");


            //for (int i = 0; i < 100000; i++)
            //{
            //    var tktmagl = new Key();

            //    if(Validator.IsValidAddress(tktmagl.PubKey.GetAddress(Network.TestNet).ToString()) == true) Console.WriteLine("TRUE");
            //    else { throw new Exception("FASLE"); }
            //}




            //bool hugo = Validator.IsValidAddress("mzRnZHJodRUmE6cSPvGrhtcsgvhdVFYroa");
            //bool loiseau = Validator.IsValidAddress("mh2e7YHio7fTjLXHZ3KRXDfU52RbwQbhtK");
            //bool marceau = Validator.IsValidAddress("21232e7YHio7fTjLXHZ3KRXDfU52RbwQbhtK");

            Console.ReadKey();
        }
    }
}