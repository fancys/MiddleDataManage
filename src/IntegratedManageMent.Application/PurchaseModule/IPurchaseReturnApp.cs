using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseReturn;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.PurchaseModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 14:52:02
	===============================================================================================================================*/
    public interface IPurchaseReturnApp
    {
       Task<List<PurchaseReturn>> GetPurchaseReturnAsync(QueryParam queryParam);

       Task<SaveResult> SavePurchaseReturnAsync(PurchaseReturn purchaseReturn);
       Task<bool> UpdateSyncDataAsync(DocumentSync documentSyncResult);
    }
}
