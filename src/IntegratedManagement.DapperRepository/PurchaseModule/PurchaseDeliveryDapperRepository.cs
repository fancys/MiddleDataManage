using IntegratedManagement.IRepository.PurchaseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseDelivery;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.RepositoryDapper.BaseRepository;

namespace IntegratedManagement.RepositoryDapper.PurchaseModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/7 15:57:31
	===============================================================================================================================*/
    public class PurchaseDeliveryDapperRepository : IPurchaseDeliveryRepository
    {
        public async Task<PurchaseDelivery> GetPurchaseDelivery(int DocEntry)
        {
            List<PurchaseDelivery> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();

                string sql = $"SELECT  * FROM AVA_SP_VIEW_PURCHASE_OPOR t0 left JOIN AVA_SP_VIEW_PURCHASE_OPORLINE t1 on t0.DocEntry = t1.DocEntry where t0.DocEntry = {DocEntry} ";
                try
                {
                    var coll = await conn.QueryParentChildAsync<PurchaseDelivery, PurchaseDeliveryItem, int>(sql, p => p.DocEntry, p => p.PurchaseDeliveryItems, splitOn: "DocEntry");
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
                return collection.FirstOrDefault();
            }
        }

        public async Task<List<PurchaseDelivery>> GetPurchaseDeliveryList(QueryParam Param)
        {
            List<PurchaseDelivery> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();

                string sql = $"SELECT  top {Param.limit} {Param.select} FROM AVA_SP_VIEW_PURCHASE_OPOR t0 left JOIN AVA_SP_VIEW_PURCHASE_OPORLINE t1 on t0.DocEntry = t1.DocEntry {Param.filter + " " + Param.orderby} ";
                try
                {
                    var coll = await conn.QueryParentChildAsync<PurchaseDelivery, PurchaseDeliveryItem, int>(sql, p => p.DocEntry, p => p.PurchaseDeliveryItems, splitOn: "DocEntry");
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

        public async Task<SaveResult> Save(PurchaseDelivery PurchaseOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateSyncData(DocumentSync documentSyncResult)
        {
            throw new NotImplementedException();
        }
    }
}
