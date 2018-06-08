using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TestBlockChain
{
    class TestSeria
    {

        public static void SerializeMyBlockChain(Blockchain myBlockchain, string fileName) {       

            BinaryFormatter f = new BinaryFormatter();
            using (var stream = File.OpenWrite(fileName))
            {
                f.Serialize(stream, myBlockchain);
            }
        }

        public static Blockchain ReadMyBlockChain(string fileName)
        {
            BinaryFormatter f = new BinaryFormatter();
            Blockchain blockchain2;

            using (var stream = File.OpenRead(fileName))
            {
                blockchain2 = (Blockchain)f.Deserialize(stream);
            }

            return blockchain2;
        }
        
    }
}
