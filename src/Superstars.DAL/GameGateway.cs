using System;
using System.Collections.Generic;
using System.Text;

namespace Superstars.DAL
{
    public class GameGateway
    {
        readonly string _sqlstring;

        public GameGateway(string sqlstring)
        {
            _sqlstring = sqlstring;
        }

        public DateTime GetNowDate()
        {
            return DateTime.UtcNow;
        }
    }
}
