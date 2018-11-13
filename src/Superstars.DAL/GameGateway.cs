using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class GameGateway
    {
        private readonly string _sqlstring;

        public GameGateway(string sqlstring)
        {
            _sqlstring = sqlstring;
        }

        public async Task<GameData> FindGameById(int GameID)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                return await con.QueryFirstOrDefaultAsync<GameData>(
                    "select g.GameId, g.GameType, g.StartDate, g.EndDate, g.Winner from vGames g where g.GameId = @GameId",
                    new {GameId = GameID});
            }
        }

        public async Task<Result<int>> CreateGame(int userId, string gameType)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@GameType", gameType);
                p.Add("@StartDate", DateTime.UtcNow);
                p.Add("@GameId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sGamesCreate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1)
                    return Result.Failure<int>(Status.BadRequest,
                        "A game with this gametype and start date already exists.");

                return Result.Success(p.Get<int>("@GameId"));
            }
        }

        public async Task<Result<string>> GetYamsPot(int gameId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var pot = await con.QueryFirstOrDefaultAsync<string>(
                    @"select g.Pot from sp.vGameYams g where g.YamsGameId = @YamsGameId",
                    new {YamsGameId = gameId});
                return Result.Success(pot);
            }
        }

        public async Task<Result<string>> GetBlackJackPot(int gameId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var pot = await con.QueryFirstOrDefaultAsync<string>(
                    @"select g.Pot from sp.vGameBlackJack g where g.BlackJackGameId = @BlackJackGameId",
                    new {BlackJackGameId = gameId});
                return Result.Success(pot);
            }
        }

        public async Task<Result<int>> CreateYamsGame(string pot)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@Pot", pot);
                p.Add("@YamsGameId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sGameYamsCreate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");

                return Result.Success(p.Get<int>("@YamsGameId"));
            }
        }

        public async Task<Result<int>> CreateBlackJackGame(string pot)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@Pot", pot);
                p.Add("@BlackJackGameId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sGameBlackJackCreate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");

                return Result.Success(p.Get<int>("@BlackJackGameId"));
            }
        }

        public async Task<Result<int>> DeleteAis(int userId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sUserAIDelete", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");

                return Result.Success(p.Get<int>("@Status"));
            }
        }

        public async Task<Result<int>> UpdateStats(int userid, string gametype, int wins, int losses)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@GameType", gametype);
                p.Add("@UserId", userid);
                p.Add("@Wins", wins);
                p.Add("@Losses", losses);
                //p.Add("@AverageBet", averagebet);
                //p.Add("@Averagetime", averagetime);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sStatsUpdate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "This player doesn't exist.");

                return Result.Success(p.Get<int>("@Status"));
            }
        }

        public async Task<Result<int>> GetWins(int userId, string gameType)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    @"select s.Wins from sp.tStats s where s.userid = @userid and s.GameType = @gametype",
                    new {userid = userId, gametype = gameType});
                return Result.Success(data);
            }
        }

        public async Task<Result<int>> GetLosses(int userId, string gameType)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    @"select s.Losses from sp.tStats s where s.userid = @userid and s.GameType = @gametype",
                    new {userid = userId, gametype = gameType});
                return Result.Success(data);
            }
        }

        public async Task<Result<int>> GetTrueProfit(int userId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    @"select m.Profit from sp.tMoney m where m.MoneyId = @userid and m.MoneyType = 1",
                    new {userid = userId});
                return Result.Success(data);
            }
        }

        public async Task<Result<int>> GetFakeProfit(int userId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    @"select m.Profit from sp.tMoney m where m.MoneyId = @userid and m.MoneyType = 2",
                    new {userid = userId});
                return Result.Success(data);
            }
        }

        public async Task UpdateGameEnd(int gameid)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                await con.ExecuteAsync(
                    "sp.sGamesUpdate",
                    new {GameId = gameid, EndDate = DateTime.UtcNow},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Result<int>> IsGameEndDefined(int gameid)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    @"select g.EndDate from sp.tGames g where g.GameId = @gameId",
                    new { gameId = gameid });
                return Result.Success(data);
            }
        }
        
    }
}