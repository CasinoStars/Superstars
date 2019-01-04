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
        private readonly string _salt;

        public class Hash
        {
            public string HashString { get; set; }
            public double HashValue { get; set; }
        }

        public CrashBuilder(int playNb, string salt)
        {
            var createHash = new CreateHashes(playNb, salt);
            _hashList = createHash.HashList();
            _salt = salt;
        }

        public double NextCrashValue()
        {
            var crashValue = FindCrashValue.FromSha256(_hashList[_playId], _salt);
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
                var hashAndValue = new Hash {HashString = hashString, HashValue = FindCrashValue.FromSha256(hashString, _salt)};
                hashAndValueList.Add(hashAndValue);
            }
        
            hashAndValueList.Reverse();
            return hashAndValueList;
        }
    }
}

