using IntegratedManagement.Entity.InventoryModule.Material;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.InventoryModule
{
    public interface IMaterialRepository
    {
        Task<List<Material>> GetMaterial(QueryParam queryParam);
        Material GetMaterial(string itemCode);

        Task<SaveResult> Save(Material material);

        Task<bool> Update(Material material);

        Task<bool> UpdateSyncData(string itemCode);
    }
}
