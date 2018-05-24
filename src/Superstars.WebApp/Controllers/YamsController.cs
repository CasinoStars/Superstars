using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superstars.DAL;
using Superstars.WebApp.Authentication;
using System.Threading.Tasks;
using Superstars.WebApp.Models;
using System.Collections.Generic;
using System;

namespace Superstars.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class YamsController : Controller
    {
        readonly YamsGateway _yamsGateway;
        readonly UserGateway _userGateway;

        public YamsController(YamsGateway yamsGateway, UserGateway userGateway)
        {
            _yamsGateway = yamsGateway;
            _userGateway = userGateway;

        }

        [HttpPost("{pseudo}/{myDices}/{selectedDices}")]
        public async Task<IActionResult> RollDices(string pseudo, int[] myDices, int[] selectedDices = null)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            YamsData data = await _yamsGateway.GetPlayer(user.UserId);
            data.NbrRevives = data.NbrRevives + 1;
            int[] secondarray = new int[5];
            string dices = data.Dices;
            for (int i = 0; i < 5; i++)
            {
                secondarray[i] = (int)char.GetNumericValue(dices[i]);
            }
            myDices = secondarray;
            myDices = _yamsGateway.IndexChange(myDices, selectedDices);
            myDices = _yamsGateway.Reroll(myDices);
            string des = null;
            for (int i = 0; i < myDices.Length; i++)
            {
                des += myDices[i];
            }
            Result result = await _yamsGateway.UpdateYamsPlayer(user.UserId, data.YamsGameId, data.NbrRevives, des, data.DicesValue);
            return this.CreateResult(result);
        }

        [HttpGet("{pseudo}")]
        public async Task<IActionResult> GetPlayerDices(string pseudo)
        {
            UserData user = await _userGateway.FindByName(pseudo);
            Result<string> result = await _yamsGateway.GetPlayerDices(user.UserId);
            return this.CreateResult(result);
        }

		private int PointCount(int[] hand)
		{
			int[] handcount = new int[5];
			int points = 0;
			int grandesuite = 0;

			handcount = DicesValue(hand);
			for (int i=0 ; i<5 ; i++)
			{
				//yams
				if (handcount[i]==5)
				{
					points = points +50;
					points = 5 * (i + 1);
					return points;
				}

				//carré
				if(handcount[i]==4)
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
				else if(handcount[i]==3)
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
}

        [HttpPost("{pseudo}")]
        public async Task<IActionResult> CreateYamsPlayer(string pseudo)
        {
            Result result = await _yamsGateway.CreateYamsPlayer(pseudo, 0, "12345", 0);
            return this.CreateResult(result);
        }