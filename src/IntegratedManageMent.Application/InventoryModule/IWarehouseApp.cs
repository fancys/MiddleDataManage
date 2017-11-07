using IntegratedManagement.Entity.InventoryModule.Warehouse;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.InventoryModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/6 22:07:11
	===============================================================================================================================*/
    public interface IWarehouseApp
    {
        Task<List<Warehouse>> GetWarehouseAsync(QueryParam queryParam);

        string CreateWarehouse(List<Warehouse> warehouses);
    }
}
