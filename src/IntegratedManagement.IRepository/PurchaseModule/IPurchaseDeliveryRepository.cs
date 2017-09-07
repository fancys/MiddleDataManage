using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseDelivery;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.PurchaseModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/7 15:56:11
	===============================================================================================================================*/
    public interface IPurchaseDeliveryRepository
    {
        Task<List<PurchaseDelivery>> GetPurchaseDeliveryList (QueryParam queryParam);
        Task<PurchaseDelivery> GetPurchaseDelivery(int DocEntry);

        Task<SaveResult> Save(PurchaseDelivery PurchaseOrder);

        Task<bool> UpdateSyncData(DocumentSync documentSyncResult);
    }
}
