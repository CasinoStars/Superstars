using System;
using System.Collections.Generic;
using System.Linq;
using Superstars.WebApp.Controllers;
using Superstars.YamsFair;

namespace Superstars.WebApp
{
    public class Deck
    {
        

        public string[] Symbole = {"c", "h", "p", "t"};

        public int[] Valeur = {2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14};

        public List<Card> DeckCards { get; set; }

        //Initialize and create a Deck of Cards
        public void CreateDeck()
        {
            var NewDeck = new List<Card>();

            for (var i = 0; i < Symbole.Length; i++)
            for (var j = 0; j < Valeur.Length; j++)
                NewDeck.Add(new Card(Symbole[i], Valeur[j]));

            DeckCards = NewDeck;
        }

        //Shuffle the Deck
        public void Shuffle()
        {
            for (var i = 0; i < 52; i++) DeckCards.Add(RandomDraw());
        }

        //Draw a random card
        private Card RandomDraw()
        {
            var rnd = new Random();
            var random = rnd.Next(1, DeckCards.Count);
            var drawedCard = DeckCards.ElementAt(random);
            DeckCards.RemoveAt(random);
            return drawedCard;
        }

        //Draw the top Card
        public Card Draw(int userid, int rand)
        {           
            var top = DeckCards.Count;
          var drawedcard = DeckCards.ElementAt(rand - 1);
            /// apeller le random des famille 

            DeckCards.RemoveAt(rand - 1);
            return drawedcard;
        }
    }
}