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
    public class PurchaseOrderItem: IDocumentItemBase
    {
        [JsonIgnore]
        public int DocEntry { get; set; }
        [JsonIgnore]
        public int LineNum { get; set; }
       
        public string ItemCode { get; set; }
        
        public string Dscription { get; set; }
        public decimal Quantity { get; set; }
        [Required]
        public string WhsCode { get; set; }
        
    }
}
