using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Document
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/4/7 9:19:24
    *	单据类型实体主表基类。用于中间表单据处理后回写状态
	===============================================================================================================================*/
    public class DocumentSync
    {
        public void Document()
        {
            this.SyncDate = DateTime.Now;
        }
        public string DocEntry { get; set; }

        public string SyncResult { get; set; }

        public string SyncMsg { get; set; }
        public string SAPDocEntry { get; set; }

        public DateTime SyncDate { get; set; }

    }
}
