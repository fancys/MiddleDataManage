using IntegratedManagement.Entity.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.PaymentReceivedModule.Refund
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 15:18:17
	===============================================================================================================================*/
    [JsonObject(MemberSerialization.OptOut)]
    public class Refund:ResultObject
    {
        public Refund()
        {
            RefundItems = new List<RefundItem>();
        }
        [JsonIgnore]
        public int DocEntry { get; set; }

        public int OMSDocEntry { get; set; }


        public DateTime OMSDocDate { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string DocType { get; set; }

        /// <summary>
        /// 平台编号
        /// </summary>
        public string PlatformCode { get; set; }

        /// <summary>
        /// 店铺编码
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }

        /// <summary>
        /// 退款方式
        /// </summary>
        public string RefundType { get; set; }

        /// <summary>
        /// 退款原因
        /// </summary>
        public string RefundReason { get; set; }

        /// <summary>
        /// 退款总金额
        /// </summary>
        public decimal GrossRefund { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal Freight { get; set; }

        /// <summary>
        /// 物料公司
        /// </summary>
        public string FrghtVendor { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayMethod { get; set; }

        public string Comments { get; set; }

        public string SAPDocEntry { get; set; }

        public List<RefundItem> RefundItems { get; set; }
    }
}
