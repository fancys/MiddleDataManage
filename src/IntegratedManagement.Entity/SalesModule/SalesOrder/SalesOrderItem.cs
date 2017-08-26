using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Result;
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
	*	Create by Fancy at 2017/3/16 14:23:32
	===============================================================================================================================*/
    [JsonObject(MemberSerialization.OptOut)]
    public class SalesOrderItem: IDocumentItemBase
    {
        [JsonIgnore]
        public int DocEntry { get; set; }

        [JsonIgnore]
        public int LineNum { get; set; }

        [Required]
        public int OMSDocEntry { get; set; }
        [Required]
        public int OMSLineNum { get; set; }
        [Required]
        public string ItemCode { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 商品实付金额
        /// </summary>
        public decimal ItemPaied { get; set; }

    }
}
