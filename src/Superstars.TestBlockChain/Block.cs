using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using QBitNinja.Client.Models;

namespace TestBlockChain
{
    [Serializable]
    internal class Block
    {
        private int _salt;

        public Block(int index, string timestamp, List<GetTransactionResponse> responses, string previousHash = "")
        {
            Index = index;
            PreviousHash = previousHash;
            Timestamp = timestamp;
            foreach (var trxResponse in responses) Data.Add(trxResponse.TransactionId.ToString());

            Hash = CalculateHash();
            _salt = 0;
        }

        public int Index { get; set; }
        public string PreviousHash { get; set; }

        public string Timestamp { get; set; }
        // public string Data { get; set; }

        public List<string> Data { get; set; } = new List<string>();
        public string Hash { get; set; }


        // Create the hash of the current block.
        public string CalculateHash()
        {
            var hash = SHA256_hash(Index + PreviousHash + Timestamp + Data + _salt);

            return hash;
        }

        // This is how the mining works!
        public void Mine(int difficulty)
        {
            // You mined successfully if you found a hash with certain number of leading '0's
            // difficulty defines the number of '0's required 
            // e.g. if difficulty is 2, if you found a hash like  00338500000x..., it means you mined it.
            while (Hash.Substring(0, difficulty) != new string('0', difficulty))
            {
                _salt++;
                Hash = CalculateHash();
                Console.WriteLine("Mining:" + Hash);
            }

            Console.WriteLine("Block has been mined: " + Hash);
        }

        // Create a hash string from stirng
        private static string SHA256_hash(string value)
        {
            var sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (var b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}