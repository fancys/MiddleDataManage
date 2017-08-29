using IntegratedManagement.IRepository.FinancialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.FinancialModule.JournalDestination;
using IntegratedManagement.Entity.Param;

namespace IntegratedManagement.RepositoryDapper.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:45:01
	===============================================================================================================================*/
    public class JournalDestinationDapperRepository : IJournalDestinationRepository
    {
        public async Task<List<JournalDestination>> GetJournalSourceList(QueryParam queryParam)
        {
            throw new NotImplementedException();
        }
    }
}
