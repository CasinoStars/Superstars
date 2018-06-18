using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;


namespace YamsFaire
{
    class HashManager
    {
        public static string getHashSha512(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA512Managed hashstring = new SHA512Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public static List<int> GetDicesFromHash(string cryptedServeurSeed, string clientSeedWithNonce)
        {
            //string[] results = new string[3];
            //SHA512Managed hashManager = new SHA512Managed();
            //var concatened = new byte[cryptedServeurSeed.Length + clientSeedWithNonce.Length];
            //cryptedServeurSeed.CopyTo(concatened, 0);
            //clientSeedWithNonce.CopyTo(concatened, cryptedServeurSeed.Length);
            string hash = getHashSha512(cryptedServeurSeed + clientSeedWithNonce);

            // byte[] fromResult = new byte[10];
            // int[] decimalResults = new int[3];
            int i = 0;
            string[] results = new string[hash.Length/5];
            int z = 0;
            for (int p = 0; p < hash.Length/5; p++)
            {
                z += 5;
                while (i < z)
                {
                    results[p] += hash[i];
                    i++;
                }
            }

         //   List<int> IntFromResults = new List<int>();
            List<int> dicesFromHash = new List<int>();


            foreach (var item in results)
            {
                // if(int.Parse(item, System.Globalization.NumberStyles.HexNumber) < 59999)
                if (int.Parse(item, System.Globalization.NumberStyles.HexNumber) % 6000 / 100 < 10) dicesFromHash.Add(1);
                if (int.Parse(item, System.Globalization.NumberStyles.HexNumber) % 6000 / 100 < 20) dicesFromHash.Add(2);
                if (int.Parse(item, System.Globalization.NumberStyles.HexNumber) % 6000 / 100 < 30) dicesFromHash.Add(3);
                if (int.Parse(item, System.Globalization.NumberStyles.HexNumber) % 6000 / 100 < 40) dicesFromHash.Add(4);
                if (int.Parse(item, System.Globalization.NumberStyles.HexNumber) % 6000 / 100 < 50) dicesFromHash.Add(5);
                if (int.Parse(item, System.Globalization.NumberStyles.HexNumber) % 6000 / 100 < 60) dicesFromHash.Add(6);


            }

            foreach (var item in dicesFromHash)
            {
                Console.WriteLine(item);
            }


            //foreach (var item in IntFromResults)
            //{
            //    if (item < 10000) dicesFromHash.Add(1);
            //    if (item < 20000) dicesFromHash.Add(2);
            //    if (item < 30000) dicesFromHash.Add(3);
            //    if (item < 40000) dicesFromHash.Add(4);
            //    if (item < 50000) dicesFromHash.Add(5);
            //    if (item < 60000) dicesFromHash.Add(6);
            //}
          //  if (dicesFromHash.Count < 15) throw new ArgumentException(" Length to short");

            return dicesFromHash; 
         }


        public static void get ()
        {

        }

    }
}
