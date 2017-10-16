using IntegratedManagement.Entity.BusinessPartnerModule.Branch;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/12 20:19:35
	===============================================================================================================================*/
    public interface IBranchRepository
    {
        Task<List<Branch>> Fetch(QueryParam queryParam);
    }
}
