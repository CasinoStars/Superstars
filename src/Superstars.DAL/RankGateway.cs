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

		public async Task<IEnumerable<string>> PseudoList()
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryAsync<string>(
					"select u.UserName from sp.vUser u where u.UserName is not null "
					);
			}
		}

		public async Task<IEnumerable<int>> GetPlayerProfit(string pseudo)
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryAsync<int>(
					"select m.Profit from sp.Money m left outer join sp.User u on u.UserId = m.MoneyId  where u.UserName = @Pseudo",
					new { Pseudo = pseudo });
			}
		}
	}
}
