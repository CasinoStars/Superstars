
using System;

namespace Superstars.DAL
{
    public class LogData
    {
        public int LogId { get; set; }

        public int UserId { get; set; }

        public string ActionDate { get; set; }
        
        public string ActionDescription { get; set; }
    }
}
