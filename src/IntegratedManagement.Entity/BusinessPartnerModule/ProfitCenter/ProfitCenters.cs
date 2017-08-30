using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.BusinessPartnerModule.ProfitCenter
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 15:36:19
	===============================================================================================================================*/
    public class ProfitCenters
    {
        public string PrcCode { get; set; }
        public string PrcName { get; set; }
        public int DimCode { get; set; }
        public string Active { get; set; }
        public string Type { get; set; }
        public string ERPCode { get; set; }
    }
}
