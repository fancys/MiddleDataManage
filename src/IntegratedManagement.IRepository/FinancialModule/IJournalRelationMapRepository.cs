using IntegratedManagement.Entity.FinancialModule.JournalRalationMap;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 9:30:15
	===============================================================================================================================*/
    public interface IJournalRelationMapRepository
    {
        Task<List<JournalRelationMap>> GetJournalRelationMapList(QueryParam queryParam);

        Task SaveJournalRelationMap(JournalRelationMap JournalSource);
    }
}
