using IntegratedManagement.Core.ParamHandle;
using IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.IRepository.BusinessPartnerModule;
using IntegratedManagement.RepositoryDapper.BusinessPartnerModule;
using IntegrateManagement.MiddleBaseService.B1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/24 13:10:29
	===============================================================================================================================*/
    public class BusinessPartnerApp: IBusinessPartnerApp
    {
       protected readonly  IBusinessPartnerRepository _BusinessPartnerRepository;

        public BusinessPartnerApp(IBusinessPartnerRepository BusinessPartnerRepository)
        {
            _BusinessPartnerRepository = BusinessPartnerRepository;
        }
        
        public async Task<List<BusinessPartner>> GetBusinessPartnerList(QueryParam queryParam)
        {
            return await _BusinessPartnerRepository.Fetch(QueryParamHandle.ParamHanle(queryParam));
        }

        public async Task<SaveResult>  SaveBusinessPartner(BusinessPartner businessPartner)
        {
            return await _BusinessPartnerRepository.Save(businessPartner);
        }

        public async Task<bool> PatchBusinessPartner(BusinessPartner businessPartner)
        {
            return await _BusinessPartnerRepository.Update(businessPartner);
        }

        public async Task<bool> UpdateSyncData(string CardCode)
        {
            return await _BusinessPartnerRepository.UpdateSyncData(CardCode);
        }

        /// <summary>
        /// 根据传递的json数据来部分更新主数据
        /// </summary>
        /// <param name="businessPartner"></param>
        /// <returns></returns>
        public async Task<bool> PatchBusinessPartner(string businessPartner)
        {
            if (string.IsNullOrEmpty(businessPartner))
                return false;
            var businessPartnerObj = Newtonsoft.Json.JsonConvert.DeserializeObject(businessPartner);
            var properties = businessPartnerObj.GetType().GetProperties();
            BusinessPartner _businessPartner = new BusinessPartner();
            foreach(var item in properties)
            {
                if (item.Name == "CardCode")
                    _businessPartner.CardCode = item.GetValue(null).ToString();

            }
            return await _BusinessPartnerRepository.Update(_businessPartner);
        }

        public  string CreateBusinessPartner(List<BusinessPartner> businessPartners)
        {
            StringBuilder resultStr = new StringBuilder();
            foreach (var item in businessPartners)
            {
                try
                {
                    BusinessPartnerSerivice.AddOrUpdateBusinessPartner(item);
                }
                catch (Exception ex)
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

    }
}
