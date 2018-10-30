namespace Superstars.WebApp.Models
{
    public class WalletViewModel
    {
        public int MoneyType { get; set; }

        public int RealCoins { get; set; }

        public int FakeCoins { get; set; }

        public string DestinationAddress { get; set; }

        public int AmountToSend { get; set; }
    }
}