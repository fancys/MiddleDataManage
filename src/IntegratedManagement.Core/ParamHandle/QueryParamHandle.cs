using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Core.ParamHandle
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 13:48:01
	===============================================================================================================================*/
    public class QueryParamHandle
    {
        public static QueryParam ParamHanle(QueryParam QueryParam)
        {
            if (QueryParam == null)
                throw new Exception("the Param of Quanry is null.");
            QueryParam newParam = new QueryParam();
            if (!string.IsNullOrEmpty(QueryParam.filter))
                newParam.filter = "where " + GetQueryFilter(QueryParam.filter);//待处理问题 主表与子表含有相同字段名称如何处理？--只能查询主表字段
            if (!string.IsNullOrEmpty(QueryParam.expand))
                newParam.expand = QueryParam.expand;
            if (!string.IsNullOrEmpty(QueryParam.orderby))
                newParam.orderby = "order by t0." + QueryParam.orderby;
            if (!string.IsNullOrEmpty(QueryParam.select))
                newParam.select = QueryParam.select;
            else
                newParam.select = "*";
            if (QueryParam.limit == 0)
                newParam.limit = 50;
            else
                newParam.limit = QueryParam.limit;
            return newParam;
        }


        private static string GetQueryFilter(string Filter)
        {
            string[] filterList = Filter.Split(' ').Where(c => c != "").ToArray();
            StringBuilder sbFilter = new StringBuilder();
            for (int i = 1; i <= filterList.Count(); i++)
            {
                
                if (i == 4 * (i / 4) + 1)
                    sbFilter.Append(filterList[i - 1].Insert(filterList[i-1].LastIndexOf('(')+1,"t0."));
                else if (i == 4 * (i / 4) + 2)
                    sbFilter.Append(GetSQLCompare(filterList[i - 1]));
                else
                    sbFilter.Append(filterList[i - 1]);
            }
            //eq（Equals =）、ne（Not Equals <>）、gt（Greater Than >）、ge（Greater Than or Equal >=）、lt（Less Than <）、le（Less Than or Equal <=）、and（And）、or（Or）
            return sbFilter.ToString();
        }

        private static string GetSQLCompare(string CompareKey)
        {
            switch(CompareKey)
            {
                case "eq":return "=";
                case "ne":return "<>";
                case "gt":return ">";
                case "ge":return ">=";
                case "lt":return "<";
                case "le":return "<=";
                case "in":return " in ";
                default:
                    throw new Exception("invalid filter.");

            }
        }
    }
}
