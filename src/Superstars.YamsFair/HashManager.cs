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
        public static int GetDiceFromHash(string serverSeed, string clientSeed, int nonce, int maxRand)
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
                if (int.Parse(item, NumberStyles.HexNumber) > /*550000 */(maxRand)*10000)
                {
                }
                else
                {
                    candidate = candidate / 10000;
                    if (candidate > maxRand) throw new Exception("value must be lower or equal  maxRand");
                    if (candidate < 1) continue;

                    for (int index = 0; index < maxRand; index++)
                    {
                        if (candidate <= index) {
                            result = index;
                            break;
                                }
                    }
                    break;
                }
            }
            if (i == 0) return GetDiceFromHash(serverSeed, clientSeed, nonce += 1, maxRand);
            return result+1;
        }

        /// <summary>
        ///     retrieve dices from seeds
        /// </summary>
        /// <param name="serveurSeed"></param>
        /// <param name="clientSeed"></param>
        /// <param name="nbOfDices"></param>
        /// <returns></returns>
        public static List<int> RetriveDicesFromSeeds(string serveurSeed, string clientSeed, int nbOfDices, int maxRand)
        {
            var retriveDicesFromSeeds = new List<int>();
            for (var i = 0; i < nbOfDices; i++) retriveDicesFromSeeds.Add(GetDiceFromHash(serveurSeed, clientSeed, i, maxRand));

            return retriveDicesFromSeeds;
        }
    }
}