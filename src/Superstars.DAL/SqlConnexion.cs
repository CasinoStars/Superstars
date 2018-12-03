using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Superstars.DAL
{
    public class SqlConnexion
    {
        string _connexionString;
        public SqlConnexion(string connexionString)
        {
            _connexionString = connexionString;
        }

        public string connexionString {

            get { return _connexionString; }
        }
	}
}