using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PaymentReceivedModule.Refund;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.PaymentReceivedModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/27 10:03:18
	===============================================================================================================================*/
    public interface IRefundRepository
    {
        Task<List<Refund>> GetRefund(QueryParam queryParam);
        Refund GetRefund(int  DocEntry);

        Task<SaveResult> Save(Refund Refund);

        Task<bool> UpdateSyncData(DocumentSync documentResult);

        Task<bool> UpdateSyncDataBatch(DocumentSync documentResult);

        //Task<bool> 
    }
}
