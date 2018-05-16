using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    class YamsGateway
    {
		readonly string _connectionString;

		public YamsGateway(string connectionString)
		{
			_connectionString = connectionString;
		}

        public async Task<Result<int>> CreateYamsGame(GamesTypes game)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@GameType", (int)game);
                p.Add("@StartDate", DateTime.UtcNow);
                p.Add("@GameId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sGamesCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A game with this gametype and  start date already exists.");

                return Result.Success(p.Get<int>("@GameId"));
            }
        }
    }
}
