using System;
using System.Collections.Generic;
using System.Text;

namespace CrashGameMath
{
    public class CrashBuilder
    {
        private readonly List<string> _hashList;
        public static string ActualHashString { get; set; }
        public static double ActualHashValue { get; set; }
        private static int _playId;

        public class Hash
        {
            public string HashString { get; set; }
            public double HashValue { get; set; }
        }

        public CrashBuilder(int playNb, string salt)
        {
            var createHash = new CreateHashes(playNb, salt);
            _hashList = createHash.HashList();
        }

        public double NextCrashValue()
        {
            var crashValue = FindCrashValue.FromSha256(_hashList[_playId], "0000000000000000004d6ec16dafe9d8370958664c1dc422f452892264c59526");
            ActualHashValue = crashValue;
            ActualHashString = _hashList[_playId];
            _playId++;
            return crashValue;
        }

        public List<Hash> CreateHashList(int nb, string hash)
        {
            if (nb > _playId)
                nb = _playId;
            var hashList = new CreateHashes(nb-1, hash).HashList();
            var hashAndValueList = new List<Hash>();
            foreach (var hashString in hashList)
            {
                var hashAndValue = new Hash {HashString = hashString, HashValue = FindCrashValue.FromSha256(hashString, "0000000000000000004d6ec16dafe9d8370958664c1dc422f452892264c59526") };
                hashAndValueList.Add(hashAndValue);
            }
        
            hashAndValueList.Reverse();
            return hashAndValueList;
        }
    }
}

