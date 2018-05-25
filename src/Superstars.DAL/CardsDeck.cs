using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    class CardsDeck
    {
        public Dictionary<string, int> Deck { get; set; }

        public Dictionary<string, int> CreateDeck()
        {
            string[] colors = new string[3];
            colors[0] = "Carreau";
            colors[1] = "Coeur";
            colors[2] = "Trefle";
            colors[3] = "Pique";

            int[] numbers = new int[13];
            for (int i = 1; i < 14; i++)
            {
                numbers[i - 1] = i;
            }

            Dictionary<string, int> deck = new Dictionary<string, int>();
            foreach (string item in colors)
            {
                foreach (int nmbr in numbers)
                {
                    deck.Add(item, nmbr);
                }
            }
            return deck;
        }


    }
}
