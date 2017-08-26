using IntegratedManagement.Entity.Document;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.PaymentReceivedModule.Refund
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 15:20:13
	===============================================================================================================================*/
    [JsonObject(MemberSerialization.OptOut)]
    public class RefundItem: IDocumentItemBase
    {
        [JsonIgnore]
        public int DocEntry { get; set; }
        [JsonIgnore]
        public int LineNum { get; set; }

        public int OMSDocEntry { get; set; }

        public int OMSLineNum { get; set; }

        public string ItemCode { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal ItemRefund { get; set; }


    }
}
