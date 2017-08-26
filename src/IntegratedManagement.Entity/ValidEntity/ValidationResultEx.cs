using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.ValidEntity
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/13 11:29:42
    *	model层数据验证 错误处理的扩展方法
	===============================================================================================================================*/
    public static class ValidationResultEx
    {
        public static string ForEachToString<T>(this IEnumerable<T> IEnumerable) where T: ValidationResult
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in IEnumerable)
            {
                sb.Append(item.ErrorMessage);
            }
            return sb.ToString();
        }
    }
}
