using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    [Serializable()]
    public class ActionEndGameData
    {
        public int UserID { get; set; }

        public int GameID { get; set; }

        public DateTime EndDate { get; set; }

        public string Gametype { get; set; }

        public int Bet { get; set; }

        public string HasWin { get; set; }
    }
}
