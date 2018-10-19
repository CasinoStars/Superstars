using System;
using System.Collections.Generic;

namespace Superstars.YamsFair
{
    class Program
    {
        static void Main(string[] args)
        {


            

            List<int> dicesFromSeeds = HashManager.RetriveDicesFromSeeds("ff0b463695f88ecd9a8946327b764a3a0acf935d1f6a527e9359643950bd025900495acd3c4a1e727f4ac5be9713e313700993878b0960a59a05627b9a51268b", "AlbinSeed",5);

            foreach (var item in dicesFromSeeds)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }

        
        
    }
}
