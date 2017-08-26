using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.SalesModule.SalesReturn
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 14:48:57
	===============================================================================================================================*/
    public class SalesReturnItem: IDocumentItemBase
    {
        public  int DocEntry { get; set; }

        public int LineNum { get; set; }

        public string ItemCode { get; set; }

        public string WhsCode { get; set; }

        public decimal Quantity { get; set; }


    }
}
