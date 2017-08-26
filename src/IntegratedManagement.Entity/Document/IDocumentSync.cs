using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Document
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/7/10 9:43:32
	===============================================================================================================================*/
    public interface IDocumentSync
    {

         string DocEntry { get; set; }

         string SyncResult { get; set; }

         string SyncMsg { get; set; }

        [JsonIgnore]
        string SAPDocEntry { get; set; }

         DateTime SyncDate { get; set; }

    }
}
