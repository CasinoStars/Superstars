using System;
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

            string ok = "oigroidjd";
            ok = HashManager.getHashSha256(ok);
            Console.WriteLine(ok);

            var rnd = new RNGCryptoServiceProvider();
            var b = new byte[16];

 


            foreach (var dice in dices.dices)
            {
                Console.Write(dice + " ");
            }
            Console.ReadKey();
        }
    }
}
