using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Services
{
    public class YamsService
    {
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
            value = rdn.Next(1, 6);
            return value;
        }

        private int[] DicesValue(int[] hand)
        {
            int[] count = new int[5] { 0, 0, 0, 0, 0 };
            for (int i = 0; i < 5; i++)
            {
                for (int z = 1; z <= 6; z++)
                {
                    if (hand[i] == z) count[z - 1]++;
                }
            }
            return count;
        }

        public int PointCount(int[] hand)
        {
            int[] handcount = new int[5];
            int points = 0;
            int grandesuite = 0;

            handcount = DicesValue(hand);
            for (int i = 0; i < 5; i++)
            {
                //yams
                if (handcount[i] == 5)
                {
                    points = points + 50;
                    points = 5 * (i + 1);
                    return points;
                }

                //carré
                if (handcount[i] == 4)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        if (handcount[l] == 1)
                        {
                            points = points + 40 + (4 * (i + 1)) + l + 1;
                            return points;
                        }
                    }
                }

                // full
                else if (handcount[i] == 3)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        if (handcount[l] == 2)
                        {
                            points = points + 30 + (3 * (i + 1)) + (2 * (l + 1));
                            return points;
                        }
                    }
                }

                // grande suite
                else if (handcount[i] == 1)
                {
                    grandesuite++;
                }
            }
            // petite suite
            if (handcount[0] == 1)
            {
                if ((handcount[1] == 1) && (handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1))
                {
                    points = points + 45;
                }
            }
            else if (handcount[1] == 1)
            {
                if ((handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1) && (handcount[5] == 1))
                {
                    points = points + 45;
                }
            }

            // grande suite
            if (grandesuite == 5)
            {
                points = points + 50;
            }

            // chance
            if (points == 0)
            {
                for (int i = 1; i < 6; i++)
                {
                    points = points + handcount[i - 1] * i;
                }
            }

            return points;
        }

        private string FindFigureName(int[] hand)
		{
			string figureName;
			int[] handcount = new int[5];

			handcount = DicesValue(hand);
			for (int i = 0; i < 5; i++)
			{
				//yams
				if (handcount[i] == 5)
				{
					return figureName = ("yams de " + (i+1));
				}

				//carré
				if (handcount[i] == 4)
				{
					for (int l = 0; l < 5; l++)
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
					for (int l = 0; l < 5; l++)
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