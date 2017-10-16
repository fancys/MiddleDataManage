using IntegratedManagement.Entity.BusinessPartnerModule.Branch;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/12 20:25:01
	===============================================================================================================================*/
    public interface IBranchApp
    {
        Task<List<Branch>> GetBranchList(QueryParam queryParam);
    }
}
