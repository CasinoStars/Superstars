﻿using Superstars.WebApp.Controllers;
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

            while (GetHandValue(myhand) < 17 && GetHandValue(ennemyhand) > GetHandValue(myhand) && _dealerTurn == true)
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
