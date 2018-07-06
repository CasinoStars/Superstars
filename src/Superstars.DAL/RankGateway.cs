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
					"select u.UserName from sp.vUser u where u.UserName is not null order by u.UserId ASC "
					);
			}
		}

		public async Task<IEnumerable<int>> GetPlayerTrueProfitList()
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryAsync<int>(
					"select m.Profit from sp.tMoney m where m.MoneyType = 1 "
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

		public async Task<IEnumerable<int>> GetPlayersBlackJackWins()
		{
			using (SqlConnection con  = new SqlConnection(_connectionString))
			{
				return await con.QueryAsync<int>(
					"select s.Wins from sp.tStats s where GameType = 'BlackJack'"
					);
			}
		}

		public async Task<IEnumerable<int>> GetPlayersBlackJackLosses()
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryAsync<int>(
					"select s.Losses from sp.tStats s where GameType = 'BlackJack'"
					);
			}
		}

		public async Task<IEnumerable<int>> GetPlayersYamsWins()
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryAsync<int>(
					"select s.Wins from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameType = 'Yams'"
					);
			}
		}

		public async Task<IEnumerable<int>> GetPlayerYamsLosses()
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				return await con.QueryAsync<int>(
					"select s.Losses from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameType = 'Yams'"
					);
			}
		}
	}
}
