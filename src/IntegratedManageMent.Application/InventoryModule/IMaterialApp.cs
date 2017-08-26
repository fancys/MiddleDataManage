using IntegratedManagement.Entity.InventoryModule.Material;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.InventoryModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 14:34:02
	===============================================================================================================================*/
    public interface IMaterialApp
    {
        Task<List<Material>> GetMaterialAsync(QueryParam queryParam);
        Task<SaveResult> SaveMaterialAsync(Material material);
        Task<bool> UpdateSyncDataAsync(string ItemCode);

        Task<bool> PatchMaterialAsync(Material material);
    }
}
