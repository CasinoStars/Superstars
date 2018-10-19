using System;

namespace Superstars.YamsFair
{
    public class SeedManager
    {

        Random random = new Random();
        string _previousUncryptedServerSeed;
        string _uncryptedServerSeed;
        string _clientSeed;
        string _CryptedServerSeed;
        string _previousCryptedServerSeed;
        string _previousClientSeed;

        public SeedManager()
        {
            _uncryptedServerSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _clientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            _CryptedServerSeed = HashManager.getHashSha512(_uncryptedServerSeed);
            _previousCryptedServerSeed = "testCrypted";
            _previousClientSeed = "testClient";
        }
        public SeedManager(string uncryptedServerSeed, string previousUncryptedServerSeed, string clientSeed, string cryptedServerSeed, string previousClientSeed, string previousCryptedServerSeed)
        {
            _previousCryptedServerSeed = previousCryptedServerSeed;
            _previousClientSeed = previousClientSeed;
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

        public string PreviousClientSeeds
        {
            get { return _previousClientSeed; }
        }

        public string PreviousCryptedServerSeed
        {
            get { return _previousCryptedServerSeed; }
        }

        public void NewSeed(string clientSeed = null)
        {
            _previousCryptedServerSeed = _CryptedServerSeed;
            _previousClientSeed = _clientSeed;
            _previousUncryptedServerSeed = _uncryptedServerSeed;
            _uncryptedServerSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            if(clientSeed == null)
            _clientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            else
            {
                _clientSeed = clientSeed;
            }

            _CryptedServerSeed = HashManager.getHashSha512(_uncryptedServerSeed);
        }

    }
}
