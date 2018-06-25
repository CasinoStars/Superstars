using System;
using System.Collections.Generic;
using System.Text;

namespace YamsFaire
{
    class Dices
    {

        public int[] dices { get { return _dices; } }
        int[] _dices;

        public Dices()
        {
            _dices = new int[5] { 1, 2, 3, 4, 5 };
        }


        public void rolleDices(string serveurSeed, string clientSeed,int nonce)
        {
            int random = 0;
            int i = 0;
            foreach (var dice in dices)
            {
                random = HashManager.GetDicesFromHash(serveurSeed, clientSeed, nonce);
                _dices[i] = random;
                i++;
            }
        }
    }
}
