using System;

namespace Superstars.WebApp.Services
{
    public class EloService
    {
        // contructor

        private void Proba()
        {
            var d = _firstPlayerElo - _secondPlayerElo;
            prob_first = 1 / (1 + Math.Pow(10, -d / 400)); //+d
            prob_second = 1 / (1 + Math.Pow(10, d / 400)); //-d
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
                _firstPlayerElo = _firstPlayerElo + K * (W_first - (int) prob_first);
                _secondPlayerElo = _secondPlayerElo + K * (W_second - (int) prob_second);
            }
            else
            {
                W_first = 0;
                W_second = 1;
                _firstPlayerElo = _firstPlayerElo + K * (W_first - (int) prob_first);
                _secondPlayerElo = _secondPlayerElo + K * (W_second - (int) prob_second);
            }
        }

        #region fields

        private int _firstPlayerElo;
        private int _secondPlayerElo;
        private double prob_first;
        private double prob_second;
        private bool _winner; // true if fist player win // false if second player win

        private int K;

        #endregion
    }
}