using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Result
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 15:34:38
    *	接口返回类型详细描述
	===============================================================================================================================*/
    public class SaveResult:ResultObject
    {
        ///保存或更新实体对象的结果

        public int Code { get; set; }

        /// <summary>
        /// 保存或更新实体对象的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 推送的主键
        /// </summary>
        public string UniqueKey { get; set; }


        /// <summary>
        /// 保存到中间库中的主键
        /// </summary>
        public string ReturnUniqueKey { get; set; }
    }
}
