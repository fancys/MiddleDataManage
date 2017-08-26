using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.SalesModule.SalesReturn
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 14:48:16
	===============================================================================================================================*/
    public class SalesReturn
    {
        public int DocEntry { get; set; }

        public DateTime DocDate { get; set; }

        public string CardCode { get; set; }

        public List<SalesReturnItem> SalesReturnItems { get; set; }

        public SalesReturn()
        {
            SalesReturnItems = new List<SalesReturnItem>();
        }
    }
}
