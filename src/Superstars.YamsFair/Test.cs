using System;
using System.Collections.Generic;

namespace Superstars.YamsFair
{
    internal class Test
    {
        public static void TestRetrieveHash(SeedManager seedManager)
        {
            var work = true;
            for (var i = 0; i < 100; i++)
            {
                var test = seedManager.CryptedServerSeed;
                seedManager.NewSeed();
                if (!(test == HashManager.getHashSha512(seedManager.PreviousUncryptedServerSeed)))
                {
                    work = false;
                    break;
                }
            }

            Console.WriteLine(work ? "Work" : "Cant retrieve hash...");
        }

        /// <summary>
        ///     Run the methode GetDicesFromHash the number of time ask by nbOfTest and write the percentage
        ///     where each dices was found
        /// </summary>
        /// <param name="clientSeed"></param>
        /// <param name="serverSeed"></param>
        /// <param name="nonce"></param>
        /// <param name="nbOfTest"></param>
        /// <returns></returns>
        public static void TestRandomness(string clientSeed, string serverSeed, int nbOfTest, int maxRand)
        {

            double[] test = new double[maxRand];

        var nonce = 0;

            for (var i = 0; i < nbOfTest; i++)
            {
                var dicesFromHash = HashManager.GetDiceFromHash(clientSeed, serverSeed, nonce, maxRand);
                test[dicesFromHash - 1]++;
                nonce++;
            }

            for (var i = 0; i < test.Length; i++) test[i] = test[i] / nbOfTest * 100;
            var result = new List<string>();

            for (var i = 0; i < test.Length; i++) result.Add(test[i] + " Dice of " + (i + 1));

            for (var i = 0; i < test.Length; i++) Console.WriteLine(test[i] + " % Dices of " + (i + 1));
        }
    }
}