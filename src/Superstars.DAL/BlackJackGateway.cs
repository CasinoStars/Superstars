using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Superstars.DAL
{
    public class BlackJackGateway
    {
        readonly string _connectionString;
        
        public BlackJackGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<int>> CreateJackPlayer(int userId, int nbturn, string[] cards = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@PlayerCards",cards);
                p.Add("@NbTurn", nbturn);
                p.Add("@BlackJackPlayerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sBlackJackPlayerCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "This player already exists.");

                return Result.Success(p.Get<int>("@BlackJackPlayerId"));
            }
        }

        public async Task<Result<int>> CreateJackAi(int userId, int nbturn, string[] cards = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@PlayerCards", cards);
                p.Add("@NbTurn", nbturn);
                p.Add("@PlayerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sBlackJackAICreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A player already exists.");

                return Result.Success(p.Get<int>("@PlayerId"));
            }
        }

        public async Task<Result<int>> DeleteJackAi(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sBlackJackAIDelete", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");

                return Result.Success(p.Get<int>("@Status"));
            }
        }

        public async Task<BlackJackData> GetPlayer(int playerId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<BlackJackData>(
                    "select top 1 t.BlackJackPlayerId, t.BlackJackGameId, t.PlayerCards, t.NbTurn from sp.tBlackJackPlayer t where t.BlackJackPlayerId = @BJPlayerId order by BlackJackGameId desc",
                    new { BJPlayerId = playerId });
            }
        }

        public async Task<BlackJackData> GetGameId(int playerId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<BlackJackData>(
                    "select top 1 t.BlackJackGameId from sp.tBlackJackPlayer t where t.BlackJackPlayerId = @BJPlayerId order by BlackJackGameId desc",
                    new { BJPlayerId = playerId });
            }
        }


        public async Task<string> GetPlayerCards(int userId, int gameId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<string>(
                    @"select t.PlayerCards from sp.tBlackJackPlayer t where t.BlackJackPlayerId = @BJPlayerId and t.BlackJackGameId = @BJGameId;",
                    new { BJPlayerId = userId, BJGameId = gameId });
            }
        }


        public async Task<Result<int>> UpdateBlackJackPlayer(int playerid, int gameid, string cards, int nbturn)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@BlackJackPlayerId", playerid);
                p.Add("@BlackJackGameId", gameid);
                p.Add("@PlayerCards", cards);
                p.Add("@NbTurn", nbturn);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sBlackJackPlayerUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "This game doesn't exist");
                if (status == 2) return Result.Failure<int>(Status.BadRequest, "This player id doesnt correspond to the game");

                return Result.Success(p.Get<int>("@BlackJackPlayerId"));
            }
        }

        public async Task<Result<int>> GetTurn(int playerId, int gameId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int data = await con.QueryFirstOrDefaultAsync<int>(
                    "select top 1 t.NbTurn from sp.tBlackJackPlayer t where t.BlackJackPlayerId = @BJPlayerId and t.BlackJackGameId = @BJGameId order by BlackJackGameId desc",
                    new { BJPlayerId = playerId, BJGameId = gameId });
                return Result.Success(data);
            }
        }

    }
}
