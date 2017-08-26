using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.ValidEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.SalesModule.SalesOrder
{
    /*===============================================================================================================================
		Create by Fancy at 2017/3/16 14:23:32
	===============================================================================================================================*/
    [JsonObject(MemberSerialization.OptOut)]
    public class SalesOrder:ResultObject
    {
       [JsonIgnore]
        public int DocEntry { get; set; }

        [Required]
        public int OMSDocEntry { get; set; }
        [Required]
        public DateTime OMSDocDate { get; set; }

        [Required]
        public string DocType { get; set; }
        [Required]
        public string BusinessType { get; set; }
        [Required]
        public string PlatformCode { get; set; }
        [Required]
        public string CardCode { get; set; }
        
        /// <summary>
        /// 物流公司
        /// </summary>
        public string FrghtVendor { get; set; }

        /// <summary>
        /// 商品实付金额
        /// </summary>
        public decimal OrderPaied { get; set; }

        public decimal Freight { get; set; }

        public string PayMethod { get; set; }

        public string Comments { get; set; }

        [JsonIgnore]
        public string IsJESync { get; set; }

        [JsonIgnore]
        public string JESAPDocEntry { get; set; }

        [JsonIgnore]
        public string IsINSync { get; set; }

      
        [JsonIgnore]
        public string INSAPDocEntry { get; set; }

        [Required]
        public List<SalesOrderItem> SalesOrderItems { get; set; }



        public SalesOrder()
        {
            SalesOrderItems = new List<SalesOrderItem>();
        }
    }
}
