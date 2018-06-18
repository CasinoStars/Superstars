using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace YamsFaire
{
    class Program
    {
        static void Main(string[] args)
        {
            Dices dices = new Dices();
            dices.rolleDices();

            //string ok = "oigroidjd";
         // Console.WriteLine(ok);

            var rnd = new RNGCryptoServiceProvider();
            int nonce = 300;
            //      byte[] ServeurSeed = System.Text.Encoding.ASCII.GetBytes("MA CHAINE");
            //     byte[] clientSeedWithNonce = System.Text.Encoding.ASCII.GetBytes("MA CHAINE" + nonce);


            //  for (int i = 0; i < length; i++)
            // {

            //            }

            int[] Test = new int[] {0,0,0,0,0,0};

            for (int i = 0; i < 100; i++)
            {
                List<int> dicesFromHash = HashManager.GetDicesFromHash("ok", "ok" + i.ToString());
                for (int p = 0; p < 6; p++)
                {
                    Test[dicesFromHash[p]-1]++;
                }
                //Console.WriteLine(dicesFromHash.Count + "   Nonce "  + i);

             //   if (dicesFromHash.Count < 18) throw new ArgumentException(" Length to short");
            }


            foreach (var item in Test)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();

        }
    }
}
