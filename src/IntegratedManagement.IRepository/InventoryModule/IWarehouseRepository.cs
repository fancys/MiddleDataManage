using IntegratedManagement.Entity.InventoryModule.Warehouse;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.InventoryModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/6 22:11:02
	===============================================================================================================================*/
    public interface IWarehouseRepository
    {
        Task<List<Warehouse>> GetWarehouse(QueryParam queryParam);
    }
}
