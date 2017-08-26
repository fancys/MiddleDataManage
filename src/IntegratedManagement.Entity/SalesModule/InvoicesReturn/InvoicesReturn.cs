using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.SalesModule.InvoicesReturn
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/21 11:56:15
	===============================================================================================================================*/
    public class InvoicesReturn : ResultObject
    {
        /// <summary>
        /// 发票号码
        /// </summary>
        public string ZFPHM { get; set; }

        /// <summary>
        /// 发票代码
        /// </summary>
        public string ZFPDM { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime ZKPRQ { get; set; }

        /// <summary>
        /// 发票状态标志
        /// </summary>
        public string ZFPZT { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        public decimal ZFPJE { get; set; }

        /// <summary>
        /// 上端系统订单号
        /// </summary>
        public string ZSKPH { get; set; }
    }
}
