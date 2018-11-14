using System;

namespace Superstars.DAL
{
    public class GameData
    {
        public int GameId { get; set; }

        public int UserId { get; set; }

        public string GameType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ResultWinner { get; set; }
    }
}