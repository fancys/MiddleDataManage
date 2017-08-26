using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Result
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/16 14:23:32
    *	接口返回类型描述
	===============================================================================================================================*/
    public class Result<T> where T : ResultObject
    {
        public Result()
        {
            this.Code = 0;
            this.Message = "successful operation.";
            ResultObject = new List<T>();
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public List<T> ResultObject { get; set; }
    }
}
