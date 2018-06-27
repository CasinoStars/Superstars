using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    class ProvablyFairData
    {
        public string UncryptedPreviousServerSeed { get; set; }

        public string CryptedPreviousServerSeed { get; set; }

        public string CryptedServerSeed { get; set; }

        public string ClientSeed { get; set; }

        public int Nonce { get; set; }
    }
}
