using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.YamsFair
{
    class SeedManager
    {

        Random random = new Random();
        string _previousUncryptedServerSeed;
        string _uncryptedServerSeed;
        string _clientSeed;
        string _CryptedServerSeed;
        int _nonce;


        public SeedManager()
        {
            _uncryptedServerSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _clientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _CryptedServerSeed = HashManager.getHashSha512(_uncryptedServerSeed);
            _nonce = 0;
        }

        public string PreviousUncryptedServerSeed
        {
            get { return _previousUncryptedServerSeed; }
        }

        public string CryptedServerSeed
        {
            get { return _CryptedServerSeed; }
        }
        public string ClientSeed
        {
            get { return _clientSeed; }
        }

        public int Nonce
        {
            get { return _nonce; } set { _nonce = value; }
        }

        public void NewSeed()
        {
            _previousUncryptedServerSeed = _uncryptedServerSeed;
            _uncryptedServerSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _clientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _CryptedServerSeed = HashManager.getHashSha512(_uncryptedServerSeed);
            _nonce = 0;
        }

    }
}
