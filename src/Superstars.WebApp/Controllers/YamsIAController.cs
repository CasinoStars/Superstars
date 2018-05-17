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
         T6-D4 = 56 | T5-D4 = 55 | T4-D5 = 52 | T3-D5 = 49 | T2-D5 = 46 | T1-D5 = 43
         T6-D3 = 54 | T5-D3 = 53 | T4-D3 = 50 | T3-D4 = 47 | T2-D4 = 44 | T1-D4 = 41
         T6-D2 = 52 | T5-D2 = 51 | T4-D2 = 48 | T3-D2 = 43 | T2-D3 = 42 | T1-D3 = 39
         T6-D1 = 50 | T5-D1 = 49 | T4-D1 = 46 | T3-D1 = 41 | T2-D1 = 38 | T1-D2 = 37

         Grande suite 50 
         Petite Suite 45          
		*/
		#endregion

		int _enemypoints;
		int _mypoints;
		int[] _myhand;
		int[][] _pointstab = new int[68][]; 

		public YamsIAController(int enemypoints,int mypoints,int[] myhand)
		{
			_enemypoints = enemypoints;
			_mypoints = mypoints;
			_myhand = myhand;
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
			_pointstab[0] = carre6_5;

			int[] carre6_4 = new int[] { 6, 6, 6, 6, 4, 68};
			_pointstab[0] = carre6_4;

			int[] carre6_3 = new int[] { 6, 6, 6, 6, 3, 67};
			_pointstab[0] = carre6_3;

			int[] carre6_2 = new int[] { 6, 6, 6, 6, 2, 66 };
			_pointstab[0] = carre6_2;

			int[] carre5_6 = new int[] { 5, 5, 5, 5, 6, 66 };
			_pointstab[0] = carre5_6;

			int[] carre6_1 = new int[] { 6, 6, 6, 6, 1, 65 };
			_pointstab[0] = carre6_1;

			int[] yams3 = new int[] { 3, 3, 3, 3, 3, 65 };
			_pointstab[0] = yams3;

			int[] carre5_4 = new int[] { 5, 5, 5, 5, 6, 64 };
			_pointstab[0] = carre5_4;

			int[] carre5_3 = new int[] { 5, 5, 5, 5, 3, 63 };
			_pointstab[0] = carre5_3;

			int[] carre5_2 = new int[] { 5, 5, 5, 5, 2, 62 };
			_pointstab[0] = carre5_2;

			int[] carre4_6 = new int[] { 4, 4, 4, 4, 6, 62 };
			_pointstab[0] = carre4_6;

			int[] carre5_1 = new int[] { 5, 5, 5, 5, 1, 61 };
			_pointstab[0] = carre5_1;

			int[] carre4_5 = new int[] { 4, 4, 4, 4, 5, 61 };
			_pointstab[0] = carre4_5;

			int[] yams2 = new int[] { 2, 2, 2, 2, 2, 60};
			_pointstab[0] = yams2;

			int[] carre4_3 = new int[] { 4, 4, 4, 4, 3, 59 };
			_pointstab[0] = carre4_3;

			int[] carre4_2 = new int[] { 4, 4, 4, 4, 2, 58 };
			_pointstab[0] = carre4_2;

			int[] carre3_6 = new int[] { 3, 3, 3, 3, 6, 58 };
			_pointstab[0] = carre3_6;

			int[] Full_t6d5 = new int[] { 6, 6, 6, 5, 5, 58 };
			_pointstab[0] = Full_t6d5;

			int[] carre4_1 = new int[] { 4, 4, 4, 4, 1, 57};
			_pointstab[0] = carre4_1;

			int[] carre3_5 = new int[] { 3, 3, 3, 3, 5, 57 };
			_pointstab[0] = carre3_5;

			int[] Full_t5d6 = new int[] { 5, 5, 5, 6, 6, 57 };
			_pointstab[0] = Full_t5d6;

			int[] carre3_4 = new int[] { 3, 3, 3, 3, 4, 56 };
			_pointstab[0] = carre3_4;

			int[] Full_t6d4 = new int[] { 6, 6, 6, 4, 4, 56};
			_pointstab[0] = Full_t6d4;

			int[] yams1 = new int[] { 1, 1, 1, 1, 1, 55};
			_pointstab[0] = yams6;

			int[] Full_t5d4 = new int[] { 5, 5, 5, 4, 4, 55 };
			_pointstab[0] = Full_t5d4;

			int[] carre3_2 = new int[] { 3, 3, 3, 3, 2, 54 };
			_pointstab[0] = carre3_2;

			int[] Full_t6d3 = new int[] { 6, 6, 6, 3, 3, 54 };
			_pointstab[0] = Full_t6d3;

			int[] carre2_6 = new int[] { 2, 2, 2, 2, 6, 54 };
			_pointstab[0] = carre2_6;

			int[] Full_t4d6 = new int[] { 4, 4, 4, 6, 6, 54 };
			_pointstab[0] = Full_t4d6;

			int[] carre3_1 = new int[] { 3, 3, 3, 3, 1, 53 };
			_pointstab[0] = carre3_1;

			int[] carre2_5 = new int[] { 2, 2, 2, 2, 5, 53 };
			_pointstab[0] = carre2_5;

			int[] Full_t5d3 = new int[] { 5, 5, 5, 3, 3, 53};
			_pointstab[0] = Full_t5d6;

			int[] carre2_4 = new int[] { 2, 2, 2, 2, 4, 52 };
			_pointstab[0] = carre2_4;

			int[] carre1_6 = new int[] { 1, 1, 1, 1, 6, 52 };
			_pointstab[0] = carre1_6;

			int[] Full_t6d2 = new int[] { 6, 6, 6, 2, 2, 52 };
			_pointstab[0] = Full_t6d2;

			int[] Full_t4d5 = new int[] { 4, 4, 4, 5, 5, 52 };
			_pointstab[0] = Full_t4d5;

			int[] carre2_3 = new int[] { 2, 2, 2, 2, 3, 51 };
			_pointstab[0] = carre2_3;

			int[] carre1_5 = new int[] { 1, 1, 1, 1, 5, 51 };
			_pointstab[0] = carre1_5;

			int[] Full_t5d1 = new int[] { 5, 5, 5, 1, 1, 51 };
			_pointstab[0] = Full_t5d1;

			int[] Full_t3d6 = new int[] { 3, 3, 3, 6, 6, 51 };
			_pointstab[0] = Full_t3d6;

			int[] carre1_4 = new int[] { 1, 1, 1, 1, 4, 50 };
			_pointstab[0] = carre1_4;

			int[] Full_t6d1 = new int[] { 6, 6, 6, 1, 1, 50 };
			_pointstab[0] = Full_t6d1;

			int[] carre2_1 = new int[] { 2, 2, 2, 2, 1, 49 };
			_pointstab[0] = carre2_1;

			int[] carre1_3 = new int[] { 1, 1, 1, 1, 3, 49 };
			_pointstab[0] = carre1_3;

			int[] Full_t3d5 = new int[] { 3, 3, 3, 5, 5, 49 };
			_pointstab[0] = Full_t3d5;

			int[] carre1_2 = new int[] { 1, 1, 1, 1, 2, 48};
			_pointstab[0] = carre1_2;

			int[] Full_t4d3 = new int[] { 4, 4, 4, 3, 3, 48 };
			_pointstab[0] = Full_t4d3;

			int[] Full_t3d4 = new int[] { 3, 3, 3, 4, 4, 47 };
			_pointstab[0] = Full_t3d4;

			int[] Full_t4d2 = new int[] { 4, 4, 4, 2, 2, 46 };
			_pointstab[0] = Full_t4d2;

			int[] Full_t4d1 = new int[] { 4, 4, 4, 1, 1, 44 };
			_pointstab[0] = Full_t4d1;

			int[] Full_t3d2 = new int[] { 3, 3, 3, 2, 2, 43 };
			_pointstab[0] = Full_t3d2;

			int[] Full_t3d1 = new int[] { 3, 3, 3, 1, 1, 41 };
			_pointstab[0] = Full_t3d1;
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

		public void RerollHand()
		{
			int LeCaca = 100;
		}
    }
}
