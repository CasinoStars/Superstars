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
		Random rdn = new Random();
		int[] _mydices = new int[5];
		int[] _IAdices = new int[5];
		bool _IsIAturn;
		int _IAturn,_MYturn;

		public YamsController()
		{
			_IsIAturn = false;
			_IAturn = 0;
			_MYturn = 0;
			while(_MYturn<3)
			{
				FirstShot();
				IndexChange();
				Reroll();
				_MYturn++;
			}
			_IsIAturn = true;
			while(_IAturn<3)
			{
				FirstShot();
				IndexChange();
				Reroll();
				_IAturn++;

			}
		}

		public void FirstShot()
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
		
		public void Reroll()
		{
			if(_IsIAturn == false)
			for(int i = 0;i<6  ;i++ )
			{
				if(_mydices[i]==0)
				{
					_mydices[i] = RollDice();
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

		public int RollDice()
		{
			int value;
			value = rdn.Next(1, 6);   
			return value;
		}
    }
}
