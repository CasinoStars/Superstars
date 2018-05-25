using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    class CardsDeck
    {
        public Dictionary<string, List<int>> Deck { get; set; }

        public void CreateDeck()
        {
            Deck = new Dictionary<string, List<int>>();
            string[] colors = new string[4];
            colors[0] = "Carreau";
            colors[1] = "Coeur";
            colors[2] = "Trefle";
            colors[3] = "Pique";

            List<int> numbers = new List<int>(15);

            for (int i = 2; i < numbers.Capacity; i++)
            {
                numbers.Add(i);
            }

            foreach (var item in colors)
            {
                Deck.Add(item, numbers);
            }
        }
    }
}
