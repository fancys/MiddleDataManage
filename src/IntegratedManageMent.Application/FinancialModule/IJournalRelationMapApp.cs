using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:47:51
	===============================================================================================================================*/
    public interface IJournalRelationMapApp
    {
        Task<List<JournalRelationMap>> GetJournalRelationMapListAsync(QueryParam QueryParam);

        Task<SaveResult> SaveJournalRelationMapAsync(JournalRelationMap JournalRelationMap);

        Task<bool> UpdateJournalRelationMapPositiveStatuAsync(DocumentSync SyncData);
        Task<bool> UpdateJournalRelationMapMinusStatuAsync(DocumentSync SyncData);
        Task<bool> UpdateJournalRelationMapStatuAsync(DocumentSync SyncData);
    }
}
