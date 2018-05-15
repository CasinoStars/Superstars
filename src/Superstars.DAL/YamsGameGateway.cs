using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    class YamsGameGateway
    {
		readonly string _connectionString;

		public YamsGameGateway(string connectionString)
		{
			_connectionString = connectionString;
		}

		//public async Task<IEnumerable<YamsData>>GetClassement()
		//{
		//	using (SqlConnection con = new SqlConnection(_connectionString))
		//	{
		//		return await con.QueryAsync<>(
		// 	}
		//}
    }
}
