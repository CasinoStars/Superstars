using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class RankGateway
    {
        private readonly SqlConnexion _sqlConnexion;


        public RankGateway(SqlConnexion sqlConnexion)
        {
            _sqlConnexion = sqlConnexion;
        }

        public async Task<IEnumerable<RankData>> GetPlayersGlobalProfit(int moneyTypeId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryAsync<RankData>(
                    @"select u.UserName, SUM(s.Profit) as Profit from sp.tUser u left join sp.tStats s on s.UserId = u.UserId where s.MoneyTypeId = @TypeId group by u.UserName order by Profit desc",
                    new { TypeId = moneyTypeId }
                    );
                return data;
            }
        }

        public async Task<IEnumerable<RankData>> GetPlayerStats(string pseudo, int moneyTypeId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryAsync<RankData>(
                @"select u.UserName, g.GameName, s.Profit, s.TotalBet, s.Wins, s.Losses, s.Equality, s.AverageTime from sp.vStats s 
                inner join sp.vUser u on u.UserName = @Pseudo
                inner join sp.vGameType g on g.GameTypeId = s.GameTypeId
                inner join sp.vMoneyType m on m.MoneyTypeId = @TypeId
                where s.UserId = u.UserId and s.MoneyTypeId = m.MoneyTypeId",
                new { TypeId = moneyTypeId, Pseudo = pseudo }
                );
                return data;
            }
        }

        public async Task<IEnumerable<string>> PseudoList()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<string>(
                    "select u.UserName from sp.vUser u where u.UserName is not null and u.UserName not like '#%' order by u.UserId ASC "
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerTrueProfitList()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select m.Profit from sp.tMoney m where m.MoneyTypeId = 1 "
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerProfitList()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select m.Profit from sp.tMoney m where m.MoneyTypeId = 0 "
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayersBlackJackWins()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Wins from sp.tStats s where GameTypeId = 1"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayersBlackJackLosses()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Losses from sp.tStats s where GameTypeId = 1"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayersYamsWins()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Wins from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameTypeId = 0"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerYamsLosses()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Losses from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameTypeId = 0"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerYamsBets(int userid)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select a.Pot from sp.tYamsPlayer y inner join sp.tGames as g on y.YamsGameId = g.GameId left join sp.tGameYams as a on a.YamsGameId = g.GameId where g.GameTypeId = 0 and y.YamsPlayerId = @UserId",
                    new {UserId = userid});
            }
        }

        public async Task<IEnumerable<int>> GetPlayerBJBets(int userid)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select a.Pot from sp.tBlackJackPlayer b inner join sp.tGames g on b.BlackJackGameId = g.GameId left join sp.tGameBlackJack a on a.BlackJackGameId = g.GameId where g.GameTypeId = 1 and b.BlackJackPlayerId = @UserId",
                    new {UserId = userid});
            }
        }

        public async Task<IEnumerable<GameData>> GetGames(int userid, int gametypeId)
        {
            if (gametypeId == 0)
            {
                using (var con = new SqlConnection(_sqlConnexion.connexionString))
                {
                    return await con.QueryAsync<GameData>(
                        "select g.GameId, g.GameTypeId, g.StartDate, g.EndDate, g.Winner from sp.tGames g inner join sp.tYamsPlayer p on p.YamsGameId = g.GameId where g.GameTypeId = 0 and p.YamsPlayerId = @UserId and g.EndDate is not null",
                        new { UserId = userid });
                }
            } else
            {
                using (var con = new SqlConnection(_sqlConnexion.connexionString))
                {
                    return await con.QueryAsync<GameData>(
                        "select g.GameId, g.GameTypeId, g.StartDate, g.EndDate, g.Winner from sp.tGames g inner join sp.tBlackJackPlayer p on p.BlackJackGameId = g.GameId where g.GameTypeId = 1 and p.BlackJackPlayerId = @UserId and g.EndDate is not null",
                        new { UserId = userid });
                }
            }

        }

    }
}