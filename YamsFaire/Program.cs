using System;
using System.Security.Cryptography;
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
            SHA512 sha512 = new SHA512Managed();
                Console.WriteLine(sha256.Hash);
            sha512.ComputeHash(ok);

            foreach (var dice in dices.dices)
            {
                Console.Write(dice + " ");
            }
            Console.ReadKey();
        }
    }
}
