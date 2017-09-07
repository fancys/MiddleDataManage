using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.PurchaseModule.PurchaseDelivery
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/7 14:05:30
	===============================================================================================================================*/
    public class PurchaseDelivery: ResultObject
    {
        public PurchaseDelivery()
        {
            this.PurchaseDeliveryItems = new List<PurchaseDeliveryItem>();
        }
        
        public int DocEntry { get; set; }
        public int ERPDocEntry { get; set; }

        public string CardCode { get; set; }

        public string CardName { get; set; }

        public int BPLId { get; set; }

        public DateTime DocDate { get; set; }

        public string Comments { get; set; }

        public List<PurchaseDeliveryItem> PurchaseDeliveryItems { get; set; }

    }
}
