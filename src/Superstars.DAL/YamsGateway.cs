﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class YamsGateway
    {
        private readonly SqlConnexion _sqlConnexion;

        public YamsGateway(SqlConnexion sqlConnexion)
        {
            _sqlConnexion = sqlConnexion;
        }

        public async Task<Result<int>> CreateYamsPlayer(int userId, int nbturn, string dices, int dicesvalue)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@PlayerId", userId);
                p.Add("@NbrRevives", nbturn);
                p.Add("@Dices", dices);
                p.Add("@DicesValue", dicesvalue);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsPlayerCreate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A player already exists.");

                return Result.Success(p.Get<int>("@PlayerId"));
            }
        }

        public async Task<Result> DeleteYamsPlayer(int gameid)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryFirstOrDefaultAsync<Result>(
                    "delete from sp.tYamsPlayer where YamsGameId = @GameID",
                    new { GameID = gameid });
            }
        }

        public async Task<Result<int>> CreateYamsAI(int userId, int nbturn, string dices, int dicesvalue)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@NbrRevives", nbturn);
                p.Add("@Dices", dices);
                p.Add("@DicesValue", dicesvalue);
                p.Add("@PlayerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsAICreate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A player already exists.");

                return Result.Success(p.Get<int>("@PlayerId"));
            }
        }

        public async Task<Result<int>> DeleteYamsAi(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsAIDelete", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");

                return Result.Success(p.Get<int>("@Status"));
            }
        }

        public async Task<Result<int>> UpdateYamsPlayer(int playerid, int gameid, int nbturn, string dices,
            int dicesvalue)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@YamsPlayerId", playerid);
                p.Add("@YamsGameId", gameid);
                p.Add("@NbrRevives", nbturn);
                p.Add("@Dices", dices);
                p.Add("@DicesValue", dicesvalue);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsPlayerUpdate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "This game not exists");

                return Result.Success(p.Get<int>("@YamsPlayerId"));
            }
        }

        public async Task<YamsData> GetPlayer(int playerId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryFirstOrDefaultAsync<YamsData>(
                    "select top 1 t.YamsPlayerId, t.YamsGameId, t.NbrRevives, t.Dices, t.DicesValue from sp.vYamsPlayer t where t.YamsPlayerId = @YamsPlayerId order by YamsGameId desc",
                    new {YamsPlayerId = playerId});
            }
        }

        public async Task<YamsData> GetGameId(int playerId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryFirstOrDefaultAsync<YamsData>(
                    "select top 1 t.YamsGameId from sp.vYamsPlayer t where t.YamsPlayerId = @YamsPlayerId order by YamsGameId desc",
                    new {YamsPlayerId = playerId});
            }
        }


        public async Task<Result<string>> GetPlayerDices(int userId, int gameId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<string>(
                    @"select t.Dices from sp.tYamsPlayer t where t.YamsPlayerId = @YamsPlayerId and t.YamsGameId = @YamsGameId;",
                    new {YamsPlayerId = userId, YamsGameId = gameId});
                if (data == null) return Result.Failure<string>(Status.NotFound, "Data not found.");
                return Result.Success(data);
            }
        }

        public async Task<Result<string>> GetIaDices(int userId, int gameId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<string>(
                    @"select t.Dices from sp.tYamsPlayer t where t.YamsPlayerId = @YamsPlayerId and t.YamsGameId = @YamsGameId;",
                    new {YamsPlayerId = userId, YamsGameId = gameId});
                if (data == null) return Result.Failure<string>(Status.NotFound, "Data not found.");
                return Result.Success(data);
            }
        }

        public async Task<Result<int>> GetTurn(int playerId, int gameId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var data = await con.QueryFirstOrDefaultAsync<int>(
                    "select top 1 t.NbrRevives from sp.vYamsPlayer t where t.YamsPlayerId = @YamsPlayerId and t.YamsGameId = @YamsGameId order by YamsGameId desc",
                    new {YamsPlayerId = playerId, YamsGameId = gameId});
                return Result.Success(data);
            }
        }

        //WIP
        public async Task ActionRollDices(int userid, string username, int gameId, DateTime date, string oldDices, string newDices)
        {
            string action = "Player named " + username + " with UserID " + userid + " in GameID " + gameId + " and with dices " + oldDices +  " revived his dices and get " + newDices + " at " + date.ToString();

            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                await con.ExecuteAsync("sp.sLogTableCreate", new { UserId = userid, ActionDate = date, ActionDescription = action },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}