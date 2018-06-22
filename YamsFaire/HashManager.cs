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

        public static int GetDicesFromHash(string cryptedServeurSeed, string clientSeedWithNonce)
        {
            //string[] results = new string[3];
            //SHA512Managed hashManager = new SHA512Managed();
            //var concatened = new byte[cryptedServeurSeed.Length + clientSeedWithNonce.Length];
            //cryptedServeurSeed.CopyTo(concatened, 0);
            //clientSeedWithNonce.CopyTo(concatened, cryptedServeurSeed.Length);
            string hash = getHashSha512(cryptedServeurSeed + clientSeedWithNonce);
            int result = 100;
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
                int candidate = int.Parse(item, System.Globalization.NumberStyles.HexNumber);
                if (int.Parse(item, System.Globalization.NumberStyles.HexNumber) > 609999) continue;
                else
                {
                    candidate = candidate / 10000;
                    if (candidate > 60) throw new Exception("value must be lower then 60");
                    if (candidate < 1) continue;
                    if (candidate  < 1) throw new Exception("Error"); 
                    if ((candidate ) < 11 && candidate > 0) result = 1;
                    else if ((candidate) < 21) result = 2;
                    else if ((candidate) < 31) result = 3;
                    else if ((candidate) < 41) result = 4;
                    else if ((candidate) < 51) result = 5;
                    else if ((candidate) < 61) result = 6;
                    break;
                }                
            }
            if (i == 0) return GetDicesFromHash(cryptedServeurSeed, clientSeedWithNonce += 1);

            return result; 
         }
    }
}
