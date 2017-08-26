using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Param
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/29 16:53:24
	===============================================================================================================================*/
    /// <summary>
    /// 回调接口 用于更新中间表已查询（已处理的单据）
    /// </summary>
    public class CallbackParam
    {
        /// <summary>
        /// 对象类型
        /// </summary>
        public int ObjType { get; set; }

        /// <summary>
        /// 回调时间
        /// </summary>
        public DateTime CallbackDate { get; set; }

        /// <summary>
        /// 是否处理成功
        /// </summary>
        public string IsHandled { get; set; }
    }
}
