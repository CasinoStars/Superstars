using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class CrashGateway
    {
        private readonly SqlConnexion _sqlConnexion;

        public CrashGateway(SqlConnexion sqlConnexion)
        {
            _sqlConnexion = sqlConnexion;
        }

        public async Task<Result<int>> CreateCrashPlayer(int userId, int bet, double multi)
        {

            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@Bet", bet);
                p.Add("@Multi", multi);
              
                await con.ExecuteAsync("sp.sCrashCreate", p, commandType: CommandType.StoredProcedure);

                return Result.Success(p.Get<int>("@UserId"));
            }
        }
        public async Task<IEnumerable<CrashData>> GetGamePlayers()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryAsync<CrashData>(
                    @"select c.GameId, c.UserId, u.UserName, c.Bet, c.Multi from sp.tCrash c left join sp.tUser u on c.UserId = u.UserId where c.Gameid = (select TOP 1 GameId from sp.tGames where GameTypeId = 2 order by GameId desc)");
                return data;
            }
        }
    }
}
