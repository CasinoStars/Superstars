using System;
using System.Collections.Generic;
using System.Text;

namespace YamsFaire
{
    class Dices
    {

        public int[] dices { get; set; }

        public Dices()
        {
            dices = new int[5] { 1, 1, 1, 1, 1 };
        }


        public int[] rolleDices()
        {
            Random random = new Random();
            int i = 0;
            foreach (var dice in dices)
            {
                dices[i] = random.Next(1, 6);
                i++;
            }
           return dices;
        }
    }
}
