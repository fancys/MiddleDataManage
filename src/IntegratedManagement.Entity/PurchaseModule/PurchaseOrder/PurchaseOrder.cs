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
        [Required]
        public int OMSDocEntry { get; set; }
        [Required]
        public DateTime OMSDocDate { get; set; }
        public string Comments { get; set; }
        [Required]
        public string BusinessType { get; set; }
        [Required]
        public string CardCode { get; set; }
        [Required]
        public string DocType { get; set; }
        public string BatchNum { get; set; }

        [Required]
        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
