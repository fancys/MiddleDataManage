using IntegratedManagement.IRepository.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Help;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;
using System.Data;

namespace IntegratedManagement.RepositoryDapper.Help
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/10/16 17:53:37
	===============================================================================================================================*/
    public class SerialNumberDapperRepository : ISerialNumberRepository
    {
        public async Task<SerialNumber> GetSerialNumber()
        {
            List<SerialNumber> customerList = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  *  FROM T_SerialNumber where Year = {DateTime.Now.Year} and Month = {DateTime.Now.Month}";
                try
                {
                    var collection = await conn.QueryAsync<SerialNumber>(sql);
                    customerList = collection.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return customerList.FirstOrDefault();
            }
        }

        public async Task<SaveResult> Save(SerialNumber serialNumber)
        {
            SaveResult saveRlt = new SaveResult();
            saveRlt.UniqueKey = serialNumber.ObjectKey.ToString();//回传接收方的主键
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"INSERT INTO T_SerialNumber(Year,Month,CurrentNumber)
                                        VALUES (@Year,@Month,@CurrentNumber)";
                    await conn.ExecuteScalarAsync(insertSql,
                        new
                        {
                            Year = serialNumber.Year,
                            Month = serialNumber.Month,
                            CurrentNumber = serialNumber.CurrentNumber,
                        }, dbTransaction);
                    
                    dbTransaction.Commit();
                    saveRlt.Code = 0;
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    saveRlt.Code = 1;
                    saveRlt.Message = ex.Message;
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return saveRlt;
            }
        }

        public async Task<bool> Update(SerialNumber serialNumber)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = $@"update T_SerialNumber set CurrentNumber = '{serialNumber.CurrentNumber}'
                                    where ObjectKey in ( {serialNumber.ObjectKey} )";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql);
                    if (rtCount >= 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the refund document by docentry:{serialNumber.ObjectKey}");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return isSuccessOperate;
            }
        }
    }
}
