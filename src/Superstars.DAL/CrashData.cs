using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    public class CrashData
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Bet { get; set; }
        public float Multi { get; set; }
        public int MoneyTypeId { get; set; }
        public string CrashHash { get; set; }
        public int CrashValue { get; set; }


        
    }
}
