using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 15:15:07
	===============================================================================================================================*/
    public class BPAddress
    {
        //        CardCode
        //Address
        //AddrType
        //Building
        //StreetNo
        //Block
        //State
        //City
        public string CardCode { get; set; }
        public string Address { get; set; }

        /// <summary>
        /// 传输类型：
        ///  B-开票地址
        ///  S-收货地址 
        /// /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// 企业开票地址信息
        /// </summary>
        public string Building { get; set; }
        /// <summary>
        /// 企业客户编码
        /// </summary>
        public string StreetNo { get; set; }
        public string Block { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
