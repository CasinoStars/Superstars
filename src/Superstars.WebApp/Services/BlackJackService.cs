using Superstars.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superstars.WebApp
{
    public class BlackJackService
    {
        Deck _deck;
        List<Card> _myhand = new List<Card>();
        List<Card> _ennemyhand = new List<Card>();
        int _pot;
        bool _hit;
        bool _stand;

        Dictionary<Card, int> _values = new Dictionary<Card, int>();

        public BlackJackService()
        {
            _deck = new Deck();
            _deck.CreateDeck();
            _values = Valueforcards(_values);
            _deck.Shuffle();
            _hit = false;
            _stand = false;
        }

        internal Dictionary<Card, int> Valueforcards(Dictionary<Card, int> valuesdic)
        {
            foreach (Card carte in _deck.DeckCards)
            {
                if (carte.Value < 10)
                {
                    valuesdic.Add(carte, carte.Value);
                }
                else if (carte.Value < 14)
                {
                    valuesdic.Add(carte, 10);
                }
                else
                {
                    valuesdic.Add(carte, 11);
                }
            }
            return valuesdic;
        }

        public List<Card> DrawCard(List<Card> hand)
        {
            Card card = _deck.Draw();
            hand.Add(card);
            return hand;
        }

        public int GetHandValue(List<Card> hand)
        {
            int valeur = 0;
            foreach (Card carte in hand)
            {
                foreach (KeyValuePair<Card, int> item in _values)
                {
                    if (carte == item.Key)
                    {
                        if (item.Key.Value == 14 && valeur + item.Value > 21)
                        {
                            valeur += 1;
                        }
                        else
                        {
                            valeur += item.Value;
                        }
                    }
                }
            }
            return valeur;
        }

        public List<Card> HitOrStand(List<Card> hand, int value, bool choice)
        {
            if (choice && value < 21)
            {
                hand = DrawCard(hand);
                return hand;
            }
            else
            {
                _stand = true;
                return hand;
            }
        }


        public void PlayBlackJack(int bet)
        {
            _pot = 2 * bet;
            _myhand = DrawCard(_myhand);
            _myhand = DrawCard(_myhand);
            int a = GetHandValue(_myhand);
            bool choice = true;
            _myhand = HitOrStand(_myhand, a, choice);

        }
    }
}
