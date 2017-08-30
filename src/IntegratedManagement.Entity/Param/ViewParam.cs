using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Param
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 9:50:07
    *	视图查询参数
	===============================================================================================================================*/

    public class ViewParam
    {
        public DateTime MaxCreateDate { get; set; }

        public DateTime MinCreateDate { get; set; }

        public int BPLId { get; set; }

        public string BPLName { get; set; }

        public string OMSDocEntry { get; set; }

        public int TransId { get; set; }


    }
}
