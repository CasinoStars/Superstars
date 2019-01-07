namespace Superstars.DAL
{
    public class ProvablyFairData
    {
        public string UncryptedPreviousServerSeed { get; set; }

        public string UncryptedServerSeed { get; set; }

        public string PreviousCryptedServerSeed { get; set; }

        public string PreviousClientSeed { get; set; }

        public string CryptedServerSeed { get; set; }

        public string ClientSeed { get; set; }

        public int Nonce { get; set; }
    }
}