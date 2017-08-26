using IntegratedManagement.Core.ParamHandle;
using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PaymentReceivedModule.PaymentReceived;
using IntegratedManagement.Entity.PaymentReceivedModule.Refund;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.IRepository.PaymentReceivedModule;
using IntegratedManagement.RepositoryDapper.PaymentReceivedModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.PaymentReceivedModule
{
    public class RefundApp: IRefundApp
    {
        private readonly IRefundRepository _refundRepository;

        public RefundApp(IRefundRepository IRefundRepository)
        {
            _refundRepository = IRefundRepository;
        }

        public async Task<SaveResult> PostRefundAsync(Refund refund)
        {
            return await _refundRepository.Save(refund);
        }

        public async Task<List<Refund>> GetRefundListAsync(QueryParam queryParam)
        {
            return await _refundRepository.GetRefund(QueryParamHandle.ParamHanle(queryParam));
        }

        public async Task<bool> UpdateSyncDataAsync(DocumentSync documentSyncResult)
        {
            return await _refundRepository.UpdateSyncData(documentSyncResult);
        }

        public async Task<bool> UpdateSyncDataBatchAsync(DocumentSync documentSyncResult)
        {
            return await _refundRepository.UpdateSyncDataBatch(documentSyncResult);
        }
    }
}
