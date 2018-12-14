using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class WalletGateway
    {
        private readonly SqlConnexion _sqlConnexion;

        public WalletGateway(SqlConnexion sqlConnexion)
        {
            _sqlConnexion = sqlConnexion;
        }

        /// <summary>
        /// Add/Update Coins (real or fake) in t.userMoney
        /// </summary>
        /// <param name="userId">UserId of specific user</param>
        /// <param name="moneyTypeId">MoneyType is specified in t.MoneyType (0=> Fakecoin; 1=> Bitcoin)</param>
        /// <param name="coins">Number of coins to add in t.UserMoney</param>
        /// <param name="profit">Profit if user win or Loose a game</param>
        /// <param name="credit">Credit if user win or Loose with Bitcoin (Never credit if moneyType = 0)</param>
        /// <returns></returns>
        public async Task<Result<int>> AddCoins(int userId, int moneyTypeId, int coins, int credit)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@Credit", credit);
                p.Add("@UserId", userId);
                p.Add("@Balance", coins);
                p.Add("@MoneyTypeId", moneyTypeId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("sp.sMoneyUpdate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "Money type do no exist");

                return Result.Success(p.Get<int>("@Balance"));
            }
        }

        public async Task<Result> InsertInBankRoll(int trueCoins, int fakeCoins)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
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
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var bankRoll = await con.QueryFirstOrDefaultAsync<int>("select RealCoins from sp.tBankRoll");
                return Result.Success(bankRoll);
            }
        }

        public async Task<Result<int>> GetFakeBankRoll()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var bankRoll = await con.QueryFirstOrDefaultAsync<int>("select FakeCoins from sp.tBankRoll");
                return Result.Success(bankRoll);
            }
        }


        public async Task<Result<WalletData>> GetFakeBalance(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var wallet = await con.QueryFirstOrDefaultAsync<WalletData>(
                    "select m.UserId, m.MoneyTypeId, m.Balance from sp.vMoney m where m.UserId = @UserId and m.MoneyTypeId = 0",
                    new {UserId = userId});
                if (wallet == null) return Result.Failure<WalletData>(Status.NotFound, "Wallet not found.");
                return Result.Success(wallet);
            }
        }

        public async Task<Result<WalletData>> GetPrivateKey(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
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
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var credit = await con.QueryFirstOrDefaultAsync<int>(
                    "select m.Credit from sp.vMoney m where m.UserId = @UserId and m.MoneyTypeId = 1",
                    new {UserId = userId});
                return Result.Success(credit);
            }
        }

        public async Task<Result<int>> GetAllCredit()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var allCredit = await con.QueryFirstOrDefaultAsync<int>(
                    "select coalesce(SUM(m.Credit),0) from sp.tMoney m");
                return Result.Success(allCredit);
            }
        }
    }
}