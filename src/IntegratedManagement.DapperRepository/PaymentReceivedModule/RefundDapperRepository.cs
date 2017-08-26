using IntegratedManagement.IRepository.PaymentReceivedModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.PaymentReceivedModule.Refund;
using IntegratedManagement.Entity.Result;
using System.Data;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;
using IntegratedManagement.Core.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Document;

namespace IntegratedManagement.RepositoryDapper.PaymentReceivedModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/27 10:04:23
	===============================================================================================================================*/
    public class RefundDapperRepository : IRefundRepository
    {
        public async Task<List<Refund>> GetRefund(QueryParam Param)
        {
            List<Refund> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();

                string sql = $"SELECT  top {Param.limit} {Param.select} FROM T_Refund t0 left JOIN T_RefundItem t1 on t0.DocEntry = t1.DocEntry {Param.filter + " " + Param.orderby} ";
                try
                {
                    var coll =await conn.QueryParentChildAsync<Refund, RefundItem, int>(sql, p => p.DocEntry, p => p.RefundItems, splitOn: "DocEntry");
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

        public Refund GetRefund(int DocEntry)
        {
           
            Refund refund = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();

                string sql = $"SELECT * FROM T_Refund t0 left JOIN T_RefundItem t1 on t0.DocEntry = t1.DocEntry where t0.DocEntry = {DocEntry} ";
                try
                {
                    refund = conn.QueryParentChild<Refund, RefundItem, int>(sql, p => p.DocEntry, p => p.RefundItems, splitOn: "DocEntry").ToList().First();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return refund;
            }
        }

        public async Task<SaveResult> Save(Refund Refund)
        {
            SaveResult saveRlt = new SaveResult();
            saveRlt.UniqueKey = Refund.OMSDocEntry.ToString();//回传接收方的主键
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"INSERT INTO T_Refund (OMSDocEntry,PlatformCode,DocType,BusinessType,RefundType,RefundReason,CardCode,GrossRefund,Freight,FrghtVendor,PayMethod,Comments,CreateDate,UpdateDate,Creator,UpDator)
                                        VALUES (@OMSDocEntry,@PlatformCode,@DocType,@BusinessType,@RefundType,@RefundReason,@CardCode,@GrossRefund,@Freight,@FrghtVendor,@PayMethod,@Comments,@CreateDate,@UpdateDate,@Creator,@UpDator)select SCOPE_IDENTITY();";
                    string insertItemSql = @"INSERT INTO T_RefundItem (DocEntry,LineNum,OMSDocEntry,OMSLineNum,ItemCode,Quantity,Price,ItemRefund)
                                        VALUES (@DocEntry,@LineNum,@OMSDocEntry,@OMSLineNum,@ItemCode,@Quantity,@Price,@ItemRefund)";
                    object DocEntry = await conn.ExecuteScalarAsync(insertSql,
                        new
                        {
                            OMSDocEntry = Refund.OMSDocEntry,
                            PlatformCode = Refund.PlatformCode,
                            DocType = Refund.DocType,
                            BusinessType = Refund.BusinessType,
                            RefundType = Refund.RefundType,
                            RefundReason = Refund.RefundReason,
                            CardCode = Refund.CardCode,
                            GrossRefund = Refund.GrossRefund,
                            Freight = Refund.Freight,
                            FrghtVendor = Refund.FrghtVendor,
                            PayMethod = Refund.PayMethod,
                            Comments = Refund.Comments,
                            CreateDate = DateTime.Now,
                            UpdateDate = Refund.UpdateDate,
                            Creator = Refund.Creator,
                            UpDator = Refund.UpDator
                        }, dbTransaction);
                    saveRlt.ReturnUniqueKey = DocEntry.ToString();//回传保存订单的主键
                    conn.Execute(insertItemSql, DocumentItemHandle<RefundItem>.GetDocumentItems(Refund.RefundItems, Convert.ToInt32(DocEntry)), dbTransaction);

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
                string sql = "update T_Refund set IsSync = @SyncResult,SAPDocEntry = @SAPDocEntry,SyncMsg = @SyncMsg  where DocEntry = @DocEntry";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, new { DocEntry = documentSyncResult.DocEntry,
                        SAPDocEntry = documentSyncResult.SAPDocEntry,
                        SyncMsg = documentSyncResult.SyncMsg,
                        SyncResult = documentSyncResult.SyncResult});
                    if (rtCount == 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the refund document by docentry:{documentSyncResult.DocEntry}");
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

        public async Task<bool> UpdateSyncDataBatch(DocumentSync documentSyncResult)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = "update T_Refund set IsSync = @SyncResult,SAPDocEntry = @SAPDocEntry,SyncDate = @SyncDate ,SyncMsg = @SyncMsg  where DocEntry in ( @DocEntry )";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, new
                    {
                        DocEntry = documentSyncResult.DocEntry,
                        SAPDocEntry = documentSyncResult.SAPDocEntry,
                        SyncMsg = documentSyncResult.SyncMsg,
                        SyncResult = documentSyncResult.SyncResult,
                        SyncDate = documentSyncResult.SyncDate
                    });
                    if (rtCount >= 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the refund document by docentry:{documentSyncResult.DocEntry}");
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
