using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    public class CrashMeta
    {
        public int GameId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CrashHash { get; set; }
        public float CrashValue { get; set; }
    }
}
