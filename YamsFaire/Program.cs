using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace Superstars.YamsFair
{
    class Program
    {
        static void Main(string[] args)
        {


            int nonce = 50000000;
            int nbOfTest = 10000;

            SeedManager seedManager = new SeedManager();
            Test.TestRetrieveHash(seedManager);

            Console.ReadKey();
        }
    }
}
