using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Superstars.DAL
{
    public class RankGateway
    {
		readonly string _connectionString;


		public RankGateway(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<List<string>> PseudoList()
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryFirstOrDefaultAsync<List<string>>(
					"select u.UserName from sp.vUser u where u.UserName is not null ",
					);
			}
		}
	}
}
