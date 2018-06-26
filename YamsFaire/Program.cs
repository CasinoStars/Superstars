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


            int nonce = 50000000;
            int nbOfTest = 10000;

            Random random = new Random();

            string serverSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            string clientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            string CryptedServerSeed = HashManager.getHashSha512(serverSeed);


            Console.WriteLine(serverSeed);

            Console.WriteLine(CryptedServerSeed.Length);


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
