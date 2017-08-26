using Dapper;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManagement.IRepository.SalesModule;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Core.Document;
using IntegratedManagement.Entity.Document;

namespace IntegratedManagement.RepositoryDapper.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/16 23:56:32
	===============================================================================================================================*/
    public class SalesOrderDapperRepository: ISalesOrderRepository
    {
        public async Task<List<SalesOrder>> Fetch(QueryParam Param)
        {
            List<SalesOrder> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();

                string sql = $"SELECT  top {Param.limit} {Param.select} FROM T_SalesOrder t0 left JOIN T_SalesOrderItem t1 on t0.DocEntry = t1.DocEntry {Param.filter+" "+Param.orderby} ";
                try
                {
                    var coll = await conn.QueryParentChildAsync<SalesOrder, SalesOrderItem, int>(sql, p => p.DocEntry, p => p.SalesOrderItems, splitOn: "DocEntry");
                    collection = coll.ToList();
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

        public SalesOrder GetSalesOrder(int DocEntry)
        {
            throw new NotImplementedException();
        }

        public async Task<SaveResult> Save(SalesOrder SalesOrder)
        {
            SaveResult saveRlt = new SaveResult();
            saveRlt.UniqueKey = SalesOrder.OMSDocEntry.ToString();//回传接收方的主键
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"insert into T_SalesOrder(OMSDocEntry,OMSDocDate,DocType,BusinessType,PlatformCode,CardCode,OrderPaied,Freight,PayMethod,Comments,CreateDate,UpdateDate) 
                                    values(@OMSDocEntry,@OMSDocDate,@DocType,@BusinessType,@PlatfromCode,@CardCode,@OrderPaied,@Freight,@Paymenthod,@Comments,@CreateDate,@UpdateDate)select SCOPE_IDENTITY();";
                    string insertItemSql = @"insert into T_SalesOrderItem(DocEntry,LineNum,OMSDocEntry,OMSLineNum,ItemCode,Quantity,Price,ItemPaied) 
                                               values(@DocEntry,@LineNum,@OMSDocEntry,@OMSLineNum,@ItemCode,@Quantity,@Price,@ItemPaied)";

                    object DocEntry = await conn.ExecuteScalarAsync(insertSql, 
                        new { OMSDocEntry = SalesOrder.OMSDocEntry,
                            OMSDocDate = SalesOrder.OMSDocDate,
                          
                            DocType = SalesOrder.DocType,
                            BusinessType = SalesOrder.BusinessType,
                            PlatfromCode = SalesOrder.PlatformCode,
                            CardCode = SalesOrder.CardCode,
                            OrderPaied = SalesOrder.OrderPaied,
                            Freight = SalesOrder.Freight,
                            Paymenthod = SalesOrder.PayMethod,
                            Comments = SalesOrder.Comments,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now
                        }, dbTransaction);
                    saveRlt.ReturnUniqueKey = DocEntry.ToString();//回传保存订单的主键
                    await conn.ExecuteAsync(insertItemSql, DocumentItemHandle<SalesOrderItem>.GetDocumentItems(SalesOrder.SalesOrderItems, Convert.ToInt32(DocEntry)), dbTransaction);

                    dbTransaction.Commit();
                    saveRlt.Code = 0;
                }
                catch(Exception ex)
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

        public async Task<bool> UpdateINSyncDataBatch(DocumentSync documentSyncData)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = $@"update T_SalesOrder set IsINSync = '{documentSyncData.SyncResult}',
                                                        INSAPDocEntry = '{documentSyncData.SAPDocEntry}',
                                                        INMsg = '{documentSyncData.SyncMsg}',
                                                        INSyncDate='{DateTime.Now}'  
                                    where DocEntry in ( {documentSyncData.DocEntry} )";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql);
                    if (rtCount >= 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the refund document by docentry:{documentSyncData.DocEntry}");
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

        public async Task<bool> UpdateJESyncDataBatch(DocumentSync documentSyncData)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = $"update T_SalesOrder set IsJESync = @SyncResult,JESAPDocEntry = @SAPDocEntry,JEMsg = @SyncMsg,JESyncDate='{DateTime.Now}'  where DocEntry in ( @DocEntry )";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, new
                    {
                        DocEntry = documentSyncData.DocEntry,
                        SAPDocEntry = documentSyncData.SAPDocEntry,
                        SyncMsg = documentSyncData.SyncMsg,
                        SyncResult = documentSyncData.SyncResult
                    });
                    if (rtCount >= 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the refund document by docentry:{documentSyncData.DocEntry}");
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
