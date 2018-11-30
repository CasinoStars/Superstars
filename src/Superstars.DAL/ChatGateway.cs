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
        private SqlConnexion _sqlConnexion;

        public ChatGateway(SqlConnexion sqlConnexion)
        {
            _sqlConnexion = sqlConnexion;
        }

        public async Task<IEnumerable<ChatData>> ListAll()
        {
            using (SqlConnection con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<ChatData>(@"select * from sp.vChat");
            }
        }

        public async Task<Result<int>> CreateMessage(int userId, string message)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                p.Add("@Message", message);

                return Result.Success(await con.ExecuteAsync("sp.sChatCreate", p, commandType: CommandType.StoredProcedure));
            }
        }
    }
}
