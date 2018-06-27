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


        public async Task<UserData> FindByName(string pseudo)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select u.UserId, u.Email, u.UserName, u.UserPassword from sp.vUser u where u.UserName = @UserName",
                    new { UserName = pseudo });
            }
        }

        public async Task<ProvablyFairData> GetSeeds(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<ProvablyFairData>(
                    "select m.UncryptedPreviousServerSeed, m.UncryptedServerSeed, m.CryptedServerSeed, m.ClientSeed, m.Nonce from sp.tProvablyFair m where m.ProvablyFair = @userId",
                    new { UserId = userId, });
            }
        }


        //       UserId int identity(0,1),
        //UncryptedPreviousServerSeed varchar(128),
        //UncryptedServerSeed varchar(128),
        //CryptedServerSeed varchar(128),
        //ClientSeed nvarchar(128),
        //Nonce int

        public async Task UpdateSeeds(int userId,string clientSeed = null)
        {

            ProvablyFairData seeds = GetSeeds(userId).Result;
            SeedManager seedManager = new SeedManager(seeds.UncryptedServerSeed,seeds.UncryptedPreviousServerSeed,seeds.ClientSeed,seeds.CryptedServerSeed);
            seedManager.NewSeed(clientSeed);
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "sp.ProvablyFairUpdate",
                  new { UserId = userId, UncryptedPreviousServerSeed = seeds.UncryptedServerSeed,CryptedServerSeed = seeds.CryptedServerSeed, ClientSeed = seeds.ClientSeed, Nonce = 0},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Result<int>> AddSeeds(int moneyId, int moneyType, int coins)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SeedManager seedManager = new SeedManager();
                var p = new DynamicParameters();
                p.Add("@UncryptedPreviousServerSeed", seedManager.PreviousUncryptedServerSeed);
                p.Add("@UncryptedServerSeed", seedManager.UncryptedServerSeed);
                p.Add("@CryptedServerSeed", seedManager.CryptedServerSeed);
                p.Add("@ClientSeed", seedManager.ClientSeed);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sProvablyFairCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "Marceau t'est trop un beau gosse ;)");

                return Result.Success(p.Get<int>("@Balance"));
            }
        }


    }
}
