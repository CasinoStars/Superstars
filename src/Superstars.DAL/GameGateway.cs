using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class GameGateway
    {
        private readonly SqlConnexion _sqlConnexion;

        public GameGateway(SqlConnexion sqlConnexion)
        {
            _sqlConnexion = sqlConnexion;
        }

        public async Task<GameData> FindGameById(int GameID)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryFirstOrDefaultAsync<GameData>(
                    "select g.GameId, g.GameType, g.StartDate, g.EndDate, g.Winner from vGames g where g.GameId = @GameId",
                    new {GameId = GameID});
            }
        }

        public async Task<int> GetGameIdToDeleteByPlayerId(int playerId, int gametype)
        {
            if (gametype == 0)
            {
                using (var con = new SqlConnection(_sqlConnexion.connexionString))
                {
                    return await con.QueryFirstOrDefaultAsync<int>( 
                        "select top 1 y.YamsGameId from sp.tYamsPlayer y where y.YamsPlayerId = @PlayerId",
                        new { PlayerId = playerId});
                }
            }
            else if (gametype == 1)
            {

                using (var con = new SqlConnection(_sqlConnexion.connexionString))
                {
                    return await con.QueryFirstOrDefaultAsync<int>(
                       "select top 1 b.BlackJackGameId from sp.tBlackJackPlayer b where b.BlackJackPlayerId = @PlayerId",
                        new { PlayerId = playerId });
                }
            }
            else
            {
                throw new Exception("PRoblem");
            }
        }

        public async Task<GameData> GetGameByPlayerId(int playerId, int gametype)
        {
            if(gametype == 0)
            {
                using (var con = new SqlConnection(_sqlConnexion.connexionString))
                {
                    return await con.QueryFirstOrDefaultAsync<GameData>(
                        "select top 1 g.GameId, g.GameTypeId, g.StartDate, g.EndDate, g.Winner from sp.tGames g left join sp.tYamsPlayer y on g.GameId = y.YamsGameId where y.YamsPlayerId = @PlayerId and g.GameTypeId = @Gametype order by g.StartDate desc",
                        new { PlayerId = playerId, Gametype = gametype });
                }
            } else if (gametype == 1) {

                using (var con = new SqlConnection(_sqlConnexion.connexionString))
                {
                    return await con.QueryFirstOrDefaultAsync<GameData>(
                        "select top 1 g.GameId, g.GameTypeId, g.StartDate, g.EndDate, g.Winner from sp.tGames g left join sp.tBlackJackPlayer b on g.GameId = b.BlackJackGameId where b.BlackJackPlayerId = @PlayerId and g.GameTypeId = @Gametype order by g.StartDate desc",
                        new { PlayerId = playerId, Gametype = gametype });
                }
            } else
            {
                throw new Exception("Gametype is not defined");
            }
        }

        public async Task<Result> DeleteGameByPlayerId(int playerId, int gametype)
        {
            if(gametype == 0)
            {
                using (var con = new SqlConnection(_sqlConnexion.connexionString))
                {
                    return await con.QueryFirstOrDefaultAsync<Result>(
                        "delete g from sp.tGames g left join sp.tYamsPlayer y on g.GameId = y.YamsGameId where y.YamsPlayerId = @PlayerId and g.GameTypeId = @GametypeId",
                        new { PlayerId = playerId, GametypeId = gametype });
                }
            }
            else if(gametype == 1)
            {
                using (var con = new SqlConnection(_sqlConnexion.connexionString))
                {
                    return await con.QueryFirstOrDefaultAsync<Result>(
                        "delete g from sp.tGames g left join sp.tBlackJackPlayer b on g.GameId = b.BlackJackGameId where b.BlackJackPlayerId = @PlayerId and g.GameTypeId = @GametypeId",
                        new { PlayerId = playerId, GametypeId = gametype });
                }
            }
            else
            {
                throw new Exception("Gametype is not defined");
            }
        }

        public async Task<Result> DeleteYamsGameByGameId(int gameid)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
               return await con.QueryFirstOrDefaultAsync<Result>(
                    "delete from sp.tGameYams where YamsGameId = @GameID",
                    new { GameID = gameid });
            }
        }

        public async Task<Result> DeleteBlackJackGameByGameId(int gameid)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryFirstOrDefaultAsync<Result>(
                     "delete from sp.tBlackJackPlayer where BlackJackGameId = @GameID",
                     new { GameID = gameid });
            }
        }
        public async Task<Result<int>> CreateGame(int gameTypeId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@GameTypeId", gameTypeId);
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

        public async Task ActionStartGameFake(int userid, string username, DateTime date, int gametype, int gameid)
        {
            Result<string> pot;
            int bet = 0;
            if (gametype == 0)
            {
                pot = await GetYamsPot(gameid);
                bet = Convert.ToInt32(pot.Content);
                bet = bet / 2;
            }
            else
            {
                pot = await GetBlackJackPot(gameid);
                bet = Convert.ToInt32(pot.Content);
                bet = bet / 2;
            }

            string action = "Player named " + username + " with UserID " + userid + " started a game of " + gametype + " with GameID " + gameid +
                " and a bet of " + bet + " All`in Coins at " + date.ToString();

            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                await con.ExecuteAsync("sp.sLogTableCreate", new { UserId = userid, ActionDate = date, ActionDescription = action },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ActionStartGameBTC(int userid, string username, DateTime date, int gametype, int gameid)
        {
            Result<string> pot;
            decimal bet;

            if (gametype == 0)
            { 
                pot = await GetYamsPot(gameid);
                bet = Convert.ToDecimal(pot.Content);
                bet = bet/2;
            } else {
                pot = await GetBlackJackPot(gameid);
                bet = Convert.ToDecimal(pot.Content);
                bet = bet/2;
            }

            string action = "Player named " + username + " with UserID " + userid + " started a game of " + gametype + " with GameID " + gameid + 
                " and a bet of " + bet + " bits (BTC) at " + date.ToString();

            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                await con.ExecuteAsync("sp.sLogTableCreate", new { UserId = userid, ActionDate = date, ActionDescription = action },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ActionEndGame(int userid, string username, DateTime date, int gametype, int gameid, string haswin, string trueOrFake)
        {
            Result<string> pot;
            decimal bet;
            string money;

            if (trueOrFake == "real")
            {
                money = " bits (BTC) ";
            }else
            {
                money = " All`in Coins ";
            }

            if (gametype == 0)
            {
                pot = await GetYamsPot(gameid);
                bet = Convert.ToDecimal(pot.Content);
                bet = bet / 2;
            }
            else
            {
                pot = await GetBlackJackPot(gameid);
                bet = Convert.ToDecimal(pot.Content);
                bet = bet / 2;
            }

            string action = "Player named " + username + " with UserID " + userid + " ended a game of " + gametype + " with GameID " + gameid +
                " and has " + haswin + " a bet of " + bet +  money + "at " + date.ToString();

            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                await con.ExecuteAsync("sp.sLogTableCreate", new { UserId = userid, ActionDate = date, ActionDescription = action },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Result<string>> GetYamsPot(int gameId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var pot = await con.QueryFirstOrDefaultAsync<string>(
                    @"select g.Pot from sp.vGameYams g where g.YamsGameId = @YamsGameId",
                    new {YamsGameId = gameId});
                return Result.Success(pot);
            }
        }

        public async Task<Result<string>> GetBlackJackPot(int gameId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var pot = await con.QueryFirstOrDefaultAsync<string>(
                    @"select g.Pot from sp.vGameBlackJack g where g.BlackJackGameId = @BlackJackGameId",
                    new {BlackJackGameId = gameId});
                return Result.Success(pot);
            }
        }

        public async Task<Result<int>> CreateYamsGame(string pot)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
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

        public async Task<Result<string>> UpdateYamsGame(string pot, int gameId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@Pot", pot);
                p.Add("@YamsGameId", gameId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sGameYamsUpdate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");

                return Result.Success(p.Get<string>("@Pot"));
            }
        }

        public async Task<Result<int>> CreateBlackJackGame(string pot)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
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

        public async Task<Result<int>> UpdateBlackJackGame(string pot, int gameId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@Pot", pot);
                p.Add("@BlackJackGameId", gameId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sGameBlackJackUpdate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");

                return Result.Success(p.Get<dynamic>("@BlackJackGameId"));
            }
        }

        public async Task<Result<int>> DeleteAis(int userId, int gametypeid)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@GameType", gametypeid);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sUserAIDelete", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");

                return Result.Success(p.Get<int>("@Status"));
            }
        }

        public async Task<Result<int>> UpdateStats(int userid, int gameTypeId, int wins, int losses)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@GameTypeId", gameTypeId);
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

        public async Task<Result<int>> GetWins(int userId, int gameTypeId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    @"select s.Wins from sp.tStats s where s.userId = @userid and s.GameTypeId = @gametypeid",
                    new {userid = userId, gametypeid = gameTypeId});
                return Result.Success(data);
            }
        }

        public async Task<Result<int>> GetLosses(int userId, int gameTypeId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    @"select s.Losses from sp.tStats s where s.userId = @userid and s.GameTypeId = @gametypeid",
                    new {userid = userId, gametypeid = gameTypeId});
                return Result.Success(data);
            }
        }

        public async Task<Result<int>> GetTrueProfit(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    @"select m.Profit from sp.tMoney m where m.MoneyId = @userid and m.MoneyType = 1",
                    new {userid = userId});
                return Result.Success(data);
            }
        }

        public async Task<Result<int>> GetFakeProfit(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    @"select m.Profit from sp.tMoney m where m.MoneyId = @userid and m.MoneyType = 2",
                    new {userid = userId});
                return Result.Success(data);
            }
        }

        public async Task<Result> UpdateGameEnd(int gameid, int gametypeId, string win)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@GameId", gameid);
                p.Add("@GameTypeId", gametypeId);
                p.Add("@EndDate", DateTime.UtcNow);
                p.Add("@Winner", win);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sGamesUpdate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "Game doesn't exist.");

                return Result.Success(p.Get<int>("@Status"));
            }
        }

        public async Task<Result<DateTime>> IsGameEndDefined(int gameid, string gametype)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<DateTime>(
                    @"select g.EndDate from sp.tGames g where g.GameId = @gameId and g.GameType = @gametype",
                    new { gameId = gameid, GameType = gametype });
                return Result.Success(data);
            }
        }



    }
}