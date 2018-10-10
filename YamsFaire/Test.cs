using System;
using System.Collections.Generic;

namespace Superstars.YamsFair
{
    class Test
    {
        public static void TestRetrieveHash(SeedManager seedManager)
        {
            bool work = true;
            for (int i = 0; i < 100; i++)
            {
                  string test = seedManager.CryptedServerSeed;
                  seedManager.NewSeed();
                if (!(test == HashManager.getHashSha512(seedManager.PreviousUncryptedServerSeed)))
                {
                    work = false;
                    break;
                }                
            }
            Console.WriteLine((work) ? "Work" : "Cant retrieve hash...");
        }

        /// <summary>
        /// Run the methode GetDicesFromHash the number of time ask by nbOfTest and write the percentage
        /// where each dices was found  
        /// </summary>
        /// <param name="clientSeed"></param>
        /// <param name="serverSeed"></param>
        /// <param name="nonce"></param>
        /// <param name="nbOfTest"></param>
        /// <returns></returns>
        public static void TestRandomness(string clientSeed, string serverSeed,int nbOfTest)
        {            
                double[] test = new double[] { 0, 0, 0, 0, 0, 0 };
                int nonce = 0;

                for (int i = 0; i < nbOfTest; i++)
                {
                    int dicesFromHash = HashManager.GetDiceFromHash(clientSeed, serverSeed, nonce);
                    test[dicesFromHash - 1]++;
                nonce++;
                }
                for (int i = 0; i < test.Length; i++)
                {
                    test[i] = (test[i] / (double)nbOfTest) * 100;
                }
                List<string> result = new List<string>();

                for (int i = 0; i < test.Length; i++)
                {
                    result.Add(test[i].ToString() + " Dice of " + (i + 1).ToString());
                }

            for (int i = 0; i < test.Length; i++)
            {
                Console.WriteLine(test[i].ToString() + " % Dices of " + (i + 1).ToString());
            }

        }
    }
}
