using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.InventoryModule.Warehouse
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 15:40:28
	===============================================================================================================================*/
    public class Warehouse
    {
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        /// <summary>
        /// 业务地点标识（分支标识）
        /// </summary>
        public int BPLid { get; set; }
        public string Inactive { get; set; }
        public string Building { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string WhsClass { get; set; }
        public string WhsType { get; set; }
        public string ContractPerson { get; set; }
        public string TelephoneNum { get; set; }
        
        
    }
}
