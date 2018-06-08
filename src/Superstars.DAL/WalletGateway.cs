using Dapper;
using NBitcoin;
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

        public async Task<Result<int>> AddCoins(int moneyId, int moneyType, int coins)
        {
            using (SqlConnection con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@MoneyId", moneyId);
                p.Add("@Balance", coins);
                p.Add("@MoneyType", moneyType);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sMoneyCreateOrUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "Money type already exist ??");

                return Result.Success(p.Get<int>("@Balance"));
            }
        }

        //public async Task<Result<WalletData>> GetTrueBalance(int moneyId)
        //{
        //    using (SqlConnection con = new SqlConnection(_sqlstring))
        //    {
        //        WalletData wallet =  await con.QueryFirstOrDefaultAsync<WalletData>(
        //            "select m.MoneyId, m.MoneyType, m.Balance from sp.vMoney m where m.MoneyId = @moneyId and m.MoneyType = 1",
        //            new { MoneyId = moneyId });
        //        if (wallet == null) return Result.Failure<WalletData>(Status.NotFound, "Wallet not found.");
        //        return Result.Success(wallet);
        //    }
        //}


        public async Task<Result<WalletData>> GetFakeBalance(int moneyId)
        {
            using (SqlConnection con = new SqlConnection(_sqlstring))
            {
                WalletData wallet = await con.QueryFirstOrDefaultAsync<WalletData>(
                    "select m.MoneyId, m.MoneyType, m.Balance from sp.vMoney m where m.MoneyId = @moneyId and m.MoneyType = 2",
                    new { MoneyId = moneyId });
                if (wallet == null) return Result.Failure<WalletData>(Status.NotFound, "Wallet not found.");
                return Result.Success(wallet);
            }
        }

        public async Task<Result<WalletData>> GetPrivateKey(int userId)
        {
            using (SqlConnection con = new SqlConnection(_sqlstring))
            {
                WalletData privateKey = await con.QueryFirstOrDefaultAsync<WalletData>(
                    "select u.PrivateKey from sp.vUser u where u.UserId = @UserId",
                    new { UserId = userId });
                if (privateKey == null) return Result.Failure<WalletData>(Status.NotFound, "Private key found.");
                return Result.Success(privateKey);
            }
        }
    }
}