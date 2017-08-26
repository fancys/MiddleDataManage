using IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/24 12:01:46
	===============================================================================================================================*/
    public interface IBusinessPartnerRepository
    {
        Task<List<BusinessPartner>> Fetch(QueryParam queryParam);
        Task<SaveResult> Save(BusinessPartner BusinessPartner);

        Task<bool> Update(BusinessPartner businessPartner);

        Task<bool> UpdateSyncData( string CardCode);
    }
}
