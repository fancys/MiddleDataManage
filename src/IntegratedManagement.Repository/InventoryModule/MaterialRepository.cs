using System.Collections.Generic;
using IntegratedManagement.IRepository.InventoryModule;
using IntegratedManagement.Entity.InventoryModule.Material;
using System;
using IntegratedManagement.Entity.Result;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Param;

namespace IntegratedManagement.Repository.InventoryModule
{
    public class MaterialRepository 
    {
        public List<Material> GetMaterial(QueryParam queryParam)
        {
            throw new NotImplementedException();
        }

        public Material GetMaterial(string ItemCode)
        {
            throw new NotImplementedException();
        }

        public Task<SaveResult> Save(Material Material)
        {
            throw new NotImplementedException();
        }
    }
}
