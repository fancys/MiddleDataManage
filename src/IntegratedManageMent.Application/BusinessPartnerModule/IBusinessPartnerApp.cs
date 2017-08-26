using IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/15 11:46:28
	===============================================================================================================================*/
    public interface IBusinessPartnerApp
    {
        Task<List<BusinessPartner>> GetBusinessPartnerList(QueryParam queryParam);

        Task<SaveResult> SaveBusinessPartner(BusinessPartner businessPartner);

        Task<bool> PatchBusinessPartner(BusinessPartner businessPartner);

        Task<bool> UpdateSyncData(string CardCode);

        Task<bool> PatchBusinessPartner(string businessPartner);
    }
}
