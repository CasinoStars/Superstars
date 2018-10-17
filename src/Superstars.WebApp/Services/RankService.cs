using System.Collections.Generic;

namespace Superstars.WebApp.Services
{
	public class RankService
	{
		List<int> _profit = new List<int>();
		List<string> _userName = new List<string>();

		public RankService()
		{

		}

		public void TriProfitAndRank(List<int> profit, List<string> names)
		{
			int taille = profit.Count;
			bool tableenordre = false;
			while (!tableenordre)
			{
				tableenordre = true;
				for (int i = 0; i < taille-1; i++)
				{
					if (profit[i] < profit[i + 1])
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

		private void TriProfitAndGames(List<int> profit, List<int> games)
		{
			int taille = profit.Count;
			bool tableenordre = false;
			while (!tableenordre)
			{
				tableenordre = true;
				for (int i = 0; i < taille - 1; i++)
				{
					if (profit[i] < profit[i + 1])
					{
						int stock = profit[i];
						profit[i] = profit[i + 1];
						profit[i + 1] = stock;

						int stockos = games[i];
						games[i] = games[i + 1];
						games[i + 1] = stockos;

						tableenordre = false;
					}
				}
				taille--;
			}
		}

		public List<int> SortedNbGames(List<int> profit, List<int> wins,List<int> losses)
		{
			List<int> games = new List<int>();
			for(int i = 0; i<losses.Count;i++)
			{
				int lll = wins[i] + losses[i];
				games.Add(lll);
			}
			TriProfitAndGames(profit, games);
			return games;
		}
	}
}
