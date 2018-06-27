using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.YamsFair
{
    class FairDices
    {

        int[] _dices;
        SeedManager _seedmanager = new SeedManager();

    public FairDices()
        {
            _dices = new int[5] { 1, 2, 3, 4, 5 };
        }

        public void RolleDices(string serveurSeed, string clientSeed,int nonce)
        {
            int random = 0;
            int i = 0;
            foreach (var dice in dices)
            {
                random = HashManager.GetDiceFromHash(_seedmanager.CryptedServerSeed, _seedmanager.ClientSeed, nonce);
                nonce++;
                _dices[i] = random;
                i++;
            }
        }
        public int[] dices { get { return _dices; } }

    }
}
