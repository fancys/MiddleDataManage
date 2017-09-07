using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.PurchaseModule.PurchaseOrder
{
    [JsonObject(MemberSerialization.OptOut)]
    public class PurchaseOrder:ResultObject
    {
        public PurchaseOrder()
        {
            PurchaseOrderItems = new List<PurchaseOrderItem>();
        }
        [JsonIgnore]
        public int DocEntry { get; set; }
        
        public string DocStatus { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public DateTime DocDate { get; set; }

        [Required]
        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
