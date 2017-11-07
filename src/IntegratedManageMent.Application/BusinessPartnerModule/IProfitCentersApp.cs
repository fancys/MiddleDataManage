using IntegratedManagement.Entity.BusinessPartnerModule.ProfitCenter;
using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/6 22:02:39
	===============================================================================================================================*/
    public interface IProfitCentersApp
    {
        Task<List<ProfitCenters>> GetProfitCentersList(QueryParam queryParam);

        string CreateProfitCenter(List<ProfitCenters> profitCenterss);
    }
}
