using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    public class YamsIAController : Controller
    {
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
         T6-D2 = 52 | T5-D2 = 51 | T4-D2 = 48 | T3-D2 = 45 | T2-D3 = 42 | T1-D3 = 39
         T6-D1 = 50 | T5-D1 = 49 | T4-D1 = 46 | T3-D1 = 43 | T2-D1 = 38 | T1-D2 = 37

         Grande suite 50 
         Petite Suite 45          
		*/

		int _enemypoints;
		int _mypoints;
		int[] _myhand; 

		public YamsIAController(int enemypoints,int mypoints,int[] myhand)
		{
			_enemypoints = enemypoints;
			_mypoints = mypoints;
			_myhand = myhand;
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

		//public RerollHand()
    }
}
