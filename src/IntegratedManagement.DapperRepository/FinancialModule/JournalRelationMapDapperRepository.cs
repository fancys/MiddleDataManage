using IntegratedManagement.IRepository.FinancialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using IntegratedManagement.Entity.Result;
using System.Data;
using Dapper;
using IntegratedManagement.Core.Document;
using IntegratedManagement.Entity.Document;

namespace IntegratedManagement.RepositoryDapper.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:45:54
	===============================================================================================================================*/
    public class JournalRelationMapDapperRepository : IJournalRelationMapRepository
    {
        public async Task<List<JournalRelationMap>> GetJournalRelationMapList(QueryParam queryParam)
        {
            List<JournalRelationMap> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"select * from(SELECT  top {queryParam.limit} {queryParam.select} FROM T_JournalRelationMap t0  {queryParam.filter + " " + queryParam.orderby}) t2 inner JOIN T_JournalRelationMapItem t1 on t2.DocEntry = t1.DocEntry ";
                try
                {
                    var coll = await conn.QueryParentChildAsync<JournalRelationMap, JournalRelationMapLine, int>(sql, p => p.DocEntry, p => p.JournalRelationMapLines, splitOn: "DocEntry");
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

        public async Task<bool> ModifyJournalRelationMapMinus(DocumentSync documentSyncData)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = $@"update T_VIEW_JournalRelationMap set IsMinusSync = '{documentSyncData.SyncResult}',
                                                        NewMinusTransId = '{documentSyncData.SAPDocEntry}',
                                                        MinusSyncMessage = '{documentSyncData.SyncMsg}',
                                                        MinusSyncDate='{DateTime.Now}'  
                                    where DocEntry in ( {documentSyncData.DocEntry} )";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql);
                    if (rtCount >= 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the journalSource info by docentry:{documentSyncData.DocEntry}");
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

        public async Task<bool> ModifyJournalRelationMapPositive(DocumentSync documentSyncData)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = $@"update T_JournalRelationMap set IsPositiveSync = '{documentSyncData.SyncResult}',
                                                        NewPositiveTransId = '{documentSyncData.SAPDocEntry}',
                                                        PositiveSyncMessage = '{documentSyncData.SyncMsg}',
                                                        PositiveSyncDate='{DateTime.Now}'  
                                    where DocEntry in ( {documentSyncData.DocEntry} )";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql);
                    if (rtCount >= 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the journalSource info by docentry:{documentSyncData.DocEntry}");
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

        public async Task<bool> ModifyJournalRelationMapStatus(DocumentSync documentSyncData)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = $@"update T_JournalRelationMap set IsSync = '{documentSyncData.SyncResult}',
                                                        NewTransId = '{documentSyncData.SAPDocEntry}',
                                                        SyncMessage = '{documentSyncData.SyncMsg}',
                                                        SyncDate='{DateTime.Now}'  
                                    where DocEntry in ( {documentSyncData.DocEntry} )";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql);
                    if (rtCount >= 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the journalSource info by docentry:{documentSyncData.DocEntry}");
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

        public async Task<SaveResult> SaveJournalRelationMap(JournalRelationMap JournalRelationMap)
        {
            SaveResult saveRlt = new SaveResult();
            saveRlt.UniqueKey = JournalRelationMap.TransId.ToString();//回传接收方的主键
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"INSERT INTO T_JournalRelationMap
                                            (Number,TransId,TransType,BPLId,Series,BaseRef,Memo,RefDate,DueDate,TaxDate,CreateDate,BPLName,Ref1,Ref2,Ref3,Creator,Approver,BtfLine,IsApart)    
                                            VALUES(@Number,@TransId,@TransType,@BPLId,@Series,@BaseRef,@Memo,@RefDate,@DueDate,@TaxDate,@CreateDate,@BPLName,@Ref1,@Ref2,@Ref3,@Creator,@Approver,@BtfLine,@IsApart)
                                            select SCOPE_IDENTITY();";
                    string insertItemSql = @"INSERT INTO T_JournalRelationMapItem
                                            (DocEntry,LineNum,TransId,LineId,BPLId,AcctCode,ShorName,ProfitCode,OcrCode2,OcrCode3,OcrCode4,OcrCode5,LineMemo,CardCode,CardName,Credit,Debit)     
                                            VALUES(@DocEntry,@LineNum,@TransId,@LineId,@BPLId,@AcctCode,@ShorName,@ProfitCode,@OcrCode2,@OcrCode3,@OcrCode4,@OcrCode5,@LineMemo,@CardCode,@CardName,@Credit,@Debit)";

                    object DocEntry = await conn.ExecuteScalarAsync(insertSql,
                        new
                        {
                            Number = JournalRelationMap.Number,
                            TransId = JournalRelationMap.TransId,
                            BPLId = JournalRelationMap.BPLId,
                            Series = JournalRelationMap.Series,
                            BaseRef = JournalRelationMap.BaseRef,
                            Memo = JournalRelationMap.Memo,
                            RefDate = JournalRelationMap.RefDate,
                            DueDate = JournalRelationMap.DueDate,
                            TaxDate = JournalRelationMap.TaxDate,
                            CreateDate = JournalRelationMap.CreateDate,
                            BPLName = JournalRelationMap.BPLName,
                            Ref1 = JournalRelationMap.Ref1,
                            Ref2 = JournalRelationMap.Ref2,
                            Ref3 = JournalRelationMap.Ref3,
                            Creator = JournalRelationMap.Creator,
                            Approver = JournalRelationMap.Approver,
                            BtfLine = JournalRelationMap.BtfLine,
                            IsApart = JournalRelationMap.IsApart,
                            TransType=JournalRelationMap.TransType
                        }, dbTransaction);
                    saveRlt.ReturnUniqueKey = DocEntry.ToString();//回传保存订单的主键
                    await conn.ExecuteAsync(insertItemSql, DocumentItemHandle<JournalRelationMapLine>.GetDocumentItems(JournalRelationMap.JournalRelationMapLines, Convert.ToInt32(DocEntry)), dbTransaction);

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
    }
}
