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

		public async Task<IEnumerable<int>> GetPlayerProfitList()
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryAsync<int>(
					"select m.Profit from sp.tMoney m where m.MoneyType = 2 "
					);
			}
		}

		public async Task<int> GetPlayerBlackJackWins(int userId)
		{
			using (SqlConnection con  = new SqlConnection(_connectionString))
			{
				return await con.QueryFirstOrDefaultAsync<int>(
					"select s.Wins from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameType = 'BlackJack' and u.UserId = @userName",
					new { userName = userId }
					);
			}
		}

		public async Task<int> GetPlayerBlackJackLosses(int userId)
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryFirstOrDefaultAsync<int>(
					"select s.Losses from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameType = 'BlackJack' and u.UserId = @userName",
					new { userName = userId }
					);
			}
		}

		public async Task<int> GetPlayerYamsWins(int userId)
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryFirstOrDefaultAsync<int>(
					"select s.Wins from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameType = 'Yams' and u.UserName = @userName",
					new { userName = userId}
					);
			}
		}

		public async Task<int> GetPlayerYamsLosses(int userId)
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryFirstOrDefaultAsync<int>(
					"select s.Losses from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameType = 'Yams' and u.UserName = @userName",
					new { userName = userId}
					);
			}
		}
	}
}
