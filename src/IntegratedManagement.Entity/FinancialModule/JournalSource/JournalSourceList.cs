using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.FinancialModule.JournalSource
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/10/18 17:19:12
	===============================================================================================================================*/
    public class JournalSourceList
    {
        public JournalSourceList()
        {
            JournalSources = new List<JournalSource>();
        }
        public List<JournalSource> JournalSources { get; set; }
    }
}
