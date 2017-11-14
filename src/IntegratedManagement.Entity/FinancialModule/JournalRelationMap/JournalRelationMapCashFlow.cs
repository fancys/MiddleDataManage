using IntegratedManagement.Entity.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.FinancialModule.JournalRelationMap
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/14 21:05:58
	===============================================================================================================================*/
    public class JournalRelationMapCashFlow : IDocumentItemBase
    {
        public int DocEntry
        {
            get; set;
        }

        public int LineNum
        {
            get; set;
        }

        public int CFTId { get; set; }

        public int CFWId { get; set; }

        public string AccountCode { get; set; }
        public string CFWName { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

    }
}
