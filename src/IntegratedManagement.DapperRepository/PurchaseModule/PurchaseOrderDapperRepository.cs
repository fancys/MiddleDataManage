using IntegratedManagement.IRepository.PurchaseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.PurchaseModule.PurchaseOrder;
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
	*	Create by Fancy at 2017/3/17 0:11:11
    *	使用Dapper ORM 对采购订单进行CURD操作
	===============================================================================================================================*/
    public class PurchaseOrderDapperRepository : IPurchaseOrderRepository
    {
        public async Task<List<PurchaseOrder>> GetPurchaseOrder(QueryParam queryParam)
        {
            List<PurchaseOrder> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();

                string sql = $@"SELECT  top {queryParam.limit} {queryParam.select} FROM T_PurchaseOrder t0 left JOIN T_PurchaseOrderItem t1 on t0.DocEntry = t1.DocEntry 
                                        {queryParam.filter + " " + queryParam.orderby} ";
                try
                {
                    var purchaseList = await conn.QueryParentChildAsync<PurchaseOrder, PurchaseOrderItem, int>(sql, p => p.DocEntry, p => p.PurchaseOrderItems, splitOn: "DocEntry");
                    collection = purchaseList.ToList();
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

        public PurchaseOrder GetPurchaseOrder(int DocEntry)
        {
            throw new NotImplementedException();
        }

        public async Task<SaveResult> Save(PurchaseOrder PurchaseOrder)
        {
            SaveResult saveRlt = new SaveResult();
            saveRlt.UniqueKey = PurchaseOrder.OMSDocEntry.ToString();//回传接收方的主键
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"insert into T_PurchaseOrder(OMSDocEntry,OMSDocDate,BusinessType,CardCode,Comments,CreateDate,DocType,BatchNum) 
                                    values(@OMSDocEntry,@OMSDocDate,@BusinessType,@CardCode,@Comments,@CreateDate,@DocType,@BatchNum)select SCOPE_IDENTITY();";
                    string insertItemSql = @"insert into T_PurchaseOrderItem(DocEntry,LineNum,OMSDocEntry,OMSLineNum,ItemCode,Quantity,Price) 
                                               values(@DocEntry,@LineNum,@OMSDocEntry,@OMSLineNum,@ItemCode,@Quantity,@Price)";
                    object DocEntry = await conn.ExecuteScalarAsync(insertSql,
                        new
                        {
                            OMSDocEntry = PurchaseOrder.OMSDocEntry,
                            OMSDocDate = PurchaseOrder.OMSDocDate,
                            BusinessType = PurchaseOrder.BusinessType,
                            Comments = PurchaseOrder.Comments,
                            CardCode = PurchaseOrder.CardCode,
                            CreateDate = DateTime.Now,
                            DocType = PurchaseOrder.DocType,
                            BatchNum = PurchaseOrder.BatchNum
                        }, dbTransaction);
                    saveRlt.ReturnUniqueKey = DocEntry.ToString();//回传保存订单的主键
                    await conn.ExecuteAsync(insertItemSql, DocumentItemHandle<PurchaseOrderItem>.GetDocumentItems(PurchaseOrder.PurchaseOrderItems,Convert.ToInt32(DocEntry)), dbTransaction);
                       
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
                string sql = "update T_PurchaseOrder set IsSync = @IsSync,SAPDocEntry = @SAPDocEntry,SyncDate = @SyncDate,SyncMsg = @SyncMsg  where DocEntry = @DocEntry";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, new { DocEntry = documentSyncResult.DocEntry,
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
