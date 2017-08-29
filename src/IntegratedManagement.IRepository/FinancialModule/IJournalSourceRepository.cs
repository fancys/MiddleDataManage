using IntegratedManagement.Entity.FinancialModule.JournalSource;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 9:30:34
	===============================================================================================================================*/
    public interface IJournalSourceRepository
    {
        Task<List<JournalSource>> GetJournalSourceList(QueryParam queryParam);
    }
}
