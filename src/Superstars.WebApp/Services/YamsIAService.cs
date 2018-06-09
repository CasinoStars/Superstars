using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Services
{
    public class YamsIAService
    {
		int[] _actualBestHand = new int[5] { 0,0,0,0,0}; // on met à 0 comme ça le nombre de points de cette mmain est à 0 et la probabilité de cette main est la plus haute
		int[] _myHand = new int[5];
		int _enemyPoints;
		int _handPoints;

		public YamsIAService( int[] myHand, int enemyHand)
		{
			_enemyPoints = enemyHand;
			_myHand = myHand;
		}

		public void ChangeOneDice()
		{
			int[] testHand = new int[5];
			for(int index = 0 ; index <5 ; index ++ )
			{
				for(int diceValue = 1 ; diceValue <= 6 ; diceValue++ )
				{
					testHand[index] = diceValue;
					if( HandProba(NumberRerollDice(testHand)) >= HandProba(NumberRerollDice(_actualBestHand)) && PointCount(testHand) < _enemyPoints)
					{
						_actualBestHand = testHand; 
					}
				}
			}
		}

		public int[] GiveRerollHand()
		{
			int[] toRerollHand = new int[5];
			Array.Copy(_myHand, toRerollHand, 5);

			for(int i = 0 ; i < 5 ; i++)
			{
				if(_actualBestHand[i] != toRerollHand[i])
				{
					toRerollHand[i] = 0;
				}
			}
			return toRerollHand;
		}

		private int PointCount(int[] hand)
		{
			int[] handcount = new int[6];
			int points = 0;

			handcount = DicesValue(hand);
			for (int i = 0; i < 5; i++)
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
					for (int l = 0; l < 5; l++)
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
					for (int l = 0; l < 5; l++)
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
				for (int i = 1; i < 6; i++)
				{
					points = points + handcount[i - 1] * i;
				}
			}

			return points;
		}

		private int[] DicesValue(int[] hand)
		{
			int[] count = new int[6] { 0, 0, 0, 0, 0, 0 };
			for (int i = 0; i < 5; i++)
			{
				for (int z = 1; z <= 6; z++)
				{
					if (hand[i] == z) count[z - 1]++;
				}
			}
			return count;
		}

		private float HandProba(int diceRerollNumber)
		{
			float proba;
			switch (diceRerollNumber)
			{
				case 1:
					proba = ((float)1 / (float)6);
					break;
				case 2:
					proba = ((float)2 / (float)36);
					break;
				case 3:
					proba = ((float)6 / (float)(6 * 6 * 6));
					break;
				case 4:
					proba = ((float)24 / (float)(6 * 6 * 6 * 6));
					break;
				case 5:
					proba = ((float)120 / (float)(6 * 6 * 6 * 6 * 6));
					break;
				default:
					throw new NotImplementedException("bad numberofdices");
			}
			return proba;
		} // give proba according to the number of dices we reroll in the same time

		private int NumberRerollDice(int[] handAfterReroll)
		{
			int numberOfDiceReroll = 0 ;
			for(int i =0; i<_myHand.Length;i++)
			{
				if(_myHand[i]!=handAfterReroll[i])
				{
					numberOfDiceReroll++;
				}
			}
			return numberOfDiceReroll;
		}
	}
}
