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
        public List<Card> _myhand { get; set; }
        public List<Card> _ennemyhand { get; set; }
        public List<Card> _ennemysecondhand { get; set; }
        public int _pot;
        public bool _dealerTurn;

        Dictionary<Card, int> _values = new Dictionary<Card, int>();

        public BlackJackService()
        {
            InitGame();
        }

        public void InitGame()
        {
            _deck = new Deck();
            _deck.CreateDeck();
            _values = Valueforcards(_values);
            _deck.Shuffle();
            _myhand = new List<Card>();
            _ennemysecondhand = new List<Card>();
            _ennemyhand = new List<Card>();

            _dealerTurn = false;
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

        public bool SplitHand()
        {
            bool hassplit = false;
            if (CanSplit(_ennemyhand))
            {
                List<Card> copylist = new List<Card>();
                copylist = _ennemyhand;
                _ennemyhand.Clear();
                int i = 0;
                foreach (var item in copylist)
                {
                    if (i == 0)
                    {
                        _ennemyhand.Add(item);
                    } else
                    {
                        _ennemysecondhand.Add(item);
                    }
                    i++;
                }
                hassplit = true;
            }
            return hassplit;
        }
        //public int GetHandValue(List<Card> hand)
        //{
        //    int valeur = 0;
        //    foreach (Card carte in hand)
        //    {
        //        foreach (KeyValuePair<Card, int> item in _values)
        //        {
        //            if (carte == item.Key)
        //            {
        //                if (item.Key.Value == 14 && valeur + item.Value > 21)
        //                {
        //                    valeur += 1;
        //                }
        //                else
        //                {
        //                    valeur += item.Value;
        //                }
        //            }
        //        }
        //    }
        //    return valeur;
        //}


        public int GetHandValue(List<Card> hand)
        {
            int valeur = 0;
            int nbraces = 0;
            foreach (Card carte in hand)
            {
                foreach (KeyValuePair<Card, int> item in _values)
                {
                    if (carte == item.Key)
                    {
                        if (item.Key.Value == 14)
                        {
                            nbraces = nbraces + 1;
                        }
                        valeur += item.Value;
                       
                    }
                }
            }

            while(valeur > 21 && nbraces > 0)
            {
                valeur = valeur - 10;
                nbraces = nbraces - 1;
            }
            return valeur;
        }

        public bool CanSplit(List<Card> hand)
        {
            int [] doubletab = new int[hand.Capacity];
            int a = 0;
            foreach (Card carte in hand)
            {
                doubletab[a] = carte.Value;
                a++;
            }

            bool ish = doubletab.Distinct().Count() != doubletab.Length;
            return ish;
        }

        public bool FinishTurn()
        {
            _dealerTurn = true;
            return _dealerTurn;
        }

        public bool BlackJackCheck(List<Card> hand)
        {
            int blowjob = GetHandValue(hand);
            if (blowjob == 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Card> PlayIA(List<Card> myhand, List<Card> ennemyhand)
        {
            if (BlackJackCheck(myhand))
            {
                // BLACKJACK
                return myhand;
            }

            while (GetHandValue(myhand) < 17 || GetHandValue(ennemyhand) > GetHandValue(myhand) && _dealerTurn == true)
            {
                DrawCard(myhand);

                if (BlackJackCheck(myhand))
                {
                    // BLACKJACK
                    return myhand;
                }
            }
            return myhand;
        }
    }
}
