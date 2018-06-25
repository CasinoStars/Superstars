using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;


namespace Superstars.DAL
{
    public class YamsGateway
    {
        readonly string _connectionString;

        public YamsGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<int>> CreateYamsPlayer(int userId, int nbturn, string dices, int dicesvalue)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@PlayerId", userId);
                p.Add("@NbrRevives", nbturn);
                p.Add("@Dices", dices);
                p.Add("@DicesValue", dicesvalue);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsPlayerCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A player already exists.");

                return Result.Success(p.Get<int>("@PlayerId"));
            }
        }

        public async Task<Result<int>> CreateYamsAI(int userId, int nbturn, string dices, int dicesvalue)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@NbrRevives", nbturn);
                p.Add("@Dices", dices);
                p.Add("@DicesValue", dicesvalue);
                p.Add("@PlayerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsAICreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A player already exists.");

                return Result.Success(p.Get<int>("@PlayerId"));
            }
        }

        public async Task<Result<int>> DeleteYamsAi(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsAIDelete", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");

                return Result.Success(p.Get<int>("@Status"));
            }
        }

        public async Task<Result<int>> UpdateYamsPlayer(int playerid, int gameid, int nbturn, string dices, int dicesvalue)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@YamsPlayerId", playerid);
                p.Add("@YamsGameId", gameid);
                p.Add("@NbrRevives", nbturn);
                p.Add("@Dices", dices);
                p.Add("@DicesValue", dicesvalue);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsPlayerUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "This game not exists");

                return Result.Success(p.Get<int>("@YamsPlayerId"));
            }
        }

        public async Task<YamsData> GetPlayer(int playerId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<YamsData>(
                    "select top 1 t.YamsPlayerId, t.YamsGameId, t.NbrRevives, t.Dices, t.DicesValue from sp.vYamsPlayer t where t.YamsPlayerId = @YamsPlayerId order by YamsGameId desc",
                    new { YamsPlayerId = playerId });
            }
        }

        public async Task<YamsData> GetGameId(int playerId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<YamsData>(
                    "select top 1 t.YamsGameId from sp.vYamsPlayer t where t.YamsPlayerId = @YamsPlayerId order by YamsGameId desc",
                    new { YamsPlayerId = playerId, });
            }
        }


        public async Task<Result<string>> GetPlayerDices(int userId, int gameId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string data = await con.QueryFirstOrDefaultAsync<string>(
                    @"select t.Dices from sp.tYamsPlayer t where t.YamsPlayerId = @YamsPlayerId and t.YamsGameId = @YamsGameId;",
                    new { YamsPlayerId = userId, YamsGameId = gameId });
                if (data == null) return Result.Failure<string>(Status.NotFound, "Data not found.");
                return Result.Success(data);
            }
        }

        public async Task<Result<string>> GetIaDices(int userId, int gameId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string data = await con.QueryFirstOrDefaultAsync<string>(
                    @"select t.Dices from sp.tYamsPlayer t where t.YamsPlayerId = @YamsPlayerId and t.YamsGameId = @YamsGameId;",
                    new { YamsPlayerId = userId, YamsGameId = gameId });
                if (data == null) return Result.Failure<string>(Status.NotFound, "Data not found.");
                return Result.Success(data);
            }
        }

        public async Task<Result<int>> GetTurn(int playerId, int gameId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int data = await con.QueryFirstOrDefaultAsync<int>(
                    "select top 1 t.NbrRevives from sp.vYamsPlayer t where t.YamsPlayerId = @YamsPlayerId and t.YamsGameId = @YamsGameId order by YamsGameId desc",
                    new { YamsPlayerId = playerId, YamsGameId = gameId });
                return Result.Success(data);
            }
        }
    }
}