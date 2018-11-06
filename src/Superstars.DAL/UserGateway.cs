﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Dapper;

namespace Superstars.DAL
{
    public class UserGateway
    {
        private readonly string _connectionString;


        public UserGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UserData> FindById(int userId)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select u.UserId, u.Email, u.UserName, u.UserPassword, u.PrivateKey, u.Isingameyams, u.Isingameblackjack u from sp.vUser u where u.UserId = @UserId",
                    new {UserId = userId});
            }
        }

        public async Task<UserData> FindByName(string pseudo)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select u.UserId, u.Email, u.UserName, u.UserPassword, u.PrivateKey, u.Isingameyams, u.Isingameblackjack from sp.vUser u where u.UserName = @UserName",
                    new {UserName = pseudo});
            }
        }

        public async Task<UserData> FindByEmail(string email)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select u.UserId, u.Email, u.UserName, u.UserPassword, u.PrivateKey, u.Isingameyams, u.Isingameblackjack from sp.vUser u where u.Email = @Email",
                    new {Email = email});
            }
        }

        public async Task<Result> CreateUser(string pseudo, byte[] password, string email, string privateKey)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@Email", email);
                p.Add("@UserName", pseudo);
                p.Add("@UserPassword", password);
                p.Add("@PrivateKey", privateKey);
                p.Add("@Country", "France");
                p.Add("@Isingameyams", 0);
                p.Add("@Isingameblackjack", 0);
                p.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                await con.ExecuteAsync("sp.sUserCreate", p, commandType: CommandType.StoredProcedure);

                var status = p.Get<int>("@Status");
                if (status == 1)
                    return Result.Failure(Status.BadRequest, "An account with this pseudo already exists.");
                if (status == 2) return Result.Failure(Status.BadRequest, "An account with this email already exists.");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

        public async Task ActionConnexion(int userid, string username, DateTime date)
        {

            //XDocument xDoc = new XDocument( 
            //    new XDeclaration("1.0","UTF-16",null),
            //    new XElement("UserID", userid),
            //    new XElement("UserName", username),
            //    new XElement("Date", date.ToString() ));
            //StringWriter sw = new StringWriter();
            //XmlWriter xWrite = XmlWriter.Create(sw);
            //xDoc.Save(xWrite);
            //xWrite.Close();
            //xDoc.Elements("UserID");                                              

            string action = "Player named " + username + " with UserID " + userid + " connected at " + date.ToString();

            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync("sp.sLogTableCreate", new { UserId = userid, ActionDate = date, ActionDescription = action },
                    commandType: CommandType.StoredProcedure);
            }    
        }

        public async Task ActionDeconnexion(int userid, string username, DateTime date)
        {
            string action = "Player named " + username + " with UserID " + userid + " disconnected at " + date.ToString();

            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync("sp.sLogTableCreate", new { UserId = userid, ActionDate = date, ActionDescription = action },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Delete(int userId)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync("sp.sUserDelete", new {UserId = userId},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateEmail(int userId, string email)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "sp.sUserUpdate",
                    new {UserId = userId, Email = email},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateName(int userId, string pseudo)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "sp.sUserUpdate",
                    new {UserId = userId, UserName = pseudo},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePassword(int userId, byte[] password)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "sp.sUserUpdate",
                    new {UserId = userId, UserPassword = password},
                    commandType: CommandType.StoredProcedure);
            }
        }


        public async Task UpdateLastConnexion(int userId, DateTime time)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "sp.sUserUpdate",
                    new {UserId = userId, LastConnexionDate = time},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateLastDeconnexion(int userId, DateTime time)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "sp.sUserUpdate",
                    new {UserId = userId, LastDeconnexionDate = time},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateCountry(int userId, string country)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "sp.sUserUpdate",
                    new {UserId = userId, Country = country},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> GetIsingameyams(int userid)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<int>(
                    "select u.Isingameyams from sp.vUser u where u.UserId = @UserID",
                    new {UserID = userid});
            }
        }

        public async Task<int> GetIsingameblackjack(int userid)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<int>(
                    "select u.Isingameblackjack from sp.vUser u where u.UserId = @UserID",
                    new {UserID = userid});
            }
        }

        public async Task UpdateIsingameyams(int userId, int isingame)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.QueryFirstOrDefaultAsync<Task>(
                    "update sp.tUser set Isingameyams = @Isingameyams where UserId = @UserID",
                    new {UserID = userId, Isingameyams = isingame});
            }
        }

        public async Task UpdateIsingameblackjack(int userId, int isingame)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                await con.QueryFirstOrDefaultAsync<Task>(
                    "update sp.tUser set Isingameblackjack = @Isingamebj where UserId = @UserID",
                    new { UserID = userId, Isingamebj = isingame });
            }
        }
    }
}