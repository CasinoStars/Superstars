using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrashGameMath
{
    class FindCrashValue
    {
       public static double FromSha256(string seed, string salt)
        {
            salt = salt ?? "";
            int nBits = 52;

            byte[] keyBytes = Encoding.Default.GetBytes(salt);
            byte[] seedBytes = seed.HexStringToByteArray();
            using (var hmacSha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashMessage = hmacSha256.ComputeHash(seedBytes);
                seed = BitConverter.ToString(hashMessage);
            }

            seed = seed.Replace("-", "");
            seed = seed.Slice(0, nBits / 4);
            long r = Int64.Parse(seed, NumberStyles.HexNumber);
            var X = r / Math.Pow(2, nBits);
            X = 99 / (1 - X);

            var result = Math.Floor(X);
            return Math.Max(1, result / 100);
        }
    }
}