using System;

namespace CrashGameMath
{
    class Program
    {
        static void Main(string[] args)
        {
            var createHash = new CreateHashes(100, "0000000000000000004d6ec16dafe9d8370958664c1dc422f452892264c59526");
            foreach (string hash in createHash.HashList())
            {
                double crashValue = FindCrashValue.FromSha256(hash,
                    "0000000000000000004d6ec16dafe9d8370958664c1dc422f452892264c59526");
                Console.WriteLine(hash + "-----" + crashValue);
            }
            Console.ReadKey();
        }
    }
}
