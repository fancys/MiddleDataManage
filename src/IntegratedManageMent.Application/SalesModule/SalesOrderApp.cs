using IntegratedManagement.Core.ParamHandle;
using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManagement.IRepository.SalesModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/16 14:23:32
	===============================================================================================================================*/
    public class SalesOrderApp:ISalesOrderApp
    {
        private readonly ISalesOrderRepository salesOrderRepository ;

        public SalesOrderApp(ISalesOrderRepository ISalesOrderRepository)
        {
            salesOrderRepository = ISalesOrderRepository;
        }
        public async Task<List<SalesOrder>> GetSalesOrderAsync(QueryParam QueryParam)
        {
            return await salesOrderRepository.Fetch(QueryParamHandle.ParamHanle(QueryParam));
        }

        public async Task<SaveResult> PostSalesOrderAsync(SalesOrder order)
        {
            order.IsDelete = "N";
            order.IsSync = "N";
            return await salesOrderRepository.Save(order);
        }

        public async Task<bool> UpdateINSyncDataBatchAsync(DocumentSync documentsSyncResutl)
        {
            return await salesOrderRepository.UpdateINSyncDataBatch(documentsSyncResutl);
        }

        public async Task<bool> UpdateJESyncDataBatchAsync(DocumentSync documentsSyncResutl)
        {
            return await salesOrderRepository.UpdateJESyncDataBatch(documentsSyncResutl);
        }


    }
}
