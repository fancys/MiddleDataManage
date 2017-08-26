using IntegratedManagement.IRepository.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Token;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;
using System.Data;

namespace IntegratedManagement.RepositoryDapper.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 17:35:45
	===============================================================================================================================*/
    public class RefreshTokenRepositoryDapper : IRefreshTokenRepository
    {
        public async Task<bool> Delete(string Id)
        {
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"delete  FROM T_RefreshToken where Id =  '{Id}' ";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, null);
                    if (rtCount == 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
               
            }
        }

        public async Task<RefreshToken> FindByClientId(string ClientId)
        {
            RefreshToken collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  *  FROM T_RefreshToken where ClientId =  '{ClientId}' ";
                try
                {
                    var queryResult = await conn.QueryAsync<RefreshToken>(sql, null);
                    collection = queryResult.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return collection;
            }
        }

        public async Task<RefreshToken> FindById(string Id)
        {
            RefreshToken collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  *  FROM T_RefreshToken where Id =  '{Id}' ";
                try
                {
                    var queryResult = await conn.QueryAsync<RefreshToken>(sql, null);
                    collection = queryResult.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return collection;
            }
        }

        public async Task<bool> Insert(RefreshToken refreshToken)
        {
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"INSERT INTO T_RefreshToken(Id,UserName,ClientId,IssuedUtc,ExpiresUtc,ProtectedTicket)
                                                    VALUES(@Id,@UserName,@ClientId,@IssuedUtc,@ExpiresUtc,@ProtectedTicket)";
                    string sql = $@"INSERT INTO T_RefreshToken(Id,UserName,ClientId,IssuedUtc,ExpiresUtc,ProtectedTicket)
                                                    VALUES('{refreshToken.Id}','{refreshToken.UserName}','{refreshToken.ClientId}',{refreshToken.IssuedUtc},
                                                            {refreshToken.ExpiresUtc},'{refreshToken.ProtectedTicket}')";
                    int rtCount = await conn.ExecuteAsync(insertSql,
                        new
                        {
                            Id = refreshToken.Id,
                            UserName = refreshToken.UserName,
                            ClientId = refreshToken.ClientId,
                            IssuedUtc = refreshToken.IssuedUtc,
                            ExpiresUtc = refreshToken.ExpiresUtc,
                            ProtectedTicket = refreshToken.ProtectedTicket
                        }, dbTransaction);
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

        public async Task<bool> Update(RefreshToken refreshToken)
        {
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"UPDATE T_RefreshToken SET Id = @Id,UserName=@UserName,ClientId = @ClientId,IssuedUtc = @IssuedUtc,ExpiresUtc = @ExpiresUtc,ProtectedTicket = @ProtectedTicket)";
                    int rtCount = await conn.ExecuteAsync(insertSql,
                        new
                        {
                            Id = refreshToken.Id,
                            UserName = refreshToken.UserName,
                            ClientId = refreshToken.ClientId,
                            IssuedUtc = refreshToken.IssuedUtc,
                            ExpiresUtc = refreshToken.ExpiresUtc,
                            ProtectedTicket = refreshToken.ProtectedTicket
                        }, dbTransaction);
                    dbTransaction.Commit();
                    if (rtCount == 1)
                        return true;
                    else return false;
                   
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
