using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Superstars.YamsFair;

namespace Superstars.DAL
{
    public class ProvablyFairGateway
    {
       readonly string _connectionString;

        public ProvablyFairGateway(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<ProvablyFairData> GetSeeds(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<ProvablyFairData>(
                    "select m.UncryptedPreviousServerSeed, m.UncryptedServerSeed, m.CryptedServerSeed, m.ClientSeed,m.PreviousClientSeed,m.PreviousCryptedServerSeed, m.Nonce from sp.tProvablyFair m where m.UserId = @userId",
                    new { UserId = userId, });
            }
        }

        public async void UpdateSeeds(int userId,string clientSeed = null)
        {

            ProvablyFairData seeds = GetSeeds(userId).Result;
            SeedManager seedManager = new SeedManager(seeds.UncryptedServerSeed,seeds.UncryptedPreviousServerSeed,seeds.ClientSeed,seeds.CryptedServerSeed,seeds.PreviousClientSeed,seeds.PreviousCryptedServerSeed);
            seedManager.NewSeed(clientSeed);
            using (SqlConnection con = new SqlConnection(_connectionString))
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
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SeedManager seedManager = new SeedManager();
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

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "Marceau t'est trop un beau gosse ;)");

                return Result.Success(p.Get<int>("@Status"));
            }
        }


    }
}
