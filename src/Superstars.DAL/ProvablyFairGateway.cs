using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Superstars.YamsFair;

namespace Superstars.DAL
{
    public class ProvablyFairGateway
    {
        private readonly string _connectionString;

        public ProvablyFairGateway(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<ProvablyFairData> GetSeeds(int userId)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<ProvablyFairData>(
                    "select m.UncryptedPreviousServerSeed, m.UncryptedServerSeed, m.CryptedServerSeed, m.ClientSeed,m.PreviousClientSeed,m.PreviousCryptedServerSeed, m.Nonce from sp.tProvablyFair m where m.UserId = @userId",
                    new {UserId = userId});
            }
        }

        public async void UpdateSeeds(int userId, string clientSeed = null)
        {
            var seeds = GetSeeds(userId).Result;
            var seedManager = new SeedManager(seeds.UncryptedServerSeed, seeds.UncryptedPreviousServerSeed,
                seeds.ClientSeed, seeds.CryptedServerSeed, seeds.PreviousClientSeed, seeds.PreviousCryptedServerSeed);
            seedManager.NewSeed(clientSeed);
            using (var con = new SqlConnection(_connectionString))
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
//                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sProvablyFairUpdate", p, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Result<int>> AddSeeds(int userId)
        {
            using (var con = new SqlConnection(_connectionString))
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
            using (var con = new SqlConnection(_connectionString))
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