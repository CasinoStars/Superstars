using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class RankGateway
    {
        private readonly string _connectionString;


        public RankGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<string>> PseudoList()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<string>(
                    "select u.UserName from sp.vUser u where u.UserName is not null and u.UserName not like '#%' order by u.UserId ASC "
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerTrueProfitList()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<int>(
                    "select m.Profit from sp.tMoney m where m.MoneyType = 1 "
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerProfitList()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<int>(
                    "select m.Profit from sp.tMoney m where m.MoneyType = 2 "
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayersBlackJackWins()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Wins from sp.tStats s where GameType = 'BlackJack'"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayersBlackJackLosses()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Losses from sp.tStats s where GameType = 'BlackJack'"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayersYamsWins()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Wins from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameType = 'Yams'"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerYamsLosses()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Losses from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameType = 'Yams'"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerYamsBets(int userid)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<IEnumerable<int>>(
                    "select s.Pot from sp.tGameYams s left outer join sp.tUser u on s.UserId = @UserId where GameType = 'Yams'",
                    new {UserId = userid});
            }
        }

        public async Task<IEnumerable<int>> GetPlayerBJBets(int userid)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<IEnumerable<int>>(
                    "select s.Pot from sp.tGameBlackJack s left outer join sp.tUser u on s.UserId = @UserId where GameType = 'BlackJack'",
                    new {UserId = userid});
            }
        }
    }
}