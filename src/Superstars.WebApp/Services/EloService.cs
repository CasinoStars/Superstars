using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superstars.WebApp.Services
{
	public class EloService
	{
		#region fields
		int _firstPlayerElo;
		int _secondPlayerElo;
		double prob_first;
		double prob_second;
		bool _winner; // true if fist player win // false if second player win

		int K;
		#endregion

		// contructor
		public EloService()
		{


		}

		private void Proba()
		{
			int d = _firstPlayerElo - _secondPlayerElo;
			prob_first = 1 / (1 + Math.Pow(10, (-d / 400)));//+d
			prob_second = 1 / (1 + Math.Pow(10, (d / 400)));//-d
		}

		private void NewElo()
		{
			Proba();
			int W_first;
			int W_second;
			if (_winner)
			{
				W_first = 1;
				W_second = 0;
				_firstPlayerElo = _firstPlayerElo + K * (W_first - (int)prob_first);
				_secondPlayerElo = _secondPlayerElo + K * (W_second - (int)prob_second);
			}
			else
			{
				W_first = 0;
				W_second = 1;
				_firstPlayerElo = _firstPlayerElo + K * (W_first - (int)prob_first);
				_secondPlayerElo = _secondPlayerElo + K * (W_second - (int)prob_second);
			}
		}

	}
}
