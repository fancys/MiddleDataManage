using IntegratedManagement.Core.ParamHandle;
using IntegratedManagement.Entity.BusinessPartnerModule.Branch;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.IRepository.BusinessPartnerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/12 20:25:31
	===============================================================================================================================*/
    public class BranchApp: IBranchApp
    {
        protected readonly IBranchRepository _BranchRepository;

        public BranchApp(IBranchRepository BranchRepository)
        {
            _BranchRepository = BranchRepository;
        }

        public async Task<List<Branch>> GetBranchList(QueryParam queryParam)
        {
            return await _BranchRepository.Fetch(QueryParamHandle.ParamHanle(queryParam));
        }

    }
}
