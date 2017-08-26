using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Param
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 13:23:01
    *	查询参数实体类
	===============================================================================================================================*/
    public class QueryParam
    {
        /// <summary>
        /// 过滤条件 eq（Equals）、ne（Not Equals）、gt（Greater Than）、ge（Greater Than or Equal）、lt（Less Than）、le（Less Than or Equal）、and（And）、or（Or）
        /// </summary>
        public string filter { get; set; }
        /// <summary>
        /// 明细扩展
        /// </summary>
        public string expand { get; set; }
        /// <summary>
        /// 查询字段
        /// </summary>
        public string select { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string orderby { get; set; }
        /// <summary>
        /// 查询记录限制
        /// </summary>
        public int limit { get; set; }
        
   
    }
}
