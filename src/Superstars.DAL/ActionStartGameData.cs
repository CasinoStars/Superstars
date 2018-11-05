using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    [Serializable()]
    public class ActionStartGameData
    {
        public int UserID { get; set; }

        public int GameID { get; set; }

        public DateTime StartDate { get; set; }

        public string Gametype { get; set; }

        public int Bet { get; set; }
    }
}
