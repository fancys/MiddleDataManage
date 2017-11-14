using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Result;
using System.Data;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;
using IntegratedManagement.IRepository.BusinessPartnerModule;
using IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner;
using IntegratedManagement.Entity.Param;
using System.Data.SqlClient;

namespace IntegratedManagement.RepositoryDapper.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/24 12:04:03
	===============================================================================================================================*/
    public class BusinessPartnerDapperRepository : IBusinessPartnerRepository
    {
        public async Task<List<BusinessPartner>> Fetch(QueryParam queryParam)
        {
            List<BusinessPartner> customerList = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  top {queryParam.limit} {queryParam.select} FROM T_BusinessPartner t0  {queryParam.filter + " " + queryParam.orderby} ";
                try
                {
                    var collection = await conn.QueryAsync<BusinessPartner>(sql);
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
                return customerList;
            }
        }

        public async Task<SaveResult> Save(BusinessPartner BusinessPartner)
        {
            SaveResult saveRlt = new SaveResult();
            saveRlt.UniqueKey = BusinessPartner.CardCode;//回传接收方的主键
            using (IDbConnection conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                IDbTransaction dbTransaction = conn.BeginTransaction();
                try
                {
                    string insertSql = @"INSERT INTO T_BusinessPartner(PlatformCode,CardCode,CardName,CreateDate,UpdateDate)
                                        VALUES (@PlatformCode,@CardCode,@CardName,@CreateDate,@UpdateDate)";
                    await conn.ExecuteScalarAsync(insertSql,
                        new
                        {
                           // PlatformCode = BusinessPartner.PlatformCode,
                            CardCode = BusinessPartner.CardCode,
                            CardName = BusinessPartner.CardName,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now
                        }, dbTransaction);
                    saveRlt.ReturnUniqueKey = BusinessPartner.CardCode;//回传保存订单的主键
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

        public async Task<bool> Update(BusinessPartner businessPartner)
        {
            if (businessPartner == null || string.IsNullOrEmpty(businessPartner.CardCode))
                throw new Exception("the customer is null or the cardcode of customer is null.");
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = "update T_BusinessPartner set PlatformCode = @PlatformCode,IsSync=@IsSync,CardName = @CardName,UpdateDate=@UpdateDate  where CardCode = @CardCode";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, new {
                        CardName = businessPartner.CardName,
                       // PlatformCode = businessPartner.PlatformCode,
                        UpdateDate = DateTime.Now,
                        CardCode = businessPartner.CardCode,
                        IsSync="N"
                    });
                    if (rtCount == 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the customer by cardcode:{businessPartner.CardCode}");
                }
                catch(Exception ex)
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

        public async Task<bool> UpdateSyncData(string CardCode)
        {
            bool isSuccessOperate = false;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                string sql = "update T_BusinessPartner set IsSync = 'Y'  where CardCode = @CardCode";
                try
                {
                    var rtCount = await conn.ExecuteAsync(sql, new { CardCode = CardCode });
                    if (rtCount == 1)
                        isSuccessOperate = true;
                    else if (rtCount == 0)
                        throw new Exception($"can't found the customer by cardcode:{CardCode}");
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
