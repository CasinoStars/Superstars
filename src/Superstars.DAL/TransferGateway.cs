using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

        public async Task<IEnumerable<TransferData>> GetTransferData(int userId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<TransferData>(
                    "select * from sp.tTransfer m where m.UserId = @userId",
                    new {UserId = userId});
            }
        }

        public async Task<Result> CreateTransfer(int userId,int amount,int receiverId)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                var p = new DynamicParameters();
                p.Add("@userId", userId);
                p.Add("@amount", amount);
                p.Add("@TransferDate", DateTime.UtcNow);
                p.Add("@ReceiverId", receiverId);
                p.Add("@TransferId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await con.ExecuteAsync("sp.sTransferCreate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1)
                    return Result.Failure(Status.BadRequest, "An account with this pseudo already exists.");
                if (status == 2) return Result.Failure(Status.BadRequest, "An account with this email already exists.");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

    }
}