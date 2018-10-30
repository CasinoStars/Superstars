using System;

namespace Superstars.YamsFair
{
    public class SeedManager
    {
        private readonly Random random = new Random();

        public SeedManager()
        {
            UncryptedServerSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            ClientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            CryptedServerSeed = HashManager.getHashSha512(UncryptedServerSeed);
            PreviousCryptedServerSeed = "";
            PreviousClientSeeds = "";
        }

        public SeedManager(string uncryptedServerSeed, string previousUncryptedServerSeed, string clientSeed,
            string cryptedServerSeed, string previousClientSeed, string previousCryptedServerSeed)
        {
            PreviousCryptedServerSeed = previousCryptedServerSeed;
            PreviousClientSeeds = previousClientSeed;
            PreviousUncryptedServerSeed = previousUncryptedServerSeed;
            UncryptedServerSeed = uncryptedServerSeed;
            ClientSeed = clientSeed;
            CryptedServerSeed = cryptedServerSeed;
        }

        public string PreviousUncryptedServerSeed { get; private set; }

        public string CryptedServerSeed { get; private set; }

        public string ClientSeed { get; private set; }

        public string UncryptedServerSeed { get; private set; }

        public string PreviousClientSeeds { get; private set; }

        public string PreviousCryptedServerSeed { get; private set; }

        public void NewSeed(string clientSeed = null)
        {
            PreviousCryptedServerSeed = CryptedServerSeed;
            PreviousClientSeeds = ClientSeed;
            PreviousUncryptedServerSeed = UncryptedServerSeed;
            UncryptedServerSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            if (clientSeed == null)
                ClientSeed = HashManager.getHashSha512(random.Next(int.MaxValue).ToString());
            else
                ClientSeed = clientSeed;

            CryptedServerSeed = HashManager.getHashSha512(UncryptedServerSeed);
        }
    }
}