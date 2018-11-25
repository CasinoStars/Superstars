using System;

namespace Superstars.DAL
{
    public class GameData
    {
        public int GameId { get; set; }

        public int GameTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Winner { get; set; }
    }
}