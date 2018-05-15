using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Controllers
{
	[Route("api/[controller]")]
    public class YamsController : Controller
    {
		#region Champs
		Random rdn = new Random();
		int[] _mydices = new int[5];
		int[] _IAdices = new int[5];
		int _IApoints,_MYpoints;
		bool _IsIAturn;
		int _IAturn,_MYturn;
		#endregion

		public YamsController()
		{
			_IsIAturn = false;
			_IAturn = 0;
			_MYturn = 0;
			while(_MYturn<3)
			{
				FirstShot();
				//IndexChange();
				Reroll();
				_MYturn++;
			}
			_IsIAturn = true;
			while(_IAturn<3)
			{
				FirstShot();
				//IndexChange();
				Reroll();
				_IAturn++;
			}
			_MYpoints = PointCount(_mydices);
			_IApoints = PointCount(_IAdices);
			FindWinner();
		}

		#region méthodes
		private void FirstShot()
		{
			if(_IsIAturn==true)
			{
				for (int i = 0; i < 6; i++)
				{
					_IAdices[i] = RollDice();
				}
			}
		    else if(_IsIAturn== false)
			{
				for (int i = 0; i < 6; i++)
				{
					_mydices[i] = RollDice();
				}
			}
		}
	
	    private void IndexChange(int[] index)
		{
			if(_IsIAturn==true)
			{
				for (int i = 0; i < index.Length; i++)
				{
					_IAdices[index[i]] = 0;
				}
			}
			else
			{
				for (int i = 0; i < index.Length; i++)
				{
					_mydices[index[i]] = 0;
				}
			}
		}
		
		private void Reroll()
		{
			if (_IsIAturn == false)
			{
				for (int i = 0; i < 6; i++)
				{
					if (_mydices[i] == 0)
					{
						_mydices[i] = RollDice();
					}
				}
			}
			else
			{
				for (int i = 0; i < 6; i++)
				{
					if (_IAdices[i] == 0)
					{
						_IAdices[i] = RollDice();
					}
				}
			}
		}

		private int RollDice()
		{
			int value;
			value = rdn.Next(1, 6);   
			return value;
		}

		private int[] DicesValue(int[] hand)
		{
			int[] count = new int[5] {0,0,0,0,0};
			for (int i=0; i<5;i++)
			{
				for (int z = 1; z <= 6; z++)
				{
					if (hand[i] == z) count[z - 1]++; 
				}
			}
			return count;
		}

		private int PointCount(int[] hand)
		{
			int[] handcount = new int[5];
			int points = 0;
			int grandesuite = 0;

			handcount = DicesValue(hand);
			for (int i = 0 ; i < 5 ; i++)
			{
				//yams
				if (handcount[i]==5)
				{
					points = points +50;
					points = 5 * (i + 1);
					return points;
				}

				//carré
				if( handcount[i] == 4)
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
				else if( handcount[i] == 3 )
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
				else if(handcount[i]==1)
				{
					grandesuite++;
				}
			}
			// petite suite
			if(handcount[0]==1)
			{
				if ((handcount[1] == 1) && (handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1))
				{
					points = points + 45;
				}
			}
			else if(handcount[1]==1)
			{
				if ((handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1) && (handcount[5] == 1))
				{
					points = points + 45;
				}
			}

			// grande suite
			if(grandesuite == 5)
			{
				points = points + 50;
			}

			// chance
			if (points == 0)
			{
				for (int i = 1; i < 6; i++)
				{
					points = points + handcount[i-1] * i;
				}
			}

			return points;
		}

		private string FindWinner()
		{
			if(_IApoints<_MYpoints)
			{
				return "You Win";
			}
			else if (_IApoints>_MYpoints)
			{
				return "You Lost";
			}
			else
			{
				return "Draw";
			}
		}
    }
	#endregion
}
