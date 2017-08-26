using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/24 12:58:59
	===============================================================================================================================*/
    public class BusinessPartner: ResultObject
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string PlatformCode { get; set; }
    }
}

