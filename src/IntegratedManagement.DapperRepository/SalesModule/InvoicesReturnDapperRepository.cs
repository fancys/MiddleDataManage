using Dapper;
using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.InvoicesReturn;
using IntegratedManagement.IRepository.SalesModule;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.RepositoryDapper.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 11:57:49
	===============================================================================================================================*/
    public class InvoicesReturnDapperRepository : IInvoicesReturnRepository
    {
        public List<InvoicesReturn> Fetch(QueryParam Param)
        {
            throw new NotImplementedException();
        }

        public InvoicesReturn GetInvoicesReturn(int DocEntry)
        {
            throw new NotImplementedException();
        }

        public async Task<SaveResult> Save(InvoicesReturn InvoicesReturn)
        {
            SaveResult saveRlt = new SaveResult();
            saveRlt.UniqueKey = InvoicesReturn.ZFPHM;//回传接收方的主键
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = $@"INSERT INTO [@AVA_INVRETURN] (Code,Name,U_ZFPDM,U_ZKPRQ,U_ZFPZT,U_ZFPJE,U_ZSKPH)
                                            VALUES (
                                            '{InvoicesReturn.ZFPHM}',
                                            '{InvoicesReturn.ZFPHM}',
                                            '{InvoicesReturn.ZFPDM}',
                                            '{InvoicesReturn.ZKPRQ}',
                                            '{InvoicesReturn.ZFPZT}',
                                            '{InvoicesReturn.ZFPJE}',
                                            '{InvoicesReturn.ZSKPH}')";

                    //string insertSql = $@"INSERT INTO [@AVA_INVRETURN] (Code,Name,U_ZFPDM,U_ZKPRQ,U_ZFPZT,U_ZFPJE,U_ZSKPH)
                    //                        VALUES (@Code,@Name,@U_ZFPDM,@U_ZKPRQ,@U_ZFPZT,@U_ZFPJE,@U_ZSKPH)";
                    var rt = await conn.ExecuteScalarAsync(insertSql,null, dbTransaction);
                    //await conn.ExecuteAsync(insertSql);


                    saveRlt.ReturnUniqueKey = InvoicesReturn.ZFPHM;//回传保存订单的主键
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

        public Task<bool> UpdateINSyncDataBatch(DocumentSync documentSyncData)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateJESyncDataBatch(DocumentSync documentSyncData)
        {
            throw new NotImplementedException();
        }
    }
}
