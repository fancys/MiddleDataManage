using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.SAPBOneCommon.MergeSalesOrder
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/4/7 17:31:04
	===============================================================================================================================*/
    public class MergeSalesOrderItem
    {
        public string ItemCode { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal ItemPaied { get; set; }
    }
}
