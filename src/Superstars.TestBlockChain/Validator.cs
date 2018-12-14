using Org.BouncyCastle.Crypto.Digests;
using System;
using NBitcoin;

namespace Superstars.Wallet
{
    public class Validator
    {
        public static bool IsValidAddress(string Address)
        {
            byte[] hex = Base58CheckToByteArray(Address);
            if (hex == null || hex.Length != 21)
                return false;
            else
                return true;
        }

        public static byte[] Base58CheckToByteArray(string base58)
        {

            byte[] bb = Base58.ToByteArray(base58);
            if (bb == null || bb.Length < 4) return null;

            Sha256Digest bcsha256a = new Sha256Digest();
            bcsha256a.BlockUpdate(bb, 0, bb.Length - 4);

            byte[] checksum = new byte[32];
            bcsha256a.DoFinal(checksum, 0);
            bcsha256a.BlockUpdate(checksum, 0, 32);
            bcsha256a.DoFinal(checksum, 0);

            for (int i = 0; i < 4; i++)
            {
                if (checksum[i] != bb[bb.Length - 4 + i]) return null;
            }

            byte[] rv = new byte[bb.Length - 4];
            Array.Copy(bb, 0, rv, 0, bb.Length - 4);
            return rv;
        }


        /// <summary>
        /// nbOfTest represente the numbre of TestNet address that will be generate
        /// and then foreach address we will call the validator throw an expection if on of the address is not valid
        /// </summary>
        /// <param name="nbOfTest"></param>
        private void TestValidator(int nbOfTest)
        {
            for (int i = 0; i < nbOfTest; i++)
            {
                var testedKey = new Key();

                if (Validator.IsValidAddress(testedKey.PubKey.GetAddress(Network.TestNet).ToString()) == true) Console.WriteLine("TRUE");
                else
                {
                    throw new Exception("FASLE");
                }
            }
        }
    }
}
