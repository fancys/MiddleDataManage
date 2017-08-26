using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.SalesReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 15:47:44
	===============================================================================================================================*/
    public interface ISalesReturnRepository
    {
        List<SalesReturn> Fetch(QueryParam Param);

        SaveResult Save(SalesReturn SalesReturn);

        SalesReturn GetSalesOrder(int DocEntry);

    }
}
