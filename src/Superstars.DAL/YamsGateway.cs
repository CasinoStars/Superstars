using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    class YamsGateway
    {
		readonly string _connectionString;

		public YamsGateway(string connectionString)
		{
			_connectionString = connectionString;
		}
    }
}
