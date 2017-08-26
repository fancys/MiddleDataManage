using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.SAPBOneCommon.MergeRefund
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/4/6 17:53:22
	===============================================================================================================================*/
    public class MergeRefundItem
    {
        public string ItemCode { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal ItemRefund { get; set; }
    }
}
