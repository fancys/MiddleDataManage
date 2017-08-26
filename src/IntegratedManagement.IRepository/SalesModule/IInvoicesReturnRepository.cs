using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.InvoicesReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 11:57:10
	===============================================================================================================================*/
    public interface IInvoicesReturnRepository
    {
        List<InvoicesReturn> Fetch(QueryParam Param);

        Task<SaveResult> Save(InvoicesReturn InvoicesReturn);

        InvoicesReturn GetInvoicesReturn(int DocEntry);

        Task<bool> UpdateINSyncDataBatch(DocumentSync documentSyncData);

        Task<bool> UpdateJESyncDataBatch(DocumentSync documentSyncData);
    }
}
