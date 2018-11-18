namespace Superstars.YamsFair
{
    internal class FairDices
    {
        private readonly SeedManager _seedmanager = new SeedManager();

        public FairDices()
        {
            dices = new int[5] {1, 2, 3, 4, 5};
        }

        public int[] dices { get; }

        public void RolleDices(string serveurSeed, string clientSeed, int nonce)
        {
            var random = 0;
            var i = 0;
            foreach (var dice in dices)
            {
                random = HashManager.GetDiceFromHash(_seedmanager.CryptedServerSeed, _seedmanager.ClientSeed, nonce, 6);
                nonce++;
                dices[i] = random;
                i++;
            }
        }
    }
}