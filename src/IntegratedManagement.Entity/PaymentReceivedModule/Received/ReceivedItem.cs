using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.PaymentReceivedModule.Received
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 15:14:26
	===============================================================================================================================*/
    public class ReceivedItem: IDocumentItemBase
    {
        public int DocEntry { get; set; }

        public int LineNum { get; set; }


    }
}
