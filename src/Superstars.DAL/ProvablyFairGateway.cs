using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Superstars.YamsFair;

namespace Superstars.DAL
{
    class ProvablyFairGateway
    {
       readonly string _connectionString;

        public ProvablyFairGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task UpdateSeeds(int userId, byte[] password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "sp.ProvablyFairUpdate",
                    new { UserId = userId, UserPassword = password },
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<ProvablyFairData> FindByEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<ProvablyFairData>(
                    "select u.UserId, u.Email, u.UserName, u.UserPassword from sp.vUser u where u.Email = @Email",
                    new { Email = email });
            }
        }

        public async Task<Result<int>> AddSeeds(int moneyId, int moneyType, int coins)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SeedManager seedManager = new SeedManager();
                var p = new DynamicParameters();
                p.Add("@UncryptedPreviousServerSeed", "");
                p.Add("@UncryptedServerSeed", "");
                p.Add("@CryptedServerSeed", "");
                p.Add("@ClientSeed", "");
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sProvablyFairCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "Marceau t'est trop un beau gosse ;)");

                return Result.Success(p.Get<int>("@Balance"));
            }
        }


    }
}
