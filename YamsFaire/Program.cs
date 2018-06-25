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
            int nbOfTest = 10000;

          List<string> test = HashManager.Test("okok", "okok", 10, 10000);

            foreach (var item in test)
            {
                Console.WriteLine(item);
            }

              Console.ReadKey();
        }
    }
}
