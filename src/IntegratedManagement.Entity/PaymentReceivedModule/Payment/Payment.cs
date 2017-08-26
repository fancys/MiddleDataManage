using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.PaymentReceivedModule.PaymentReceived
{
    public class Payment : ResultObject
    {
        /// <summary>
        /// 付款单
        /// </summary>
        public Payment()
        {
            PaymentItems = new List<PaymentItem>();
        }
        public int DocEntry { get; set; }
        public List<PaymentItem> PaymentItems { get; set; }
    }
}
