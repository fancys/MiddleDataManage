using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.InvoicesReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 15:01:51
	===============================================================================================================================*/
    public   interface IInvoicesReturnApp
    {
        Task<SaveResult> PostInvoicesReturnAsync(InvoicesReturn oInvoicesReturn);
    }
}
