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
        #region Champs
        Random rdn = new Random();
        int[] _mydices = new int[5];
        int[] _IAdices = new int[5];
        int _IApoints, _MYpoints;
        bool _IsIAturn;
        int _IAturn, _MYturn;
        #endregion

        public YamsGateway(string connectionString)
        {
            _connectionString = connectionString;
            _IsIAturn = false;
            _IAturn = 0;
            _MYturn = 0;
            /*while (_MYturn < 3)
            {
                FirstShot();
                //IndexChange();
                Reroll();
                _MYturn++;
            }
            _IsIAturn = true;
            while (_IAturn < 3)
            {
                FirstShot();
                //IndexChange();
                Reroll();
                _IAturn++;
            }*/  
            _MYpoints = PointCount(_mydices);
            _IApoints = PointCount(_IAdices);
            FindWinner();
        }

        public async Task<Result<int>> CreateYamsPlayer(string pseudo, int nbturn, string dices, int dicesvalue)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Pseudo", pseudo);
                p.Add("@NbrRevives", nbturn);
                p.Add("@Dices", dices);
                p.Add("@DicesValue", dicesvalue);
                //p.Add("@YamsGameId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@PlayerId",dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsPlayerCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A player already exists.");

                return Result.Success(p.Get<int>("@PlayerId"));
            }
        }

        public async Task<Result<int>> CreateYamsAI(string pseudo, int nbturn, string dices, int dicesvalue)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Pseudo", pseudo);
                p.Add("@NbrRevives", nbturn);
                p.Add("@Dices", dices);
                p.Add("@DicesValue", dicesvalue);
                //p.Add("@YamsGameId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@PlayerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sYamsAICreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A player already exists.");

                return Result.Success(p.Get<int>("@PlayerId"));
            }
        }

        public async Task<Result<int>> DeleteYamsAi(string pseudo)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Pseudo", pseudo);
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
                    new { YamsPlayerId = playerId });
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

        public async Task<Result<int>> GetTurn(int playerId, int gameId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                int data =  await con.QueryFirstOrDefaultAsync<int>(
                    "select top 1 t.NbrRevives from sp.vYamsPlayer t where t.YamsPlayerId = @YamsPlayerId and t.YamsGameId = @YamsGameId order by YamsGameId desc",
                    new { YamsPlayerId = playerId, YamsGameId = gameId });
                return Result.Success(data);
            }
        }
        #region méthodes
        private void FirstShot()
        {
            if (_IsIAturn == true)
            {
                for (int i = 0; i < 6; i++)
                {
                    _IAdices[i] = RollDice();
                }
            }
            else if (_IsIAturn == false)
            {
                for (int i = 0; i < 6; i++)
                {
                    _mydices[i] = RollDice();
                }
            }
        }

        //public void IndexChange(int[] index)
        //{
        //    if (_IsIAturn == true)
        //    {
        //        for (int i = 0; i < index.Length; i++)
        //        {
        //            _IAdices[index[i]] = 0;
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < index.Length; i++)
        //        {
        //            _mydices[index[i]] = 0;
        //        }
        //    }
        //}

        public int[] IndexChange(int[] dices, int[] index)
        {
                for (int i = 0; i < index.Length; i++)
                {
                    dices[index[i]-1] = 0;
                }
            return dices;
        }


        //public void Reroll()
        //{
        //    if (_IsIAturn == false)
        //    {
        //        for (int i = 0; i < 6; i++)
        //        {
        //            if (_mydices[i] == 0)
        //            {
        //                _mydices[i] = RollDice();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < 6; i++)
        //        {
        //            if (_IAdices[i] == 0)
        //            {
        //                _IAdices[i] = RollDice();
        //            }
        //        }
        //    }
        //}

        public int[] Reroll(int[] dices)
        {
                for (int i = 0; i < 5; i++)
                {
                    if (dices[i] == 0)
                    {
                        dices[i] = RollDice();
                    }
                }
            return dices;
        }

        private int RollDice()
        {
            int value;
            value = rdn.Next(1, 6);
            return value;
        }

        private int[] DicesValue(int[] hand)
        {
            int[] count = new int[5] { 0, 0, 0, 0, 0 };
            for (int i = 0; i < 5; i++)
            {
                for (int z = 1; z <= 6; z++)
                {
                    if (hand[i] == z) count[z - 1]++;
                }
            }
            return count;
        }

        private int PointCount(int[] hand)
        {
            int[] handcount = new int[5];
            int points = 0;
            int grandesuite = 0;

            handcount = DicesValue(hand);
            for (int i = 0; i < 5; i++)
            {
                //yams
                if (handcount[i] == 5)
                {
                    points = points + 50;
                    points = 5 * (i + 1);
                    return points;
                }

                //carré
                if (handcount[i] == 4)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        if (handcount[l] == 1)
                        {
                            points = points + 40 + (4 * (i + 1)) + l + 1;
                            return points;
                        }
                    }
                }

                // full
                else if (handcount[i] == 3)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        if (handcount[l] == 2)
                        {
                            points = points + 30 + (3 * (i + 1)) + (2 * (l + 1));
                            return points;
                        }
                    }
                }

                // grande suite
                else if (handcount[i] == 1)
                {
                    grandesuite++;
                }
            }
            // petite suite
            if (handcount[0] == 1)
            {
                if ((handcount[1] == 1) && (handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1))
                {
                    points = points + 45;
                }
            }
            else if (handcount[1] == 1)
            {
                if ((handcount[2] == 1) && (handcount[3] == 1) && (handcount[4] == 1) && (handcount[5] == 1))
                {
                    points = points + 45;
                }
            }

            // grande suite
            if (grandesuite == 5)
            {
                points = points + 50;
            }

            // chance
            if (points == 0)
            {
                for (int i = 1; i < 6; i++)
                {
                    points = points + handcount[i - 1] * i;
                }
            }

            return points;
        }

        private string FindWinner()
        {
            if (_IApoints < _MYpoints)
            {
                return "You Win";
            }
            else if (_IApoints > _MYpoints)
            {
                return "You Lost";
            }
            else
            {
                return "Draw";
            }
        }
        #endregion

    }
}