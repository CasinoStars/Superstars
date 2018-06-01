using System;
using System.Collections.Generic;
using System.Text;

namespace TriABulle
{
	class IAYams
	{
		int _enemypoints;
		int _mypoints;
		int[] _myhand;
		int[][] _pointstab = new int[68][];

		public IAYams(int enemypoints, int[] myhand)
		{
			_enemypoints = enemypoints;
			_myhand = myhand;
			_mypoints = PointCount(_myhand);
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

		private bool IsAWinHand(int[] hand)
		{
			int points;
			points = PointCount(hand);
			return (points > _enemypoints);
		}

		private List<int[]> MultipleFor(int diceNumber)
		{
			int[] testhand = new int[5];
			Array.Copy(_myhand, testhand, 5);
			List<int> OldIndex = new List<int>();
			List<int[]> HandPlusProba = new List<int[]>();

			Found:
			for (int i = 0; DifferentFromOthers(i, OldIndex) && i < 5; i++)
			{
				for (int j = 1; j < 6; j++)
				{
					OldIndex.Add(i);
					testhand[i] = j;
					if (diceNumber != 0)
					{
						diceNumber--;
						goto Found;
					}
					if (IsAWinHand(testhand))
					{
						foreach (int x in OldIndex)
						{
							testhand[x] = 0;
							int proba = 17;
							int[] caca = new int[6];
							Array.Copy(caca, testhand, 5);
							caca[5] = proba;
							HandPlusProba.Add(caca);
						}
					}
				}
			}
			return HandPlusProba;
		} // prend le nombre de dé à relancer et renvoi le tableau de main + probabilité

		public bool DifferentFromOthers(int one, List<int> others)
		{
			foreach (int i in others)
			{
				if (i == one)
				{
					return false;
				}
			}
			return true;
		} // dit si l'index du dé est nouvelle

		private List<int[]> PossibilitiesTabEdit() // utilise Multiple for 5fois et ajoute chaque partie à une liste entière regroupant tout
		{
			List<int[]> AllWinHands = new List<int[]>();
			for (int i = 0; i < 5; i++)
			{
				AllWinHands.AddRange(MultipleFor(i + 1));
			}
			return AllWinHands;
		}

		public int[] ChooseRerollDices()
		{
			List<int[]> AllWinHand = new List<int[]>();
			int[] hand = new int[5];

			AllWinHand = PossibilitiesTabEdit();

			if (AllWinHand.Count > 1)
			{
				for (int i = 0; i < AllWinHand.Count - 1; i++)
				{
					if (AllWinHand[0][6] < AllWinHand[1][6])
					{
						AllWinHand[0][6] = AllWinHand[1][6];
						AllWinHand.RemoveAt(1);
					}
				}
			}
			for (int i = 0; i < 5; i++)
			{
				hand[i] = AllWinHand[0][i];
			}
			return hand;
		} // select the hand with the best probabilitie
	}
}
