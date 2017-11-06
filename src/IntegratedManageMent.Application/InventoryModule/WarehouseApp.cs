using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.InventoryModule.Warehouse;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.IRepository.InventoryModule;
using IntegratedManagement.Core.ParamHandle;

namespace IntegratedManageMent.Application.InventoryModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/6 22:06:54
	===============================================================================================================================*/
    public class WarehouseApp : IWarehouseApp
    {
        protected readonly IWarehouseRepository _IWarehouseRepository;
        public WarehouseApp(IWarehouseRepository IWarehouseRepository)
        {
            this._IWarehouseRepository = IWarehouseRepository;
        }
        public async Task<List<Warehouse>> GetWarehouseAsync(QueryParam queryParam)
        {
            return await _IWarehouseRepository.GetWarehouse(QueryParamHandle.ParamHanle(queryParam));
        }
    }
}
