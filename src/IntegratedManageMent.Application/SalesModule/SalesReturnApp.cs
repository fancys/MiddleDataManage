using IntegratedManagement.IRepository.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 15:47:05
	===============================================================================================================================*/
    public class SalesReturnApp: ISalesReturnApp
    {
        private readonly ISalesReturnRepository _ISalesReturnRepository;

        public SalesReturnApp(ISalesReturnRepository ISalesReturnRepository)
        {
            _ISalesReturnRepository = ISalesReturnRepository;
        }



    }
}
