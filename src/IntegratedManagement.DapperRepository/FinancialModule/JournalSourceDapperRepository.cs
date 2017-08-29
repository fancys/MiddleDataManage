using IntegratedManagement.IRepository.FinancialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.FinancialModule.JournalSource;
using IntegratedManagement.Entity.Param;

namespace IntegratedManagement.RepositoryDapper.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:46:21
	===============================================================================================================================*/
    public class JournalSourceDapperRepository : IJournalSourceRepository
    {
        public Task<List<JournalSource>> GetJournalSourceList(QueryParam queryParam)
        {
            throw new NotImplementedException();
        }
    }
}
