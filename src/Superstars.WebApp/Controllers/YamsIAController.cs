using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    public class YamsIAController : Controller
    {
		#region AllAboutPoints
		// Calcul Points = 
		// Yams : 50 + 5 * value
		// Carré :  40 + 4*value1 + 1*value2
		// Full : 30 + 3*value1 + 2*value2
		// Grande Suite : 50
		// Petite Suite : 45
		// Chance : nb1 + nb2 * 2 + nb3 * 3 + nb4 * 4 + nb5 * 5 + nb6 * 6

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
		#endregion

		int _enemypoints;
		int _mypoints;
		int[] _myhand;
		int[] _rerollDices;
		int[][] _pointstab = new int[68][]; 

		public YamsIAController(int enemypoints,int mypoints,int[] myhand)
		{
			_enemypoints = enemypoints;
			_mypoints = mypoints;
			_myhand = myhand;
			FillPointArray();
		}
		
		private int[][] ToRerollHands()
		{
			int[][] handAndProba = new int[10000][];
			int points =_mypoints;
			int place = 0;
			// si on reroll qu'un dé
			for(int i =0;i<5;i++)
			{

				int[] _myhandtest = _myhand;
				for(int j=1; j<6;j++)
				{
					int proba = 1/6;
					int[] myhand = new int[6]; 
					_myhandtest[i] =j;
					points = PointCount(_myhandtest);

					if(points >= _mypoints)
					{
						_myhandtest[i] = 0;
						for(int z=0; z<5; z++)
						{
							myhand[z] = _myhandtest[z]; 
						}
						myhand[6] = proba;
						handAndProba[place] = myhand;
						place++;
					}
				}
			}
			
			// on prend 2 membres du tableau
			// premier dé
			for (int i = 0; i < 5; i++) // i  est la place(index) du premier dé dans la main
			{
				int[] _myhandtest = _myhand;
				for (int j = 1; j < 6; j++) // j est la valeur probable du premier dé
				{
					// le deuxième dé
					for(int k=0;k!=i && k<5;k++ ) // k est l'index du deuxième dé dans la main 
					{
						for(int m =1; m<6; m++) // m est la valeur du deuxième dé 
						{
							int proba = 1;
							int[] myhand = new int[6];
							_myhandtest[i] = j;
							_myhandtest[k] = m;
						    points = PointCount(_myhandtest);

							if (_enemypoints < points )
							{
								_myhand[i] = 0;
								_myhand[k] = 0;
								for (int z = 0; z < 5; z++)
								{
									myhand[z] = _myhandtest[z];
								}
								myhand[6] = proba;
								handAndProba[place] = myhand;
								place++;
							}
						}
					}
				}
			}

			// pour 3 dés
			// 1er dé
			for (int i = 0; i < 5; i++) // i index du dé 1
			{
				int[] _myhandtest = _myhand;
				for (int j = 1; j < 6; j++) // j valeur du dé 1
				{
					// le deuxième dé
					for (int k = 0; k != i && k < 5; k++) // k index du dé 2
					{
						for (int l = 1; l < 6; l++) // l valeur du dé 1 
						{
							// le troisième dé
							for (int m = 0; m != i && m < 5; m++) // m index du dé 3 
							{
								for (int n = 1; n < 6; n++) // n valeur du dé 1 
								{
									int[] myhand = new int[6];
									int proba = 1;

									_myhandtest[i] = j;
									_myhandtest[k] = l;
									_myhandtest[m] = n;
									points = PointCount(_myhandtest);
									if (_enemypoints < points)
									{
										_myhand[i] = 0;
										_myhand[k] = 0;
										_myhand[m] = 0;
										for (int z = 0; z < 5; z++)
										{
											myhand[z] = _myhandtest[z];
										}
										myhand[6] = proba;
										handAndProba[place] = myhand;
										place++;
									}
								}
							}
						}
					}
				}
			}

			// pour 4 dés
			// 1er dé
			for (int i = 0; i < 5; i++) // i index du dé 1
			{
				int[] _myhandtest = _myhand;
				for (int j = 1; j < 6; j++) // j valeur du dé 1
				{
					// le deuxième dé
					for (int k = 0; k != i && k < 5; k++) // k index du dé 2
					{
						for (int l = 1; l < 6; l++) // l valeur du dé 2
						{
							// le troisième dé
							for (int m = 0;m!=k && m != i && m < 5; m++) // m index du dé 3 
							{
								for (int n = 1; n < 6; n++) // n valeur du dé 3 
								{
									// le quatrième dé
									for (int p = 0;p!=k && p!=m && p != i && p < 5; p++) // p index du dé 4
									{
										for (int q = 1; q < 6; q++) // q valeur du dé 4 
										{
											_myhandtest[i] = j;
											_myhandtest[k] = l;
											_myhandtest[m] = n;
											_myhandtest[p] =q;

											int proba = 9;
											int[] myhand = new int[6]; 
											points = PointCount(_myhandtest);
											if (points > _enemypoints)
											{
												_myhand[i] = 0;
												_myhand[k] = 0;
												_myhand[m] = 0;
												_myhand[p] = 0;

												for (int z = 0; z < 5; z++)
												{
													myhand[z] = _myhandtest[z];
												}
												myhand[6] = proba;
												handAndProba[place] = myhand;
												place++;
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// pour 5 dés
			// 1er dé
			for (int i = 0; i < 5; i++) // i index du dé 1
			{
				int[] _myhandtest = _myhand;
				for (int j = 1; j < 6; j++) // j valeur du dé 1
				{
					// le deuxième dé
					for (int k = 0; k != i && k < 5; k++) // k index du dé 2
					{
						for (int l = 1; l < 6; l++) // l valeur du dé 2
						{
							// le troisième dé
							for (int m = 0; m != k && m != i && m < 5; m++) // m index du dé 3 
							{
								for (int n = 1; n < 6; n++) // n valeur du dé 3 
								{
									// le quatrième dé
									for (int p = 0; p != k && p != m && p != i && p < 5; p++) // p index du dé 4
									{
										for (int q = 1; q < 6; q++) // q valeur du dé 4 
										{
											// le cinquième dé
											for (int r = 0;r!=p && r != k && r != m && r != i && r < 5; r++) // p index du dé 5
											{
												for (int s = 1; s < 6; s++) // s valeur du dé 5 
												{
													_myhandtest[i] = j;
													_myhandtest[k] = l;
													_myhandtest[m] = n;
													_myhandtest[p] = q;
													_myhandtest[r] = s;
													int[] myhand = new int[6];
													int proba = 1;
													points = PointCount(_myhandtest);
													if (points > _enemypoints)
													{
														_myhand[i] = 0;
														_myhand[k] = 0;
														_myhand[m] = 0;
														_myhand[p] = 0;
														_myhand[r] = 0;

														for (int z = 0; z < 5; z++)
														{
															myhand[z] = _myhandtest[z];
														}
														myhand[6] = proba;
														handAndProba[place] = myhand;
														place++;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return handAndProba;
		}

		private int[] ChooseHand()
		{
			int[][] LECACA = ToRerollHands();
			List<int[]> Lepipi = new List<int[]>();
			for (int i = 0; i < LECACA.Length; i++)
			{
				LECACA[i] = Lepipi[i];
			}

			for(int i = 0; i<Lepipi.Count-1;i++)
			{
				if(Lepipi[0][5]<Lepipi[1][5])
				{
					Lepipi[0] = Lepipi[1];
				}
				Lepipi.Remove(Lepipi[1]);
			}
			int[] tab = new int[5];
			for(int i = 0;i<5;i++)
			{
				tab[i] = Lepipi[0][i];
			}
			return tab;
		}

		#region Inutile
		private void SortHand(int[] hand) // Décroissant
		{
			bool tableenordre = false;
			int taille = hand.Length - 1;

			while (!tableenordre)
			{
				tableenordre = true;
				for (int i = 0; i < taille; i++)
				{
					if (hand[i] < hand[i + 1])
					{
						int stock = hand[i];
						hand[i] = hand[i + 1];
						hand[i + 1] = stock;
						tableenordre = false;
					}
				}
				taille--;
			}
		}

		private void FillPointArray()
		{
			int[] yams6 = new int []{ 6, 6, 6, 6, 6, 80};
			_pointstab[0] = yams6;

			int[] yams5 = new int[] { 5, 5, 5, 5, 5, 75};
			_pointstab[1] = yams5;

			int[] yams4 = new int[] { 4, 4, 4, 4, 4, 70};
			_pointstab[2] = yams4;

			int[] carre6_5 = new int[] { 6, 6, 6, 6, 5, 69};
			_pointstab[3] = carre6_5;

			int[] carre6_4 = new int[] { 6, 6, 6, 6, 4, 68};
			_pointstab[4] = carre6_4;

			int[] carre6_3 = new int[] { 6, 6, 6, 6, 3, 67};
			_pointstab[5] = carre6_3;

			int[] carre6_2 = new int[] { 6, 6, 6, 6, 2, 66 };
			_pointstab[6] = carre6_2;

			int[] carre5_6 = new int[] { 5, 5, 5, 5, 6, 66 };
			_pointstab[7] = carre5_6;

			int[] carre6_1 = new int[] { 6, 6, 6, 6, 1, 65 };
			_pointstab[8] = carre6_1;

			int[] yams3 = new int[] { 3, 3, 3, 3, 3, 65 };
			_pointstab[9] = yams3;

			int[] carre5_4 = new int[] { 5, 5, 5, 5, 6, 64 };
			_pointstab[10] = carre5_4;

			int[] carre5_3 = new int[] { 5, 5, 5, 5, 3, 63 };
			_pointstab[11] = carre5_3;

			int[] carre5_2 = new int[] { 5, 5, 5, 5, 2, 62 };
			_pointstab[12] = carre5_2;

			int[] carre4_6 = new int[] { 4, 4, 4, 4, 6, 62 };
			_pointstab[13] = carre4_6;

			int[] carre5_1 = new int[] { 5, 5, 5, 5, 1, 61 };
			_pointstab[14] = carre5_1;

			int[] carre4_5 = new int[] { 4, 4, 4, 4, 5, 61 };
			_pointstab[15] = carre4_5;

			int[] yams2 = new int[] { 2, 2, 2, 2, 2, 60};
			_pointstab[16] = yams2;

			int[] carre4_3 = new int[] { 4, 4, 4, 4, 3, 59 };
			_pointstab[17] = carre4_3;

			int[] carre4_2 = new int[] { 4, 4, 4, 4, 2, 58 };
			_pointstab[18] = carre4_2;

			int[] carre3_6 = new int[] { 3, 3, 3, 3, 6, 58 };
			_pointstab[19] = carre3_6;

			int[] Full_t6d5 = new int[] { 6, 6, 6, 5, 5, 58 };
			_pointstab[20] = Full_t6d5;

			int[] carre4_1 = new int[] { 4, 4, 4, 4, 1, 57};
			_pointstab[21] = carre4_1;

			int[] carre3_5 = new int[] { 3, 3, 3, 3, 5, 57 };
			_pointstab[22] = carre3_5;

			int[] Full_t5d6 = new int[] { 5, 5, 5, 6, 6, 57 };
			_pointstab[23] = Full_t5d6;

			int[] carre3_4 = new int[] { 3, 3, 3, 3, 4, 56 };
			_pointstab[24] = carre3_4;

			int[] Full_t6d4 = new int[] { 6, 6, 6, 4, 4, 56};
			_pointstab[25] = Full_t6d4;

			int[] yams1 = new int[] { 1, 1, 1, 1, 1, 55};
			_pointstab[26] = yams6;

			int[] carre3_2 = new int[] { 3, 3, 3, 3, 2, 54 };
			_pointstab[27] = carre3_2;

			int[] Full_t6d3 = new int[] { 6, 6, 6, 3, 3, 54 };
			_pointstab[28] = Full_t6d3;

			int[] carre2_6 = new int[] { 2, 2, 2, 2, 6, 54 };
			_pointstab[29] = carre2_6;

			int[] Full_t4d6 = new int[] { 4, 4, 4, 6, 6, 54 };
			_pointstab[30] = Full_t4d6;

			int[] carre3_1 = new int[] { 3, 3, 3, 3, 1, 53 };
			_pointstab[31] = carre3_1;

			int[] carre2_5 = new int[] { 2, 2, 2, 2, 5, 53 };
			_pointstab[32] = carre2_5;

			int[] Full_t5d4 = new int[] { 5, 5, 5, 4, 4, 53 };
			_pointstab[33] = Full_t5d4;

			int[] carre2_4 = new int[] { 2, 2, 2, 2, 4, 52 };
			_pointstab[34] = carre2_4;

			int[] carre1_6 = new int[] { 1, 1, 1, 1, 6, 52 };
			_pointstab[35] = carre1_6;

			int[] Full_t6d2 = new int[] { 6, 6, 6, 2, 2, 52 };
			_pointstab[36] = Full_t6d2;

			int[] Full_t4d5 = new int[] { 4, 4, 4, 5, 5, 52 };
			_pointstab[37] = Full_t4d5;

			int[] carre2_3 = new int[] { 2, 2, 2, 2, 3, 51 };
			_pointstab[38] = carre2_3;

			int[] carre1_5 = new int[] { 1, 1, 1, 1, 5, 51 };
			_pointstab[39] = carre1_5;

			int[] Full_t5d3 = new int[] { 5, 5, 5, 3, 3, 51 };
			_pointstab[40] = Full_t5d3;

			int[] Full_t3d6 = new int[] { 3, 3, 3, 6, 6, 51 };
			_pointstab[41] = Full_t3d6;

			int[] carre1_4 = new int[] { 1, 1, 1, 1, 4, 50 };
			_pointstab[42] = carre1_4;

			int[] Full_t6d1 = new int[] { 6, 6, 6, 1, 1, 50 };
			_pointstab[43] = Full_t6d1;

			int[] G_Suite = new int[] { 6,5,4,3,2};
			_pointstab[44] = G_Suite;

			int[] carre2_1 = new int[] { 2, 2, 2, 2, 1, 49 };
			_pointstab[45] = carre2_1;

			int[] carre1_3 = new int[] { 1, 1, 1, 1, 3, 49 };
			_pointstab[46] = carre1_3;

			int[] Full_t5d2 = new int[] { 5, 5, 5, 2, 2, 49 };
			_pointstab[47] = Full_t5d2;

			int[] Full_t3d5 = new int[] { 3, 3, 3, 5, 5, 49 };
			_pointstab[48] = Full_t3d5;

			int[] carre1_2 = new int[] { 1, 1, 1, 1, 2, 48};
			_pointstab[49] = carre1_2;

			int[] Full_t4d3 = new int[] { 4, 4, 4, 3, 3, 48 };
			_pointstab[50] = Full_t4d3;

			int[] Full_t2d6 = new int[] { 2, 2, 2, 6, 6, 48 };
			_pointstab[51] = Full_t2d6;

			int[] Full_t5d1 = new int[] { 5, 5, 5, 1, 1, 47 };
			_pointstab[52] = Full_t5d1;

			int[] Full_t3d4 = new int[] { 3, 3, 3, 4, 4, 47 };
			_pointstab[53] = Full_t3d4;

			int[] Full_t4d2 = new int[] { 4, 4, 4, 2, 2, 46 };
			_pointstab[54] = Full_t4d2;

			int[] Full_t2d5 = new int[] { 2, 2, 2, 5, 5, 46 };
			_pointstab[55] = Full_t2d5;

			int[] Full_t1d6 = new int[] { 1, 1, 1, 6, 6, 45 };
			_pointstab[56] = Full_t1d6;

			int[] P_Suite = new int[] {5, 4, 3, 2, 1 };
			_pointstab[57] = P_Suite;

			int[] Full_t4d1 = new int[] { 4, 4, 4, 1, 1, 44 };
			_pointstab[58] = Full_t4d1;

			int[] Full_t2d4 = new int[] { 2, 2, 2, 4, 4, 44 };
			_pointstab[59] = Full_t2d4;

			int[] Full_t1d5 = new int[] { 1, 1, 1, 5, 5, 43 };
			_pointstab[60] = Full_t1d5;

			int[] Full_t3d2 = new int[] { 3, 3, 3, 2, 2, 43 };
			_pointstab[61] = Full_t3d2;

			int[] Full_t2d3 = new int[] { 2, 2, 2, 3, 3, 42 };
			_pointstab[62] = Full_t2d3;

			int[] Full_t3d1 = new int[] { 3, 3, 3, 1, 1, 41 };
			_pointstab[63] = Full_t3d1;

			int[] Full_t1d4 = new int[] { 1, 1, 1, 4, 4, 41 };
			_pointstab[64] = Full_t1d4;

			int[] Full_t1d3 = new int[] { 1, 1, 1, 3, 3, 39 };
			_pointstab[65] = Full_t1d3;

			int[] Full_t2d1 = new int[] { 2, 2, 2, 1, 1, 38 };
			_pointstab[66] = Full_t2d1;

			int[] Full_t1d2 = new int[] { 1, 1, 1, 2, 2, 37 };
			_pointstab[67] = Full_t1d2;
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
		#endregion

		private int PointCount(int[] hand)
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
    }
}
