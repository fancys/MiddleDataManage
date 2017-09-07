using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseDelivery;
using IntegratedManagement.IRepository.PurchaseModule;
using IntegratedManagement.Core.ParamHandle;

namespace IntegratedManageMent.Application.PurchaseModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/7 15:55:11
	===============================================================================================================================*/
    public class PurchaseDeliveryApp : IPurchaseDeliveryApp
    {
        IPurchaseDeliveryRepository purchaseDeliveryRepository;
        public PurchaseDeliveryApp(IPurchaseDeliveryRepository IPurchaseDeliveryRepository)
        {
            this.purchaseDeliveryRepository = IPurchaseDeliveryRepository;
        }
        public async Task<PurchaseDelivery> GetPurchaseDeliveryAsync(int DocEntry)
        {
            return await purchaseDeliveryRepository.GetPurchaseDelivery(DocEntry);
        }

        public async Task<List<PurchaseDelivery>> GetPurchaseDeliveryListAsync(QueryParam queryParam)
        {
            return await purchaseDeliveryRepository.GetPurchaseDeliveryList(QueryParamHandle.ParamHanle(queryParam));
        }
    }
}
