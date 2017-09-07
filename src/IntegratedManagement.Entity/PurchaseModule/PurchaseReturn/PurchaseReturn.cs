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

        public string DocStatus { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public DateTime DocDate { get; set; }
        public List<PurchaseReturnItem> PurchaseReturnItems { get; set; }

    }
}
