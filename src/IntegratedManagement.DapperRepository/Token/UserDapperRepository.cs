using IntegratedManagement.IRepository.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Token;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using System.Data;
using Dapper;

namespace IntegratedManagement.RepositoryDapper.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/24 9:58:29
	===============================================================================================================================*/
    public class UserDapperRepository : IUserRepository
    {
        public async Task<User> GetUser(string UserName, string Password)
        {
            User client = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  *  FROM T_User where UserName =  @UserName and Password = @Password";
                try
                {
                    var queryResult = await conn.QueryAsync<User>(sql, new { UserName = UserName,Password = Password});
                    client = queryResult.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return client;
            }
        }


        public async Task<User> GetUser(string UserName)
        {
            User client = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  *  FROM T_User where UserName =  @UserName";
                try
                {
                    var queryResult = await conn.QueryAsync<User>(sql, new { UserName = UserName });
                    client = queryResult.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return client;
            }
        }

        public async Task<bool> Insert(User User)
        {
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"INSERT INTO T_User(Id,UserName,Password,IsActive,Creator,CreateDate,UpdateTime,Updator,IsDelete)VALUES
                                            (@Id,@UserName,@Password,@IsActive,@Creator,@CreateDate,@UpdateTime,@Updator,@IsDelete)";
                    int rtCount = await conn.ExecuteAsync(insertSql,User, dbTransaction);
                    dbTransaction.Commit();
                    if (rtCount == 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
