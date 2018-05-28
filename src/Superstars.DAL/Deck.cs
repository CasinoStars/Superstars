using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    class Deck
    {
        public string[] Symbole = new string[] { "Carreau", "Coeur", "Pique", "Trefle" };

        public int[] Valeur = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

        public Card[] DeckCards { get; set; }



        public Card[] CreateDeck()
        {
            Card[] newDeck = new Card[52];
            var x = 0;

            for (int i = 0; i < Symbole.Length; i++)
            {
                for (int j = 0; j < Valeur.Length; j++)
                {
                    newDeck[x] = new Card(Symbole[i], Valeur[j]);
                    x = x + 1;
                }
            }
            DeckCards = newDeck;
            return newDeck;
        }

        public void Shuffle()
        {
            Card[] newDeck = new Card[52];
            for (int i = 0; i < 52; i++)
            {
                newDeck[i] = RandomDraw();
            }
            DeckCards = newDeck;
        }

        public Card RandomDraw()
        {
            Random rnd = new Random();
            int random = rnd.Next(1, 52);
            Card drawedCard = DeckCards[random];
            var kk = new List<Card>(DeckCards);
            kk.RemoveAt(random);
            DeckCards = kk.ToArray();
            return drawedCard;
        }

        public Card Draw()
        {
            int top = DeckCards.Length - 1;
            var drawedcard = DeckCards[top];
            var kk = new List<Card>(DeckCards);
            kk.RemoveAt(top);
            DeckCards = kk.ToArray();
            return drawedcard;
        }
    }
}
