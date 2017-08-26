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
	*	Create by Fancy at 2017/3/22 17:38:31
	===============================================================================================================================*/
    public class ClientDapperRepository : IClientRepository
    {
        public async Task<Client> FindById(string Id)
        {
            Client client = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  *  FROM T_Client where Id =  @Id ";
                try
                {
                    var queryResult = await conn.QueryAsync<Client>(sql, new { Id = Id });
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

        public async Task<bool> Save(Client client)
        {
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"INSERT INTO T_Client(Id, Secret, Name, IsActive, RefreshTokenLifeTime, DateAdded)
                                                VALUES( @Id,  @Secret,  @Name,  @IsActive,  @RefreshTokenLifeTime,  @DateAdded)";
                    int rtCount = await conn.ExecuteAsync(insertSql,
                        new
                        {
                            Id = client.Id,
                            Secret = client.Secret,
                            Name = client.Name,
                            IsActive = client.IsActive,
                            RefreshTokenLifeTime = client.RefreshTokenLifeTime,
                            DateAdded = client.DateAdded
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
    }
}
