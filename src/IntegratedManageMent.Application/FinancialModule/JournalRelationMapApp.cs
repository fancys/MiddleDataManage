using IntegratedManagement.Core.ParamHandle;
using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.IRepository.FinancialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:48:04
	===============================================================================================================================*/
    public class JournalRelationMapApp: IJournalRelationMapApp
    {
        private readonly IJournalRelationMapRepository _JournalRelationMapRepository;
        public JournalRelationMapApp(IJournalRelationMapRepository IJournalRelationMapRepository)
        {
            this._JournalRelationMapRepository = IJournalRelationMapRepository;
        }
        public async Task<List<JournalRelationMap>> GetJournalRelationMapListAsync(QueryParam QueryParam)
        {
            return await _JournalRelationMapRepository.GetJournalRelationMapList(QueryParamHandle.ParamHanle(QueryParam));
        }

        public async Task<SaveResult> SaveJournalRelationMapAsync(JournalRelationMap JournalRelationMap)
        {
           return  await _JournalRelationMapRepository.SaveJournalRelationMap(JournalRelationMap);
        }
    }
}
