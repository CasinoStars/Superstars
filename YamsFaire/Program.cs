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
  


            Console.WriteLine(HashManager.getHashSha512("e2df737b43ae8b1b2e65baa774da87c58aa621aef3cdb6e453297300366e6c8ffc7e2c5e5fd1f85e09fc8ac48096c25bc5bf828cd0cb0babc06095b769a678a5"));

            Console.ReadKey();
        }

        
        
    }
}
