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

            var rnd = new RNGCryptoServiceProvider();
            int nonce = 19990300;


            int[] Test = new int[] {0,0,0,0,0,0};

            for (int i = 0; i < 1000000; i++)
            {
                List<int> dicesFromHash = HashManager.GetDicesFromHash("aerfsfrzsf", "efe,fesofes" + nonce.ToString());
                for (int p = 0; p < dicesFromHash.Count; p++)
                {
                    Test[dicesFromHash[p]]++;
                }

            }

            foreach (var item in Test)
            {
                Console.WriteLine(item);
            }
              Console.ReadKey();

        }
    }
}
