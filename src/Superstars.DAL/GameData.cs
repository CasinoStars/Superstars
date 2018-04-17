using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    class GameData
    {
        public int GameID { get; set; }

        public int[] PlayersID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ResultWinner { get; set; }

        //public gametype GameType { get; set; }

    }
}
