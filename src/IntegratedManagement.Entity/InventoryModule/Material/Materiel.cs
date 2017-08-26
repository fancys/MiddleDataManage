using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.InventoryModule.Material
{
    public class Material:ResultObject
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        /// <summary>
        /// 销售税率
        /// </summary>
        public string VatGourpSa { get; set; }
        /// <summary>
        /// 采购税率
        /// </summary>
        public string VatGourpPu { get; set; }
        /// <summary>
        /// 初始成本
        /// </summary>
        public decimal InitialCost { get; set; }
        /// <summary>
        /// 重估成本
        /// </summary>
        public decimal RealCost { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 是否库存管理
        /// </summary>
        public string InvntItem { get; set; }
        /// <summary>
        /// 是否代销销售
        /// </summary>
        public string Consignment { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public string OMSGroupNum { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Vendor { get; set; }
    }
}
