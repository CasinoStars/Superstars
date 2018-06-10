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

            //string ok = "oigroidjd";
         // Console.WriteLine(ok);

            var rnd = new RNGCryptoServiceProvider();
            var b = new byte[16];


            HashManager.Hash("ok", "ok", 6);

            for (int i = 0; i < 1000; i++)
            {
                HashManager.Hash("ok", "ok", i);
            }


            Console.ReadKey();

        }
    }
}
