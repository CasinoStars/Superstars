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
            HMACSHA512 hashstring = new HMACSHA512();
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

            List<int> IntFromResults = new List<int>();
            List<int> dicesFromHash = new List<int>();


            foreach (var item in results)
            {
                if (int.Parse(item, System.Globalization.NumberStyles.HexNumber) > 609999) continue;

                IntFromResults.Add(int.Parse(item, System.Globalization.NumberStyles.HexNumber));
                
            }

            foreach (var value in IntFromResults)
            {
                if (value / 10000 < 1) continue;

                Console.WriteLine(value / 100000);
                if ((value    / 10000) < 11) dicesFromHash.Add(0);
               else if ((value  / 10000) < 21) dicesFromHash.Add(1);
               else if ((value  / 10000) < 31) dicesFromHash.Add(2);
               else if ((value  / 10000) < 41) dicesFromHash.Add(3);
               else if ((value  / 10000) < 51) dicesFromHash.Add(4);
               else if ((value  / 10000) < 61) dicesFromHash.Add(5);
               if (value/10000 > 60) throw new Exception("value must be lower then 60");
               if (value/10000 < 1) throw new Exception("value must at least 1");

            }
            return dicesFromHash; 
         }
    }
}
