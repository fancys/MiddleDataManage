using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManagement.IRepository.IRepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/16 14:23:32
	===============================================================================================================================*/
    public interface ISalesOrderRepository
    {
        Task<List<SalesOrder>> Fetch(QueryParam Param);

        Task<SaveResult> Save(SalesOrder SalesOrder);

        SalesOrder GetSalesOrder(int DocEntry);

        Task<bool> UpdateINSyncDataBatch(DocumentSync documentSyncData);

        Task<bool> UpdateJESyncDataBatch(DocumentSync documentSyncData);

    }
}
