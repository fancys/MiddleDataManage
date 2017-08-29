using IntegratedManagement.IRepository.FinancialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.FinancialModule.JournalRalationMap;
using IntegratedManagement.Entity.Param;

namespace IntegratedManagement.RepositoryDapper.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:45:54
	===============================================================================================================================*/
    public class JournalRelationMapDapperRepository : IJournalRelationMapRepository
    {
        public Task<List<JournalRelationMap>> GetJournalRelationMapList(QueryParam queryParam)
        {
            throw new NotImplementedException();
        }

        public Task SaveJournalRelationMap(JournalRelationMap JournalSource)
        {
            throw new NotImplementedException();
        }
    }
}
