using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.SAPBOneCommon
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/31 16:55:03
	===============================================================================================================================*/
    /// <summary>
    /// 处理单据生成的结果
    /// </summary>
    public class Result
    {
        public int ResultCode { get; set; }
        public string ObjCode { get; set; }

        public string Message { get; set; }
    }
}
