using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.BusinessPartnerModule.ProfitCenter;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.IRepository.BusinessPartnerModule;
using IntegratedManagement.Core.ParamHandle;
using IntegrateManagement.MiddleBaseService.B1;

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

        public string CreateProfitCenter(List<ProfitCenters> profitCenterss)
        {
            StringBuilder resultStr = new StringBuilder();
            foreach (var item in profitCenterss)
            {
                try
                {
                    ProfitCenterService.AddOrUpdateProfitCenter(item);
                }
                catch(Exception ex)
                {
                    resultStr.Append(ex.Message + ";");
                }
            }
            if (string.IsNullOrEmpty(resultStr.ToString()))
            {
                return "同步成功。";
            }
            return resultStr.ToString();
        }

        public async Task<List<ProfitCenters>> GetProfitCentersList(QueryParam queryParam)
        {
            return await _ProfitCentersRepository.GetProfitCenters(QueryParamHandle.ParamHanle(queryParam));
        }
    }
}
