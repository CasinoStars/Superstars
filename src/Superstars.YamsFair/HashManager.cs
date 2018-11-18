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
            var results = new string[hash.Length / 4];
            var z = 0;
            for (var p = 0; p < hash.Length / 4; p++)
            {
                z += 4;
                while (i < z)
                {
                    results[p] += hash[i];
                    i++;
                }
            }

            foreach (var item in results)
            {
                var candidate = int.Parse(item, NumberStyles.HexNumber);
                if (int.Parse(item, NumberStyles.HexNumber) >=/*550000 */(maxRand)*1000)
                {
                }
                else
                {

                    for (int y = 1; y <= maxRand; y++)
                    {
                        if(candidate <= (y*1000)-1)
                        {
                            result = y;
                            break;
                        }
                    }
                    // result = (candidate / 1000);
                    //if (candidate <= 999)
                    //{
                    //    result = 1;
                    //    break;
                    //}
                    //if (candidate <= 1999)
                    //{
                    //    result = 2;
                    //    break;
                    //}
                    //if (candidate <= 2999)
                    //{
                    //    result = 3;
                    //    break;
                    //}
                    //if (candidate <= 3999)
                    //{
                    //    result = 4;
                    //    break;
                    //}
                    //if (candidate <= 4999)
                    //{
                    //    result = 5;
                    //    break;
                    //}
                    //if (candidate <= 5999)
                    //{
                    //    result = 6;
                    //    break;
                    //}

                    if (result > maxRand) throw new Exception("value must be lower or equal  maxRand");
                    //if (candidate < 1) continue;
                    //if (candidate < 1) throw new Exception("candidate must greater then 1");

                    //if (candidate <= 1 && candidate > 0) result = 1;
                    //else if (candidate <= 2) result = 2;
                    //else if (candidate <= 3) result = 3;
                    //else if (candidate <= 4) result = 4;
                    //else if (candidate <= 5) result = 5;
                    //else if (candidate <= 6) result = 6;
                    //for (int index = 0; index < maxRand; index++)
                    //{
                    //    if (candidate <= index) {
                    //        result = index;
                    //        break;
                    //            }

                    //}

                    
                }
            }
            if (result == 0) return GetDiceFromHash(serverSeed, clientSeed, nonce += 1, maxRand);
            return result;
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