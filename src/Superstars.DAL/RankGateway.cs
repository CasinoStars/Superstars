﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Superstars.DAL
{
    public class RankGateway
    {
        private readonly SqlConnexion _sqlConnexion;


        public RankGateway(SqlConnexion sqlConnexion)
        {
            _sqlConnexion = sqlConnexion;
        }

        public async Task<IEnumerable<string>> PseudoList()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<string>(
                    "select u.UserName from sp.vUser u where u.UserName is not null and u.UserName not like '#%' order by u.UserId ASC "
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerTrueProfitList()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select m.Profit from sp.tMoney m where m.MoneyTypeId = 1 "
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerProfitList()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select m.Profit from sp.tMoney m where m.MoneyTypeId = 0 "
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayersBlackJackWins()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Wins from sp.tStats s where GameTypeId = 1"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayersBlackJackLosses()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Losses from sp.tStats s where GameTypeId = 1"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayersYamsWins()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Wins from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameTypeId = 0"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerYamsLosses()
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryAsync<int>(
                    "select s.Losses from sp.tStats s left outer join sp.tUser u on s.UserId = u.UserId where GameTypeId = 0"
                );
            }
        }

        public async Task<IEnumerable<int>> GetPlayerYamsBets(int userid)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryFirstOrDefaultAsync<IEnumerable<int>>(
                    "select s.Pot from sp.tGameYams s left outer join sp.tUser u on s.UserId = @UserId where GameTypeId = 0",
                    new {UserId = userid});
            }
        }

        public async Task<IEnumerable<int>> GetPlayerBJBets(int userid)
        {
            using (var con = new SqlConnection(_sqlConnexion.connexionString))
            {
                return await con.QueryFirstOrDefaultAsync<IEnumerable<int>>(
                    "select s.Pot from sp.tGameBlackJack s left outer join sp.tUser u on s.UserId = @UserId where GameTypeId = 1",
                    new {UserId = userid});
            }
        }
    }
}