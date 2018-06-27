using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.YamsFair
{
    public class SeedManager
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
        public SeedManager(string uncryptedServerSeed, string previousUncryptedServerSeed,string clientSeed, string cryptedServerSeed)
        {
            _previousUncryptedServerSeed = previousUncryptedServerSeed;
            _uncryptedServerSeed = uncryptedServerSeed;
            _clientSeed = clientSeed;
            _CryptedServerSeed = cryptedServerSeed;
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

        public string UncryptedServerSeed
        {
            get { return _uncryptedServerSeed; }
        }

        public void NewSeed(string clientSeed = null)
        {
            _previousUncryptedServerSeed = _uncryptedServerSeed;
            _uncryptedServerSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            if(clientSeed == null)
            _clientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            else
            {
                _clientSeed = HashManager.getHashSha512(clientSeed);
            }

            _CryptedServerSeed = HashManager.getHashSha512(_uncryptedServerSeed);
        }

    }
}
