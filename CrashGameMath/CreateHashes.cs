using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CrashGameMath
{
    class CreateHashes
    {
        private readonly int _nbHashes;
        private readonly string _firstEntry;
        internal CreateHashes(int nbHashes, string firstEntry)
        {
            _nbHashes = nbHashes;
            _firstEntry = firstEntry;
        }

        public static string HashSha256(string entry)
        {
            var Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.Default;
                var result = hash.ComputeHash(enc.GetBytes(entry));

                foreach (var b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public List<string> HashList()
        {
            var hashList = new List<string>();
            var toBeHashed = HashSha256(_firstEntry);
            for (var i = 0; i < _nbHashes; i++)
            {
                hashList.Add(toBeHashed);
                toBeHashed = HashSha256(toBeHashed);
            }
            hashList.Reverse();
            return hashList;
        }

    }
}
