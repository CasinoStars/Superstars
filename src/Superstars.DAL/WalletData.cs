namespace Superstars.DAL
{
    public class WalletData
    {
        public int MoneyId { get; set; }

        public int Balance { get; set; }

        public int MoneyType { get; set; }

        public int RealCoins { get; set; }

        public int FakeCoins { get; set; }

        public int Profit { get; set; }

        public string PrivateKey { get; set; }
    }
}
