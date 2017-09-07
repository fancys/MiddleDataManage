using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseDelivery;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.PurchaseModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/7 15:54:39
	===============================================================================================================================*/
    public interface IPurchaseDeliveryApp
    {
        Task<List<PurchaseDelivery>> GetPurchaseDeliveryListAsync(QueryParam queryParam);

        Task<PurchaseDelivery> GetPurchaseDeliveryAsync(int DocEntry);


    }
}
