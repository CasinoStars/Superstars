using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Services
{
	public class RankService
	{
		List<int> _profit = new List<int>();
		List<string> _userName = new List<string>();

		public RankService(List<int> profit, List<string> userName)
		{
			_profit = profit;
			_userName = userName;
		}

		public void TriProfitAndRank(List<int> profit, List<string> names)
		{
			int taille = profit.Count;
			bool tableenordre = false;
			while (!tableenordre)
			{
				tableenordre = true;
				for (int i = 0; i < taille; i++)
				{
					if (profit[i] > profit[i + 1])
					{
						int stock = profit[i];
						profit[i] = profit[i + 1];
						profit[i + 1] = stock;

						string stockos = names[i];
						names[i] = names[i + 1];
						names[i + 1] = stockos;

						tableenordre = false;
					}
				}
				taille--;
			}
		}
	}
}
