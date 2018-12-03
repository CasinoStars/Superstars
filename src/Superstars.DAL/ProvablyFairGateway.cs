using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Superstars.YamsFair;

namespace Superstars.DAL
{
    public class ProvablyFairGateway
    {
        private readonly SqlConnexion _sqlConnexion;

        public ProvablyFairGateway(SqlConnexion sqlConnection)
        {
            _sqlConnexion = sqlConnection;
        }


        public async Task<ProvablyFairData> GetSeeds(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryFirstOrDefaultAsync<ProvablyFairData>(
                    "select m.UncryptedPreviousServerSeed, m.UncryptedServerSeed, m.CryptedServerSeed, m.ClientSeed,m.PreviousClientSeed,m.PreviousCryptedServerSeed, m.Nonce from sp.tProvablyFair m where m.UserId = @userId",
                    new {UserId = userId});
            }
        }

        public async Task<Result<int>> UpdateSeeds(int userId, string clientSeed = null)
        {
            var seeds = GetSeeds(userId).Result;
            var seedManager = new SeedManager(seeds.UncryptedServerSeed, seeds.UncryptedPreviousServerSeed,
                seeds.ClientSeed, seeds.CryptedServerSeed, seeds.PreviousClientSeed, seeds.PreviousCryptedServerSeed);
            seedManager.NewSeed(clientSeed);
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@UncryptedPreviousServerSeed", seedManager.PreviousUncryptedServerSeed);
                p.Add("@UncryptedServerSeed", seedManager.UncryptedServerSeed);
                p.Add("@CryptedServerSeed", seedManager.CryptedServerSeed);
                p.Add("@ClientSeed", seedManager.ClientSeed);
                p.Add("@PreviousClientSeed", seedManager.PreviousClientSeeds);
                p.Add("@PreviousCryptedServerSeed", seedManager.PreviousCryptedServerSeed);
                p.Add("@Nonce", 0);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await con.ExecuteAsync("sp.sProvablyFairUpdate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status != 0) return Result.Failure<int>(Status.BadRequest, "Il y a une erreur poto");

                return Result.Success(p.Get<int>("@Status"));
            }
        }

        public async Task<Result> ActionChangeSeeds(int userid, string username, DateTime date, string clientSeed, string previousClientSeed, string serverSeed, string previousServerSeed)
        {
            string action = "Player named " + username + " with UserID " + userid + " changed his client seed from " + previousClientSeed + 
                " to " + clientSeed + " and his server seed from " + previousServerSeed + " to " + serverSeed + " at " + date.ToString();

            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var res = await con.ExecuteAsync("sp.sLogTableCreate", new { UserId = userid, ActionDate = date, ActionDescription = action },
                    commandType: CommandType.StoredProcedure);

                if (res == 1) return Result.Failure<int>(Status.BadRequest, "Il y a déjà un log");

                return Result.Success(res);
            }
        }

        public async Task<Result> IncrementSeedsinStats(int userId)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<Result>(
                    "Update sp.tStats set ClientSeedChanges = ClientSeedChanges + 1 where UserId = @UserId",
                    new { UserId = userId });
            }
        }

        public async Task<Result<int>> AddSeeds(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var seedManager = new SeedManager();
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@UncryptedPreviousServerSeed", "");
                p.Add("@UncryptedServerSeed", seedManager.UncryptedServerSeed);
                p.Add("@CryptedServerSeed", seedManager.CryptedServerSeed);
                p.Add("@ClientSeed", seedManager.ClientSeed);
                p.Add("@PreviousClientSeed", seedManager.PreviousClientSeeds);
                p.Add("@PreviousCryptedServerSeed", seedManager.PreviousCryptedServerSeed);
                p.Add("@Nonce", 0);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sProvablyFairCreate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "Marceau t'est trop un beau gosse ;)");

                return Result.Success(p.Get<int>("@Status"));
            }
        }

        public async Task<int> GetDicesFromHash(int userId)
        {
            var seeds = GetSeeds(userId).Result;
            var seedManager = new SeedManager(seeds.UncryptedServerSeed, seeds.UncryptedPreviousServerSeed,
                seeds.ClientSeed, seeds.CryptedServerSeed, seeds.PreviousClientSeed, seeds.PreviousCryptedServerSeed);
            var dice = HashManager.GetDiceFromHash(seeds.UncryptedServerSeed, seeds.ClientSeed, seeds.Nonce);
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@UncryptedPreviousServerSeed", seedManager.PreviousUncryptedServerSeed);
                p.Add("@UncryptedServerSeed", seedManager.UncryptedServerSeed);
                p.Add("@CryptedServerSeed", seedManager.CryptedServerSeed);
                p.Add("@ClientSeed", seedManager.ClientSeed);
                p.Add("@PreviousClientSeed", seedManager.PreviousClientSeeds);
                p.Add("@PreviousCryptedServerSeed", seedManager.PreviousCryptedServerSeed);
                p.Add("@Nonce", seeds.Nonce + 1);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sProvablyFairUpdate", p, commandType: CommandType.StoredProcedure);
            }

            return dice;
        }
    }
}