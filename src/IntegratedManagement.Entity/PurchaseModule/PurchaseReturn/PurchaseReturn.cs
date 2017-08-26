using IntegratedManagement.Entity.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntegratedManagement.Entity.PurchaseModule.PurchaseReturn
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PurchaseReturn : ResultObject
    {
        public PurchaseReturn()
        {
            PurchaseReturnItems = new List<PurchaseReturnItem>();
        }
        [JsonIgnore]
        public int DocEntry { get; set; }
        
        public int OMSDocEntry { get; set; }
        public DateTime OMSDocDate { get; set; }
        public string DocType { get; set; }
        public string BusinessType { get; set; }
        public string CardCode { get; set;}
        public string Comments { get; set; }

        public string BatchNum { get; set; }
        public List<PurchaseReturnItem> PurchaseReturnItems { get; set; }

    }
}
