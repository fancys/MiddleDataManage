using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
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

        Task<SaveResult> SaveJournalRelationMap(JournalRelationMap JournalSource);

        Task<bool> ModifyJournalRelationMapMinus(DocumentSync documentSyncData);

        Task<bool> ModifyJournalRelationMapPositive(DocumentSync documentSyncData);

        Task<bool> ModifyJournalRelationMapStatus(DocumentSync documentSyncData);

    }
}
