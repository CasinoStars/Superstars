using System;
using System.Collections.Generic;
using System.Text;

namespace YamsFaire
{
    class SeedManager
    {

        Random random = new Random();
        string _previousUncryptedServerSeed;
        string _uncryptedServerSeed;
        string _clientSeed;
        string _CryptedServerSeed;

        public SeedManager()
        {
            _uncryptedServerSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _clientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _CryptedServerSeed = HashManager.getHashSha512(_uncryptedServerSeed);
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

        public void NewSeed()
        {
            _previousUncryptedServerSeed = _uncryptedServerSeed;
            _uncryptedServerSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _clientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _CryptedServerSeed = HashManager.getHashSha512(_uncryptedServerSeed);
        }

    }
}
