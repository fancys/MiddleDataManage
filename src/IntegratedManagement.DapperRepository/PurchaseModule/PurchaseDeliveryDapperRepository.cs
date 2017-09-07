using IntegratedManagement.IRepository.PurchaseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseDelivery;
using IntegratedManagement.Entity.Result;

namespace IntegratedManagement.RepositoryDapper.PurchaseModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/7 15:57:31
	===============================================================================================================================*/
    public class PurchaseDeliveryDapperRepository : IPurchaseDeliveryRepository
    {
        public Task<PurchaseDelivery> GetPurchaseOrder(int DocEntry)
        {
            throw new NotImplementedException();
        }

        public Task<List<PurchaseDelivery>> GetPurchaseOrder(QueryParam queryParam)
        {
            throw new NotImplementedException();
        }

        public Task<SaveResult> Save(PurchaseDelivery PurchaseOrder)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSyncData(DocumentSync documentSyncResult)
        {
            throw new NotImplementedException();
        }
    }
}
