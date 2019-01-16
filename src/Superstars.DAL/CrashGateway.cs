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

        public async Task<Result<int>> CreateCrashPlayer(int userId, int bet, double multi, int MoneyTypeId)
        {

            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@Bet", bet);
                p.Add("@Multi", multi);
                p.Add("@MoneyTypeId", MoneyTypeId);
              
                await con.ExecuteAsync("sp.sCrashCreate", p, commandType: CommandType.StoredProcedure);

                return Result.Success(p.Get<int>("@UserId"));
            }
        }

        public async Task<Result<int>>UpdateCrashPlayer(int userId,double multi)
        {

            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@Multi", multi);

                await con.ExecuteAsync("sp.sCrashUpdate", p, commandType: CommandType.StoredProcedure);

                return Result.Success(p.Get<int>("@UserId"));
            }
        }

        public async Task<Result<int>> CreateCrashGame(string crashHash, double multi)
        {

            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@CrashHash", crashHash);
                p.Add("@CrashValue", multi);

                await con.ExecuteAsync("sp.sGameCrashCreate", p, commandType: CommandType.StoredProcedure);

                return Result.Success(0);
            }
        }

        public async Task<Result<int>> UpdateCrashGame(string crashHash, double multi)
        {
            try
            {
                using (var con = new SqlConnection(_sqlConnexion.connexionString))
                {
                    var p = new DynamicParameters();
                    p.Add("@CrashHash", crashHash);
                    p.Add("@CrashValue", multi);

                    var lol = await con.ExecuteAsync("sp.sGameCrashUpdate", p, commandType: CommandType.StoredProcedure);

                    return Result.Success(0);
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CrashData>> GetGamePlayers()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryAsync<CrashData>(
                    @"select c.GameId, c.UserId, u.UserName, c.Bet, c.Multi, c.MoneyTypeId from sp.tCrash c left join sp.tUser u on c.UserId = u.UserId where c.Gameid = (select TOP 1 GameId from sp.tGameCrash order by GameId desc)");
                return data;
            }
        }

        public async Task<IEnumerable<CrashData>> GetGamePlayers(int gameId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryAsync<CrashData>(
                    @"select c.GameId, c.UserId, u.UserName, c.Bet, c.Multi, c.MoneyTypeId from sp.tCrash c left join sp.tUser u on c.UserId = u.UserId where c.Gameid = @GameId", new {GameId = gameId});
                return data;
            }
        }

        public async Task<CrashData> GetPlayerByGame(int userId, int gameId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<CrashData>(
                    @"select c.GameId, c.UserId, u.UserName, c.Bet, c.Multi, c.MoneyTypeId from sp.tCrash c left join sp.tUser u on c.UserId = u.UserId where c.Gameid = @GameId and c.UserId = @UserId", new{GameId = gameId, UserId = userId});
                return data;
            }
        }

        public async Task<IEnumerable<CrashData>> GetTenLastGame(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {              
                var data = await con.QueryAsync<CrashData>(
                    @"select TOP 10 g.GameId, g.CrashHash, g.CrashValue, c.Bet, c.Multi, c.MoneyTypeId from sp.tGameCrash g left outer join sp.tCrash c on c.gameId = g.gameId and c.UserId = @UserId where g.CrashValue != 0 order by g.GameId desc", new {  UserId = userId });
                return data;
            }
        }

        public async Task<CrashMeta> GetGameCrashMeta(int gameId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<CrashMeta>(
                    @"select g.GameId, g.StartDate, g.EndDate, c.CrashHash, c.CrashValue from sp.tGames g left join sp.tGameCrash c on g.GameId = c.GameId where g.GameId = @GameId; ", new { GameId = gameId});
                return data;
            }
        }

    }
}
