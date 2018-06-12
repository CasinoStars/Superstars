﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Services
{
    public class YamsService
    {
		/*
		 Points:
		  yams
          yams 6 = 80
          yams 5 = 75
          yams 4 = 70
          yams 3 = 65
          yams 2 = 60
          yams 1 = 55

         carré
         carré6-5 = 69 | carré5-6 = 66 | carré4-6 = 62 | carré3-6 = 58 | carré2-6 = 54 | carré1-6 = 52
         carré6-4 = 68 | carré5-4 = 64 | carré4-5 = 61 | carré3-5 = 57 | carré2-5 = 53 | carré1-5 = 51
         carré6-3 = 67 | carré5-3 = 63 | carré4-3 = 59 | carré3-4 = 56 | carré2-4 = 52 | carré1-4 = 50
         carré6-2 = 66 | carré5-2 = 62 | carré4-2 = 58 | carré3-2 = 54 | carré2-3 = 51 | carré1-3 = 49
         carré6-1 = 65 | carré5-1 = 61 | carré4-1 = 57 | carré3-1 = 53 | carré2-1 = 49 | carré1-2 = 48

         Full
         T6-D5 = 58 | T5-D6 = 57 | T4-D6 = 54 | T3-D6 = 51 | T2-D6 = 48 | T1-D6 = 45 
         T6-D4 = 56 | T5-D4 = 53 | T4-D5 = 52 | T3-D5 = 49 | T2-D5 = 46 | T1-D5 = 43
         T6-D3 = 54 | T5-D3 = 51 | T4-D3 = 48 | T3-D4 = 47 | T2-D4 = 44 | T1-D4 = 41
         T6-D2 = 52 | T5-D2 = 49 | T4-D2 = 46 | T3-D2 = 43 | T2-D3 = 42 | T1-D3 = 39
         T6-D1 = 50 | T5-D1 = 47 | T4-D1 = 44 | T3-D1 = 41 | T2-D1 = 38 | T1-D2 = 37

         Grande suite 50 
         Petite Suite 45          
		*/

		public int[] IndexChange(int[] dices, int[] index)
        {
            for (int i = 0; i < index.Length; i++)
            {
                dices[index[i] - 1] = 0;
            }
            return dices;
        }

        public int[] Reroll(int[] dices)
        {
            for (int i = 0; i < 5; i++)
            {
                if (dices[i] == 0)
                {
                    dices[i] = RollDice();
                }
            }
            return dices;
        }

        private int RollDice()
        {
            Random rdn = new Random();
            int value;
            value = rdn.Next(1, 7);
            return value;
        }

        private int[] DicesValue(int[] hand) // hand[ 1,2,1,1,6 ]
        {
            int[] count = new int[6] { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 5; i++)
            {
				count[hand[i]-1] ++;
            }
            return count;
        }

        public int PointCount(int[] hand)
        {
            int[] handcount = new int[6];
            int points = 0;

            handcount = DicesValue(hand);
            for (int i = 0; i < 6; i++)
            {
                //yams
                if (handcount[i] == 5)
                {
                    points = points + 50 + (5 * (i + 1));
                    return points;
                }

                //carré
                if (handcount[i] == 4)
                {
                    for (int l = 0; l < 6; l++)
                    {
                        if (handcount[l] == 1)
                        {
                            points = points + 40 + (4 * (i + 1)) + (l + 1);
                            return points;
                        }
                    }
                }

                // full
                else if (handcount[i] == 3)
                {
                    for (int l = 0; l < 6; l++)
                    {
                        if (handcount[l] == 2)
                        {
                            points = points + 30 + (3 * (i + 1)) + (2 * (l + 1));
                            return points;
                        }
                    }
                }
            }
            // petite suite
            if (handcount[0] == 1)
            {
                if ((handcount[1] == 1) && (handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1))
                {
                    points = points + 45;
                }
            }//grade suite
            else if (handcount[1] == 1)
            {
                if ((handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1) && (handcount[5] == 1))
                {
                    points = points + 50;
                }
            }

            // chance
            if (points == 0)
            {
                for (int i = 1; i <= 6; i++)
                {
                    points = points + handcount[i - 1] * i;
                }
            }
            return points;
        }

        private string FindFigureName(int[] hand)
		{
			string figureName;
			int[] handcount = new int[6];

			handcount = DicesValue(hand);
			for (int i = 0; i < 6; i++)
			{
				//yams
				if (handcount[i] == 5)
				{
					return figureName = ("yams de " + (i+1));
				}

				//carré
				if (handcount[i] == 4)
				{
					for (int l = 0; l < 6; l++)
					{
						if (handcount[l] == 1)
						{
							return figureName = ("carré de " + (i + 1));
						}
					}
				}

				// full
				else if (handcount[i] == 3)
				{
					for (int l = 0; l < 6; l++)
					{
						if (handcount[l] == 2)
						{
							return figureName = "full " + (i + 1) + "-" + (l + 1);
						}
					}
			    }
		    }
			// petite suite
			if (handcount[0] == 1)
			{
				if ((handcount[1] == 1) && (handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1))
				{
					return figureName = "petite suite";
				}
			}
			else if (handcount[1] == 1)
			{
				if ((handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1) && (handcount[5] == 1))
				{
					return figureName = "grande suite";
				}
			}

			// chance
			return figureName = "chance";
		}

		public string[] TabFiguresAndWinner(int[] handIA,int[] handPlayer)
		{
			string[] figuresAndWinnnerNames = new string[3];
			figuresAndWinnnerNames[0] = FindFigureName(handIA);
			figuresAndWinnnerNames[1] = FindFigureName(handPlayer);
			if(PointCount(handIA)>=PointCount(handPlayer))
			{
				figuresAndWinnnerNames[2] = "You Lose";
			}
			else
			{
				figuresAndWinnnerNames[2] = "You Win";
			}
			return figuresAndWinnnerNames;
		}
    }
}