using IntegratedManagement.Entity.BusinessPartnerModule.ProfitCenter;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/6 23:19:34
	===============================================================================================================================*/
    public interface IProfitCentersRepository
    {
        Task<List<ProfitCenters>> GetProfitCenters(QueryParam queryParam);
    }
}
