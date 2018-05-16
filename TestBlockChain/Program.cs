using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestBlockChain
{
    class Program
    {
        static void Main(string[] args)
        {

            Blockchain myCoinBlockchain = TestSeria.ReadMyBlockChain("test2");
            myCoinBlockchain.Difficulty = 1;

            // Received a block from the P2P network.
            // Validate 300 coins transfer.
            Console.WriteLine("Mining a block...");

            

            myCoinBlockchain.AddBlock(new Block(1, "03/12/2017", "300"));
            myCoinBlockchain.AddBlock(new Block(2, "03/12/2017", "ok"));
            myCoinBlockchain.AddBlock(new Block(3, "03/12/2017", "committe"));


            // this line below will cause the chain to be invalid.
          //  myCoinBlockchain.GetLatestBlock().PreviousHash = "";

            // Validate the chain
            myCoinBlockchain.ValidateChain();




            foreach (var item in myCoinBlockchain._chain)
            {
                Console.WriteLine(item.Data);
            }

            TestSeria.SerializeMyBlockChain(myCoinBlockchain, "test");


            Console.WriteLine("Done");

           
            Console.ReadKey();
        }
    }
}
