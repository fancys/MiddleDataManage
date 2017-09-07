using IntegratedManagement.Entity.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.PurchaseModule.PurchaseDelivery
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/7 14:05:48
	===============================================================================================================================*/
    public class PurchaseDeliveryItem : IDocumentItemBase
    {
        public int DocEntry { get; set; }

        public int LineNum { get; set; }

        public int ERPDocEntry { get; set; }

        public int ERPLineNum { get; set; }

        public int BaseEntry { get; set; }

        public int BaseLine { get; set; }

        public string ItemCode { get; set; }

        public string WhsCode { get; set; }

        public decimal Quantity { get; set; }
    }
}
