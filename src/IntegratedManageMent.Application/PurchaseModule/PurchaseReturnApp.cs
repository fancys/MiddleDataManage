using IntegratedManagement.Core.ParamHandle;
using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseReturn;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.IRepository.PurchaseModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.PurchaseModule
{
    public class PurchaseReturnApp: IPurchaseReturnApp
    {
        private readonly IPurchaseReturnRepository _purchaseReturnRepository;

        public PurchaseReturnApp(IPurchaseReturnRepository IPurchaseReturnRepository)
        {
            _purchaseReturnRepository = IPurchaseReturnRepository;
        }

        public async Task<List<PurchaseReturn>> GetPurchaseReturnAsync(QueryParam queryParam)
        {
            return await _purchaseReturnRepository.GetPurchaseReturn(QueryParamHandle.ParamHanle(queryParam));
        }

        public async Task<SaveResult> SavePurchaseReturnAsync(PurchaseReturn purchaseReturn)
        {
            return await _purchaseReturnRepository.Save(purchaseReturn);
        }

        public async Task<bool> UpdateSyncDataAsync(DocumentSync documentSyncResult)
        {
            return await _purchaseReturnRepository.UpdateSyncData(documentSyncResult);
        }
    }
}
