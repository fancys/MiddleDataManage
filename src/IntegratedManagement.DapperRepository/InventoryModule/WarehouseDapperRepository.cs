using IntegratedManagement.IRepository.InventoryModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.InventoryModule.Warehouse;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;

namespace IntegratedManagement.RepositoryDapper.InventoryModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/6 22:11:55
	===============================================================================================================================*/
    public class WarehouseDapperRepository : IWarehouseRepository
    {
        public async Task<List<Warehouse>> GetWarehouse(QueryParam queryParam)
        {
            List<Warehouse> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  top {queryParam.limit} {queryParam.select} FROM T_view_warehouse t0  {queryParam.filter + " " + queryParam.orderby} ";
                try
                {
                    var warehouseList = await conn.QueryAsync<Warehouse>(sql);
                    collection = warehouseList.ToList();
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
    }
}
