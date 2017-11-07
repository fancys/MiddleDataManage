using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.InventoryModule.Warehouse;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.IRepository.InventoryModule;
using IntegratedManagement.Core.ParamHandle;
using IntegrateManagement.MiddleBaseService.B1;

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

        public string CreateWarehouse(List<Warehouse> warehouses)
        {
            StringBuilder rtStr = new StringBuilder();
            foreach (var item in warehouses)
            {
                try
                {
                    var rt = WarhouseService.AddOrUpdateWarehouse(item);
                    if(rt.SyncResult == "N")
                    {
                        rtStr.Append(rt.SyncMsg + ";");
                    }
                }
                catch(Exception ex)
                {
                    rtStr.Append(ex.Message + ";");
                }
            }
            if (string.IsNullOrEmpty(rtStr.ToString()))
            {
                return "同步成功。";
            }
            return rtStr.ToString();
        }

        public async Task<List<Warehouse>> GetWarehouseAsync(QueryParam queryParam)
        {
            return await _IWarehouseRepository.GetWarehouse(QueryParamHandle.ParamHanle(queryParam));
        }
    }
}
