using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.SAPBOneCommon.MergeSalesOrder
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/4/7 17:07:57
	===============================================================================================================================*/
    public class MergeSalesOrder
    {
        public DateTime OMSDocDate { get; set; }

        public int DocEntry { get; set; }
        public string DocType { get; set; }

        public string BusinessType { get; set; }

        public string PlatformCode { get; set; }

        public string CardCode { get; set; }
        

        public decimal OrderPaied { get; set; }

        public decimal Freight { get; set; }

        public string PayMethod { get; set; }

        public List<MergeSalesOrderItem> MergeSalesOrderItems { get; set; }

        public List<OriginSalesOrder> SalesOrders { get; set; }
        public MergeSalesOrder()
        {
            this.SalesOrders = new List<OriginSalesOrder>();
            this.MergeSalesOrderItems = new List<MergeSalesOrderItem>();
        }
    }
}
