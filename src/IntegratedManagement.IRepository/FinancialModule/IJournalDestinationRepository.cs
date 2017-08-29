using IntegratedManagement.Entity.FinancialModule.JournalDestination;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 9:29:55
	===============================================================================================================================*/
    public interface IJournalDestinationRepository
    {
        Task<List<JournalDestination>> GetJournalSourceList(QueryParam queryParam);
    }
}
