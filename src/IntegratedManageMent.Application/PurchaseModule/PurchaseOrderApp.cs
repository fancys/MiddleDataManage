using IntegratedManagement.Core.ParamHandle;
using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseOrder;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.IRepository.PurchaseModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.PurchaseModule
{
    public class PurchaseOrderApp:IPurchaseOrderApp
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository ;

        public PurchaseOrderApp(IPurchaseOrderRepository IPurchaseOrderRepository)
        {
            _purchaseOrderRepository = IPurchaseOrderRepository;
        }
        public async Task<List<PurchaseOrder>> GetPurchaseOrderAsync(QueryParam queryParam)
        {
            var purchaseOrderList = await _purchaseOrderRepository.GetPurchaseOrder(QueryParamHandle.ParamHanle(queryParam));
            return purchaseOrderList;
        }

        public async Task<SaveResult> SavePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            return await _purchaseOrderRepository.Save(purchaseOrder);
        }

        public async Task<bool> UpdateSyncDataAsync(DocumentSync documentSyncResult)
        {
            return await _purchaseOrderRepository.UpdateSyncData(documentSyncResult);
        }
         
    }
}
