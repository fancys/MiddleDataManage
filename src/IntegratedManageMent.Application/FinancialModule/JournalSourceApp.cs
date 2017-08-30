using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.FinancialModule.JournalSource;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.IRepository.FinancialModule;
using IntegratedManagement.Core.ParamHandle;

namespace IntegratedManageMent.Application.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:47:23
	===============================================================================================================================*/
    public class JournalSourceApp : IJournalSourceApp
    {
        private readonly IJournalSourceRepository _JournalSourceRepository;
        public JournalSourceApp(IJournalSourceRepository IJournalSourceRepository)
        {
            this._JournalSourceRepository = IJournalSourceRepository;
        }
        public async Task<List<JournalSource>> GetSalesOrderAsync(QueryParam QueryParam)
        {
            return await _JournalSourceRepository.GetJournalSourceList(QueryParamHandle.ParamHanle(QueryParam));
        }
    }
}
