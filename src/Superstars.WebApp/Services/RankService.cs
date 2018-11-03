using System.Collections.Generic;

namespace Superstars.WebApp.Services
{
    public class RankService
    {
        private List<int> _profit = new List<int>();
        private List<string> _userName = new List<string>();

        public void TriProfitAndRank(List<int> profit, List<string> names)
        {
            var taille = profit.Count;
            var tableenordre = false;
            while (!tableenordre)
            {
                tableenordre = true;
                for (var i = 0; i < taille - 1; i++)
                    if (profit[i] < profit[i + 1])
                    {
                        var stock = profit[i];
                        profit[i] = profit[i + 1];
                        profit[i + 1] = stock;

                        var stockos = names[i];
                        names[i] = names[i + 1];
                        names[i + 1] = stockos;

                        tableenordre = false;
                    }

                taille--;
            }
        }

        private void TriProfitAndGames(List<int> profit, List<int> games)
        {
            var taille = profit.Count;
            var tableenordre = false;
            while (!tableenordre)
            {
                tableenordre = true;
                for (var i = 0; i < taille - 1; i++)
                    if (profit[i] < profit[i + 1])
                    {
                        var stock = profit[i];
                        profit[i] = profit[i + 1];
                        profit[i + 1] = stock;

                        var stockos = games[i];
                        games[i] = games[i + 1];
                        games[i + 1] = stockos;

                        tableenordre = false;
                    }

                taille--;
            }
        }

        public List<int> SortedNbGames(List<int> profit, List<int> wins, List<int> losses)
        {
            var games = new List<int>();
            for (var i = 0; i < losses.Count; i++)
            {
                var lll = wins[i] + losses[i];
                games.Add(lll);
            }

            TriProfitAndGames(profit, games);
            return games;
        }
    }
}