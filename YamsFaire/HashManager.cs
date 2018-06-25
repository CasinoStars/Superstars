using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;


namespace YamsFaire
{
    class HashManager
    {


        /// <summary>
        /// Get SHA512 hash from string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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


        /// <summary>
        /// hash serveurSeed + clientSeed + nonce and return 1 dices from this hash  
        /// </summary>
        /// <param name="serverSeed"></param>
        /// <param name="clientSeedWithNonce"></param>
        /// <returns></returns>
        public static int GetDicesFromHash(string serverSeed, string clientSeed, int nonce)
        {
            string clientSeedWithNonce = clientSeed + nonce.ToString();
            string hash = getHashSha512(serverSeed + clientSeedWithNonce);
            int result = 100;
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
            if (i == 0) return GetDicesFromHash(serverSeed, clientSeed, nonce+= 1);

            return result; 
         }

        /// <summary>
        /// Run the methode GetDicesFromHash the number of time ask by nbOfTest and return an array with the percentage
        /// where each dices was found  
        /// </summary>
        /// <param name="clientSeed"></param>
        /// <param name="serverSeed"></param>
        /// <param name="nonce"></param>
        /// <param name="nbOfTest"></param>
        /// <returns></returns>
        public static List<string> Test(string clientSeed, string serverSeed,int nonce, int nbOfTest)
        {
            double[] test = new double[] { 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < nbOfTest; i++)
            {
                int dicesFromHash = HashManager.GetDicesFromHash(clientSeed, serverSeed,nonce);
                test[dicesFromHash - 1]++;
            }
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = (test[i] / (double)nbOfTest) * 100;
            }
            List<string> result = new List<string>();

            for (int i = 0; i < test.Length; i ++)
            {
                result.Add(test[i].ToString() + " Dice of " + (i+1).ToString());
            } 
            return result;
        }
    }
}
