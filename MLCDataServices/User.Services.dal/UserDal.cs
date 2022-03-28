using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MLCCommonILibrary.Model;
using MLCCommonILibrary.System.Config;
using MLCServiceData.User.Service.dal;
using MLCServicesData.Classes;
using MLCServicesData.T_Sql;
using Dapper;
using MLCCommonLibrary.Model.User;
using MLCCommonLibrary.Model;

namespace MLCServicesData.User.Services.dal
{
    public class UserDal : IUsersDal
    {

        private readonly ConnectionString db_con; 
        public UserDal(IApplicationSettings appSetting, IConnectionSetting con)
        {
             db_con = new  ConnectionString(appSetting, con);
        }

        public async Task<IList<IRole>> LogIn(LogIn user)
        {
            try
            {
                 
                using (SqlConnection conn = new SqlConnection(db_con.DatabaseConnection))
                {

                    conn.Open();
                    return  (await conn.QueryAsync<UserRole>(Query_Users.sel_Login, new {
                                pUserName = user.UserName,
                                pPassword = EncryptionHelper.Encrypt(user.Password)
                            }, commandTimeout: 0)).ToList<IRole>();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<IRole>> GetUsers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(db_con.DatabaseConnection))
                {
                    conn.Open();

                    return (await conn.QueryAsync<UserRole>(Query_Users.sel_Users, commandTimeout: 0)).ToList<IRole>();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}