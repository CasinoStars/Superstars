using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
    public class YamsIAController : Controller
    {
		// Points = 
		// Yams : 50 + 5 * value
		// Carré :  40 + 4*value1 + 1*value2
		// Full : 30 + 3*value1 + 2*value2
		// Grande Suite : 50
		// Petite Suite : 45
		// Chance : nb1 + nb2 * 2 + nb3 * 3 + nb4 * 4 + nb5 * 5 + nb6 * 6

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
