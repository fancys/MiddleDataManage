using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseOrder;
using IntegratedManagement.Entity.PurchaseModule.PurchaseReturn;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.PurchaseModule
{
    public interface IPurchaseReturnRepository
    {
        Task<List<PurchaseReturn>> GetPurchaseReturn(QueryParam queryParam);
        PurchaseReturn GetPurchaseReturn(int DocEntry);

        Task<SaveResult> Save(PurchaseReturn PurchaseReturn);

        Task<bool> UpdateSyncData(DocumentSync documentSyncResult);
    }
}
