using IntegratedManagement.IRepository.PurchaseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.PurchaseModule.PurchaseReturn;
using IntegratedManagement.Entity.Result;
using System.Data;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;
using IntegratedManagement.Core.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Document;

namespace IntegratedManagement.RepositoryDapper.PurchaseModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/24 11:36:08
	===============================================================================================================================*/
    public class PurchaseReturnDapperRepository : IPurchaseReturnRepository
    {
        public async Task<List<PurchaseReturn>> GetPurchaseReturn(QueryParam queryParam)
        {
            List<PurchaseReturn> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();

                string sql = $@"SELECT  top {queryParam.limit} {queryParam.select} FROM T_PurchaseReturn t0 left JOIN T_PurchaseReturnItem t1 on t0.DocEntry = t1.DocEntry 
                                        {queryParam.filter + " " + queryParam.orderby} ";
                try
                {
                    var purchaseReturnList = await conn.QueryParentChildAsync<PurchaseReturn, PurchaseReturnItem, int>(sql, p => p.DocEntry, p => p.PurchaseReturnItems, splitOn: "DocEntry");
                    collection = purchaseReturnList.ToList();

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

        public PurchaseReturn GetPurchaseReturn(int DocEntry)
        {
            throw new NotImplementedException();
        }

        public async Task<SaveResult> Save(PurchaseReturn PurchaseReturn)
        {
            SaveResult saveRlt = new SaveResult();
           
                return saveRlt;
            
        }

        public async Task<bool> UpdateSyncData(DocumentSync documentSyncResult)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = "update T_PurchaseReturn set IsSync = @IsSync,SAPDocEntry = @SAPDocEntry,SyncDate = @SyncDate,SyncMsg = @SyncMsg  where DocEntry = @DocEntry";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, new
                    {
                        DocEntry = documentSyncResult.DocEntry,
                        SAPDocEntry = documentSyncResult.SAPDocEntry,
                        IsSync = documentSyncResult.SyncResult,
                        SyncMsg = documentSyncResult.SyncMsg,
                        SyncDate = documentSyncResult.SyncDate
                    });
                    if (rtCount == 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the customer by cardcode:{documentSyncResult.DocEntry}");
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
