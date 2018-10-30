using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Superstars.YamsFair
{
    public class HashManager
    {
        /// <summary>
        ///     Get SHA512 hash from string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string getHashSha512(string text)
        {
            var bytes = Encoding.ASCII.GetBytes(text);
            HashAlgorithm hashstring = SHA512.Create();
            var hash = hashstring.ComputeHash(bytes);
            var hashString = string.Empty;
            foreach (var x in hash) hashString += string.Format("{0:x2}", x);

            return hashString;
        }


        /// <summary>
        ///     hash serveurSeed + clientSeed + nonce and return 1 dices from this hash
        /// </summary>
        /// <param name="serverSeed"></param>
        /// <param name="clientSeedWithNonce"></param>
        /// <returns></returns>
        public static int GetDiceFromHash(string serverSeed, string clientSeed, int nonce)
        {
            var clientSeedWithNonce = clientSeed + nonce;
            var hash = getHashSha512(serverSeed + clientSeedWithNonce);
            var result = 0;
            var i = 0;
            var results = new string[hash.Length / 5];
            var z = 0;
            for (var p = 0; p < hash.Length / 5; p++)
            {
                z += 5;
                while (i < z)
                {
                    results[p] += hash[i];
                    i++;
                }
            }

            foreach (var item in results)
            {
                var candidate = int.Parse(item, NumberStyles.HexNumber);
                if (int.Parse(item, NumberStyles.HexNumber) > 609999)
                {
                }
                else
                {
                    candidate = candidate / 10000;
                    if (candidate > 60) throw new Exception("value must be lower then 60");
                    if (candidate < 1) continue;
                    if (candidate < 1) throw new Exception("candidate must greater then 1");
                    if (candidate < 11 && candidate > 0) result = 1;

                    else if (candidate < 21) result = 2;
                    else if (candidate < 31) result = 3;
                    else if (candidate < 41) result = 4;
                    else if (candidate < 51) result = 5;
                    else if (candidate < 61) result = 6;
                    break;
                }
            }

            if (i == 0) return GetDiceFromHash(serverSeed, clientSeed, nonce += 1);
            return result;
        }

        /// <summary>
        ///     retrieve dices from seeds
        /// </summary>
        /// <param name="serveurSeed"></param>
        /// <param name="clientSeed"></param>
        /// <param name="nbOfDices"></param>
        /// <returns></returns>
        public static List<int> RetriveDicesFromSeeds(string serveurSeed, string clientSeed, int nbOfDices)
        {
            var retriveDicesFromSeeds = new List<int>();
            for (var i = 0; i < nbOfDices; i++) retriveDicesFromSeeds.Add(GetDiceFromHash(serveurSeed, clientSeed, i));

            return retriveDicesFromSeeds;
        }
    }
}