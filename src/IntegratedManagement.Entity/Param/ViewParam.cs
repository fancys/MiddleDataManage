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
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }


        public int BPLId { get; set; }

        public string BPLName { get; set; }

        public string Creator { get; set; }

        public int TransId { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public int TransType { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public int HandleStatu { get; set; }


    }
}
