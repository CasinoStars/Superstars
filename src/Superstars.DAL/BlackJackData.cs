using System.Collections.Generic;


namespace Superstars.DAL
{
    public class BlackJackData
    {
        public int BlackJackPlayerID { get; set; }

        public int BlackJackGameId { get; set; }

        public string PlayerCards { get; set; }

        public string SecondPlayerCards { get; set; }

        public int NbTurn { get; set; }

        public int HandValue { get; set; }

        public int SecondHandValue { get; set; }

    }
}
