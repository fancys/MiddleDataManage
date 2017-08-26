using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PaymentReceivedModule.Refund;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.PaymentReceivedModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 14:45:25
	===============================================================================================================================*/
    public interface IRefundApp
    {
         Task<SaveResult> PostRefundAsync(Refund refund);

         Task<List<Refund>> GetRefundListAsync(QueryParam queryParam);

         Task<bool> UpdateSyncDataAsync(DocumentSync documentSyncResult);

         Task<bool> UpdateSyncDataBatchAsync(DocumentSync documentSyncResult);
    }
}
