using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.BusinessPartnerModule.ProfitCenter;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.IRepository.BusinessPartnerModule;
using IntegratedManagement.Core.ParamHandle;

namespace IntegratedManageMent.Application.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/6 22:02:17
	===============================================================================================================================*/
    public class ProfitCentersApp : IProfitCentersApp
    {
        protected readonly IProfitCentersRepository _ProfitCentersRepository;
        public ProfitCentersApp(IProfitCentersRepository ProfitCentersRepository)
        {
            this._ProfitCentersRepository = ProfitCentersRepository;
        }
        public async Task<List<ProfitCenters>> GetProfitCentersList(QueryParam queryParam)
        {
            return await _ProfitCentersRepository.GetProfitCenters(QueryParamHandle.ParamHanle(queryParam));
        }
    }
}
