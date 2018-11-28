using System;
using System.Collections.Generic;
using System.Text;

namespace CrashGameMath
{
    public class CrashBuilder
    {
        private readonly List<string> _hashList;
        private static int _playId;
        private string _salt;

        public CrashBuilder(int playNb, string salt)
        {
            var createHash = new CreateHashes(playNb, salt);
            _hashList = createHash.HashList();
            _salt = salt;
        }

        public double NextCrashValue()
        {
            var crashValue = FindCrashValue.FromSha256(_hashList[_playId], _salt);
            _playId++;
            return crashValue;

        }
    }
}

