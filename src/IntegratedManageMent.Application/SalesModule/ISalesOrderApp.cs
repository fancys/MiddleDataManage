using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 14:59:24
	===============================================================================================================================*/
    public interface ISalesOrderApp
    {
        Task<List<SalesOrder>> GetSalesOrderAsync(QueryParam QueryParam);

        Task<SaveResult> PostSalesOrderAsync(SalesOrder order);

        Task<bool> UpdateINSyncDataBatchAsync(DocumentSync documentsSyncResutl);

        Task<bool> UpdateJESyncDataBatchAsync(DocumentSync documentsSyncResutl);
    }
}
