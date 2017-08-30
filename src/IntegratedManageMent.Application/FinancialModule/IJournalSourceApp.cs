using IntegratedManagement.Entity.FinancialModule.JournalSource;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 9:13:54
	===============================================================================================================================*/
    public interface IJournalSourceApp
    {
        Task<List<JournalSource>> GetSalesOrderAsync(QueryParam QueryParam);
    }
}
