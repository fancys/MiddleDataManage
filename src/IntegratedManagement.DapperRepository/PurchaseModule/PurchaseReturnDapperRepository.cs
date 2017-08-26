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
            saveRlt.UniqueKey = PurchaseReturn.OMSDocEntry.ToString();//回传接收方的主键
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"insert into T_PurchaseReturn(OMSDocEntry,OMSDocDate,BusinessType,CardCode,Comments,CreateDate,DocType,BatchNum) 
                                    values(@OMSDocEntry,@OMSDocDate,@BusinessType,@CardCode,@Comments,@CreateDate,@DocType,@BatchNum)select SCOPE_IDENTITY();";
                    string insertItemSql = @"insert into T_PurchaseReturnItem(DocEntry,LineNum,OMSDocEntry,OMSLineNum,ItemCode,Quantity,Price) 
                                               values(@DocEntry,@LineNum,@OMSDocEntry,@OMSLineNum,@ItemCode,@Quantity,@Price)";

                    object DocEntry = await conn.ExecuteScalarAsync(insertSql,
                        new
                        {
                            OMSDocEntry = PurchaseReturn.OMSDocEntry,
                            OMSDocDate = PurchaseReturn.OMSDocDate,
                            BusinessType = PurchaseReturn.BusinessType,
                            Comments = PurchaseReturn.Comments,
                            CardCode = PurchaseReturn.CardCode,
                            CreateDate = DateTime.Now,
                            DocType = PurchaseReturn.DocType,
                            BatchNum = PurchaseReturn.BatchNum
                        }, dbTransaction);
                    saveRlt.ReturnUniqueKey = DocEntry.ToString();//回传保存订单的主键
                    //保存行集合
                    conn.Execute(insertItemSql, DocumentItemHandle<PurchaseReturnItem>.GetDocumentItems(PurchaseReturn.PurchaseReturnItems, Convert.ToInt32(DocEntry)), dbTransaction);
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
