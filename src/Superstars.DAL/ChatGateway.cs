using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class ChatGateway
    {
        private string _connectionString;

        public ChatGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ChatData>> ListAll()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<ChatData>(@"select * from sp.vChat");
            }
        }

        public async Task<Result<int>> CreateMessage(int userId, string message)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@Message", message);

                return Result.Success(await con.ExecuteAsync("sp.sChatCreate", p, commandType: CommandType.StoredProcedure));
            }
        }
    }
}
