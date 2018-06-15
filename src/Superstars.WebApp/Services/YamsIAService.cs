using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Services
{
    public class YamsIAService
	{
        YamsService _yamsService;
        int[] _myHand = new int[5];
		int _enemyPoints;
		List<float[]> _indexDice = new List<float[]>();

		public YamsIAService(YamsService yamsService)
		{
            _yamsService = yamsService;
		}

        private int PointCount(int[] hand) => _yamsService.PointCount(hand);

        private void ChangeOneDice()
		{
			int[] testHand = new int[5];
			for (int index = 0; index < 5; index++)
			{
				for (int diceValue = 1; diceValue <= 6; diceValue++)
				{
					Array.Copy(_myHand, testHand, 5);
					testHand[index] = diceValue;
					if ((PointCount(testHand) > _enemyPoints))
					{
						float[] result = new float[2] { index, 1 };
						if (TabAlreadyExist(result))
						{
							var indexQuery = _indexDice.IndexOf(_indexDice.Where((float[] array) => array[0] == result[0]).First());
							// var indexQuery = _indexDice.IndexOf(_indexDice.Where((int[] array) => array[0] == result[0] && array[1] == result[1]).First());
							_indexDice[indexQuery][_indexDice[indexQuery].Length-1]++;
						}
						else
						{
							_indexDice.Add(result);
						}
					}
				}
			}
		}

		private void ChangeTwoDices()
		{
			int[] testHand = new int[5];

			for (int indexDice1 = 0; indexDice1 < 5; indexDice1++) // index of the first dice we change in the hand
			{
				for (int valueDiceOne = 1; valueDiceOne <= 6; valueDiceOne++) // value of the first dice we change in the hand
				{
					for (int indexDice2 = 0; (indexDice2 < 5) && (indexDice2 != indexDice1); indexDice2++) // index of the first dice we change in the hand
					{
						for (int valueDice2 = 1; valueDice2 <= 6; valueDice2++) // value of the first dice we change in the hand
						{
							Array.Copy(_myHand, testHand, 5);
							testHand[indexDice1] = valueDiceOne;
							testHand[indexDice2] = valueDice2;
							if (PointCount(testHand) > _enemyPoints)
							{
								if (indexDice1 == 3 || indexDice2 == 3)
								{
								}
								float[] result = new float[3] { indexDice1, indexDice2, 1 };
								if (TabAlreadyExist(result))
								{
									var indexQuery = _indexDice.IndexOf(_indexDice.Where((float[] array) => array[0] == result[0] && array[1] == result[1]).First());
									_indexDice[indexQuery][_indexDice[indexQuery].Length - 1]++;
								}
								else
								{
									_indexDice.Add(result);
								}
							}
						}
					}
				}
			}
		}

		private void ChangeThreeDices()
		{
			int[] testHand = new int[5];
			for (int indexDice1 = 0; indexDice1 < 5; indexDice1++)
			{
				for (int indexDice2 = 0; indexDice2 < 5 && indexDice2 != indexDice1; indexDice2++)
				{
					for (int indexDice3 = 0; indexDice3 < 5 && indexDice3 != indexDice1 && indexDice2 != indexDice3; indexDice3++)
					{
						for (int valueDice1 = 1; valueDice1 <= 6; valueDice1++)
						{
							for (int valueDice2 = 1; valueDice2 <= 6; valueDice2++)
							{
								for (int valueDice3 = 1; valueDice3 <= 6; valueDice3++)
								{
									Array.Copy(_myHand, testHand, 5);
									testHand[indexDice1] = valueDice1;
									testHand[indexDice2] = valueDice2;
									testHand[indexDice3] = valueDice3;
									if (PointCount(testHand) > _enemyPoints)
									{
										if (indexDice1 == 2 && (indexDice2 != 0 || indexDice3 != 0))
										{
										}
										float[] result = new float[4] { indexDice1, indexDice2, indexDice3, 1 };
										if (TabAlreadyExist(result))
										{
											var indexQuery = _indexDice.IndexOf(_indexDice.Where((float[] array) => array[0] == result[0] && array[1] == result[1] && array[2] == result[2]).First());
											_indexDice[indexQuery][_indexDice[indexQuery].Length - 1]++;
										}
										else
										{
											_indexDice.Add(result);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void ChangeFourDices()
		{
			int[] testHand = new int[5];
			for (int indexUnchangedDice = 0; indexUnchangedDice < 5; indexUnchangedDice++) // index of the unchanged dice in the hand
			{
				for (int valueDice1 = 1; valueDice1 <= 6; valueDice1++) // value of the first dice we change 
				{
					for (int valueDice2 = 1; valueDice2 <= 6; valueDice2++) // value of the second dice we change
					{
						for (int valueDice3 = 1; valueDice3 <= 6; valueDice3++) // value of the third dice we change
						{
							for (int valueDice4 = 1; valueDice4 <= 6; valueDice4++) // value of the forth dice we change
							{
								Array.Copy(_myHand, testHand, 5);
								List<int> values = new List<int> { valueDice1, valueDice2, valueDice3, valueDice4 };
								values.Insert(indexUnchangedDice, testHand[indexUnchangedDice]);
								for (int i = 0; i < 5; i++)
								{
									testHand[i] = values[i];
								}
								if (PointCount(testHand) > _enemyPoints)
								{
									int[] indexs = new int[4];
									int p = 0;
									for (int k = 0; k < 5; k++)
									{
										if (k != indexUnchangedDice)
										{
											indexs[p] = k;
											p++;
										}
									}
									float[] result = new float[5] { indexs[0], indexs[1], indexs[2], indexs[3], 1 };
									if (TabAlreadyExist(result))
									{
										var indexQuery = _indexDice.IndexOf(_indexDice.Where((float[] array) => array[0] == result[0] && array[1] == result[1]).First());
										_indexDice[indexQuery][_indexDice[indexQuery].Length - 1]++;
									}
									else
									{
										_indexDice.Add(result);
									}
								}
							}
						}
					}
				}
			}
		}

		public void ChangeFiveDices()
		{
			int betterThanEnemyHand = 0;
			int[] testHand = new int[5];
			Array.Copy(_myHand, testHand, 5);
			for (int valueDice1 = 1; valueDice1 <= 6; valueDice1++) // value of the first dice in the hand 
			{
				for (int valueDice2 = 1; valueDice2 <= 6; valueDice2++) // value of the second dice in the hand 
				{
					for (int valueDice3 = 1; valueDice3 <= 6; valueDice3++) // value of the third dice in the hand 
					{
						for (int valueDice4 = 1; valueDice4 <= 6; valueDice4++) // value of the fourth dice in the hand 
						{
							for (int valueDice5 = 1; valueDice5 <= 6; valueDice5++) // value of the fifth dice in the hand 
							{
								testHand[0] = valueDice1;
								testHand[1] = valueDice2;
								testHand[2] = valueDice3;
								testHand[3] = valueDice4;
								testHand[4] = valueDice5;
								if (PointCount(testHand) >= _enemyPoints)
								{
									betterThanEnemyHand++;
								}
							}
						}
					}
				}
			}
			float[] Levesque = new float[6] { 0, 1, 2, 3, 4, betterThanEnemyHand };
			_indexDice.Add(Levesque);
		}

		public int[] TheBestIndexProba()
		{
			int lengthOfTheTableInTheList = 0;
			int[] toRerollIndex;
			float[] THE_ONE = new float[6];
			for (int i = 0; i < _indexDice.Count - 1; i++)
			{
				if (_indexDice[i][_indexDice[i].Length - 1] > _indexDice[i + 1][_indexDice[i + 1].Length - 1])
				{
					lengthOfTheTableInTheList = _indexDice[i].Length;
					THE_ONE = new float[_indexDice[i].Length];
					Array.Copy(_indexDice[i], THE_ONE, _indexDice[i].Length);
				}
				else
				{
					lengthOfTheTableInTheList = _indexDice[i + 1].Length;
					THE_ONE = new float[_indexDice[i + 1].Length];
					Array.Copy(_indexDice[i + 1], THE_ONE, _indexDice[i + 1].Length);
				}
			}
			toRerollIndex = new int[lengthOfTheTableInTheList];
			for (int i = 0; i < (lengthOfTheTableInTheList - 1); i++)
			{
				toRerollIndex[i] = (int)THE_ONE[i];
			}
			return toRerollIndex;
		}

		public int[] GiveRerollHand(int[] myhand,int enemypoint)
		{
            _myHand = myhand;
            _enemyPoints = enemypoint;
			ChangeOneDice();
			ChangeTwoDices();
			ChangeThreeDices();
			ChangeFourDices();
			ChangeFiveDices();
			MultiplyProbaAndNumber();
			int[] indexes = TheBestIndexProba();
			int[] toRerollHand = new int[5];
			Array.Copy(_myHand, toRerollHand, 5);
			for (int i = 0; i < indexes.Length; i++)
			{
				toRerollHand[indexes[i]] = 0;
			}
			return toRerollHand;
		}

		#region Tolls methodes
		private int[] TriABulle(int[] hand)
		{
			int taille = hand.Length - 1;
			bool tableenordre = false;
			while (!tableenordre)
			{
				tableenordre = true;
				for (int i = 0; i < taille; i++)
				{
					if (hand[i] > hand[i + 1])
					{
						int stock = hand[i];
						hand[i] = hand[i + 1];
						hand[i + 1] = stock;
						tableenordre = false;
					}
				}
				taille--;
			}
			return hand;
		}

		private bool TabAlreadyExist(float[] check)
		{
			int[] part = TakeonlyIndex(check);
			part = TriABulle(part);
			bool oooll = false;
			if (_indexDice.Count == 0)
			{
				return oooll;
			}
			for (int i = 0; i < _indexDice.Count; i++)
			{
				int numberSameDice = 0;
				int[] oldIndexTab = TakeonlyIndex(_indexDice[i]);
				oldIndexTab = TriABulle(oldIndexTab);
				for (int j = 0; j < part.Length; j++)
				{
					if (oldIndexTab.Length == part.Length)
					{
						if (part[j] == oldIndexTab[j])
						{
							numberSameDice++;
						}
					}
					if (numberSameDice == part.Length)
					{
						oooll = true;
					}
				}
			}
			return oooll;
		}  // number of dices who are the sames

		private int[] TakeonlyIndex(float[] check)
		{
			int[] part = new int[check.Length - 1];
			for (int i = 0; i < part.Length; i++)
			{
				part[i] = (int)check[i];
			}
			return part;
		} // return the tab of index minus the number of apparition

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
				case 0:
					proba = 1;
					break;
				case 1:
					proba = ((float)1 / (float)6);
					break;
				case 2:
					proba = ((float)1 / (float)(6 * 6));
					break;
				case 3:
					proba = ((float)1 / (float)(6 * 6 * 6));
					break;
				case 4:
					proba = ((float)1 / (float)(6 * 6 * 6 * 6));
					break;
				case 5:
					proba = ((float)1 / (float)(6 * 6 * 6 * 6 * 6));
					break;
				default:
					throw new NotImplementedException("bad number of dices");
			}
			return proba;
		} // give proba according to the number of dices we reroll in the same time

		private void MultiplyProbaAndNumber()
		{
			for (int i = 0; i < _indexDice.Count; i++)
			{
				int m = _indexDice[i].Length - 1;
				_indexDice[i][m] = _indexDice[i][m] * HandProba(m);
			}
		}
		#endregion
	}
}


