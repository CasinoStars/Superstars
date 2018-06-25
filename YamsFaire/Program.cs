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

            string clientSeed = "test";
            string serverSeed = "test";
            int nonce = 50000000;
            int nbOfTest = 10000;


            Dices dices = new Dices();



            List<string> test = HashManager.Test(clientSeed, serverSeed, nonce, 10000000);

            foreach (var item in test)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
