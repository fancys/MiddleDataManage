using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.InvoicesReturn;
using IntegratedManagement.IRepository.SalesModule;
using IntegratedManagement.RepositoryDapper.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 12:15:12
	===============================================================================================================================*/
    public class InvoicesReturnApp: IInvoicesReturnApp
    {
        private readonly  IInvoicesReturnRepository InvoicesReturnRepository;

        public InvoicesReturnApp(IInvoicesReturnRepository IInvoicesReturnRepository)
        {
            InvoicesReturnRepository = IInvoicesReturnRepository;
        }
        public async Task<SaveResult> PostInvoicesReturnAsync(InvoicesReturn oInvoicesReturn)
        {
            return await InvoicesReturnRepository.Save(oInvoicesReturn);
        }
    }
}
