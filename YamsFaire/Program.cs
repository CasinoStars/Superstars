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
            int nonce = 100000000;


            int[] Test = new int[] {0,0,0,0,0,0};

            for (int i = 0; i < 10000000; i++)
            {
                int dicesFromHash = HashManager.GetDicesFromHash("aerfsfrzsf", "efe,fesofes" + nonce.ToString());                
                Test[dicesFromHash-1]++;              
            }

            foreach (var item in Test)
            {
                Console.WriteLine(item);
            }
              Console.ReadKey();

        }
    }
}
