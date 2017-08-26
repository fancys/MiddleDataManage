using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseOrder;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.PurchaseModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 14:51:49
	===============================================================================================================================*/
    public interface IPurchaseOrderApp
    {
        Task<List<PurchaseOrder>> GetPurchaseOrderAsync(QueryParam queryParam);

        Task<SaveResult> SavePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task<bool> UpdateSyncDataAsync(DocumentSync documentSyncResult);
    }
}
