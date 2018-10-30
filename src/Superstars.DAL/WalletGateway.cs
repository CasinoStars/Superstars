using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class WalletGateway
    {
        private readonly string _sqlstring;

        public WalletGateway(string sqlstring)
        {
            _sqlstring = sqlstring;
        }

        public async Task<Result<int>> AddCoins(int moneyId, int moneyType, int coins, int profit, int credit)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@Profit", profit);
                p.Add("@Credit", credit);
                p.Add("@MoneyId", moneyId);
                p.Add("@Balance", coins);
                p.Add("@MoneyType", moneyType);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sMoneyUpdate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "Money type do no exist");

                return Result.Success(p.Get<int>("@Balance"));
            }
        }

        public async Task<Result> InsertInBankRoll(int trueCoins, int fakeCoins)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var p = new DynamicParameters();
                p.Add("@RealCoins", trueCoins);
                p.Add("@FakeCoins", fakeCoins);
                await con.ExecuteAsync("sp.sBankRollUpdate", p, commandType: CommandType.StoredProcedure);
                return Result.Success(p);
            }
        }


        public async Task<Result<int>> GetBTCBankRoll()
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var bankRoll = await con.QueryFirstOrDefaultAsync<int>("select RealCoins from sp.tBankRoll");
                return Result.Success(bankRoll);
            }
        }

        public async Task<Result<int>> GetFakeBankRoll()
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var bankRoll = await con.QueryFirstOrDefaultAsync<int>("select FakeCoins from sp.tBankRoll");
                return Result.Success(bankRoll);
            }
        }


        public async Task<Result<WalletData>> GetFakeBalance(int moneyId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var wallet = await con.QueryFirstOrDefaultAsync<WalletData>(
                    "select m.MoneyId, m.MoneyType, m.Balance from sp.vMoney m where m.MoneyId = @moneyId and m.MoneyType = 2",
                    new {MoneyId = moneyId});
                if (wallet == null) return Result.Failure<WalletData>(Status.NotFound, "Wallet not found.");
                return Result.Success(wallet);
            }
        }

        public async Task<Result<WalletData>> GetPrivateKey(int userId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var privateKey = await con.QueryFirstOrDefaultAsync<WalletData>(
                    "select u.PrivateKey from sp.vUser u where u.UserId = @UserId",
                    new {UserId = userId});
                if (privateKey == null) return Result.Failure<WalletData>(Status.NotFound, "Private key found.");
                return Result.Success(privateKey);
            }
        }

        public async Task<Result<int>> GetCredit(int userId)
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var credit = await con.QueryFirstOrDefaultAsync<int>(
                    "select m.Credit from sp.vMoney m where m.MoneyId = @userid and m.MoneyType = 1",
                    new {userid = userId});
                return Result.Success(credit);
            }
        }

        public async Task<Result<int>> GetAllCredit()
        {
            using (var con = new SqlConnection(_sqlstring))
            {
                var allCredit = await con.QueryFirstOrDefaultAsync<int>(
                    "select SUM(m.Credit) from sp.vMoney m");
                return Result.Success(allCredit);
            }
        }
    }
}