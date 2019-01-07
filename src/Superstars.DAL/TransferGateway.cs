using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Superstars.YamsFair;

namespace Superstars.DAL
{
    public class TransferGateway
    {
        private readonly SqlConnexion _sqlConnexion;

        public TransferGateway(SqlConnexion sqlConnection)
        {
            _sqlConnexion = sqlConnection;
        }

        public async Task<TransferData> GetTransferData(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryFirstOrDefaultAsync<TransferData>(
                    "select m.TransferId, m.UserId, m.Amount, m.ReceiverId from sp.tProvablyFair m where m.UserId = @userId",
                    new {UserId = userId});
            }
        }
    }
}