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

        public static void Hash(string serverSeed, string clientSeed, int nonce)
        {
            string[] results = new string[3];
            string hashResult = getHashSha512(serverSeed + clientSeed + nonce.ToString());
            string fromResult = "";
            for (int p = 0; p < results.Length; p++)
            {
                int z = 0;
                fromResult = "";
                for (int i = 0; i < 6; i++)
                {
                    if (p == 1) z = 6;
                    if (p == 2) z = 12;
                    fromResult += hashResult[i+z];
                }
                results[p] = fromResult;
            }

            int[] decimalResults = new int[3];
            for (int i = 0; i < results.Length; i++)
            {
                decimalResults[i] = int.Parse(results[i], System.Globalization.NumberStyles.HexNumber);
                string test = decimalResults[i].ToString();
                if (test.Length < 6) throw new Exception("Length to short");
            }

            foreach (var result in decimalResults)
            {
                Console.WriteLine(result);
            }
        }
    }
}
