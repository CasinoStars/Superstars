using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Superstars.DAL
{
    public class WalletGateway
    {
        readonly string _sqlstring;

        public WalletGateway(string sqlstring)
        {
            _sqlstring = sqlstring;
        }

        public async Task<Result<int>> AddCoins(string pseudo, int coins, int moneyType)
        {
            using (SqlConnection con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@Pseudo", pseudo);
                p.Add("@Balance", coins);
                p.Add("@MoneyType", moneyType);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sMoneyCreateOrUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "Money type already exist ??");

                return Result.Success(p.Get<int>("@Balance"));
            }
        }
    }
}