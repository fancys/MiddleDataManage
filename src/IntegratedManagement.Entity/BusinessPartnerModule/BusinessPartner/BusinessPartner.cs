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
        public BusinessPartner()
        {
            this.BPContacts = new List<BPContact>();
            this.BPAddresss = new List<BPAddress>();
        }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        /// <summary>
        /// 金税注册号
        /// </summary>
        public string GTSRegNum { get; set; }

        /// <summary>
        /// 金税开户行及账号
        /// </summary>
        public string GTSBankAct { get; set; }

        /// <summary>
        /// 金税开票地址及电话
        /// </summary>
        public string GTSBilAddr { get; set; }
        /// <summary>
        /// 信用额度
        /// </summary>
        public decimal CreditLine { get; set; }

        /// <summary>
        /// 延期付款天数
        /// </summary>
        public int DelayDays { get; set; }

        public List<BPContact> BPContacts { get; set; }

        public List<BPAddress> BPAddresss { get; set; }
    }
}

