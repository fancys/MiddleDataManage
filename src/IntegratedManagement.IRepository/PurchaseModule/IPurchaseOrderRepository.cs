using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseOrder;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.PurchaseModule
{
    public interface IPurchaseOrderRepository
    {
        Task<List<PurchaseOrder>> GetPurchaseOrder(QueryParam queryParam);
        PurchaseOrder GetPurchaseOrder(int DocEntry);

        Task<SaveResult> Save(PurchaseOrder PurchaseOrder);

        Task<bool> UpdateSyncData(DocumentSync documentSyncResult);
    }
}
