using IntegratedManagement.Core.ParamHandle;
using IntegratedManagement.Entity.InventoryModule.Material;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.IRepository.InventoryModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.InventoryModule
{
    public class MaterialApp: IMaterialApp
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialApp(IMaterialRepository IMaterialRepository)
        {
            _materialRepository = IMaterialRepository;
        }

        public async Task <List<Material>> GetMaterialAsync(QueryParam queryParam)
        {            
            var materialList = await _materialRepository.GetMaterial(QueryParamHandle.ParamHanle(queryParam));
            return materialList;
        }
        public async Task<SaveResult> SaveMaterialAsync(Material material)
        {
            return await _materialRepository.Save(material);
        }

        public async Task<bool> UpdateSyncDataAsync(string ItemCode)
        {
            return await _materialRepository.UpdateSyncData(ItemCode);
        }
        
        public async Task<bool> PatchMaterialAsync(Material material)
        {
            return await _materialRepository.Update(material);
        }
    }
}
