using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.FinancialModule.JournalRelationMap
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/3 1:38:52
	===============================================================================================================================*/
    public class JournalRelationMapList
    {
        public JournalRelationMapList()
        {
            JournalRelationMaps = new List<JournalRelationMap>();
        }
        public List<JournalRelationMap> JournalRelationMaps { get; set; }
    }
}
