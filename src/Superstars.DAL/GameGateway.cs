using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Superstars.DAL
{
    public class GameGateway
    {
        readonly string _sqlstring;


        public GameGateway(string sqlstring)
        {
            _sqlstring = sqlstring;
        }

        //public DateTime GetNowDate()
        //{
        //    return DateTime.UtcNow;
        //}
        public async Task<GameData> FindGameById(int GameID)
        {
            using (SqlConnection con = new SqlConnection(_sqlstring))
            {
                return await con.QueryFirstOrDefaultAsync<GameData> (
                    "select g.GameId, g.GameType, g.StartDate, g.EndDate, g.Winner from vGames g where g.GameId = @GameId",
                    new { GameId = GameID });
            }
        }

        public async Task<Result<int>> CreateGame(GamesTypes game)
        {
            using (SqlConnection con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@GameType", (int)game);
                p.Add("@StartDate", DateTime.UtcNow);
                p.Add("@GameId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sGamesCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A game with this gametype and  start date already exists.");

                Debug.Assert(status == 0);
                return Result.Success(p.Get<int>("@GameId"));
            }

        }

    }
}
