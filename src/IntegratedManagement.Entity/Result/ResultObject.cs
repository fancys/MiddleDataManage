using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Result
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/16 14:23:32
    *	中间表接口类，接口类主表需要继承此类
	===============================================================================================================================*/
    [JsonObject(MemberSerialization.OptOut)]
    public class ResultObject
    {
      
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        [JsonIgnore]
        public DateTime UpdateDate { get; set; }

        [JsonIgnore]
        public string Creator { get; set; }

        [JsonIgnore]
        public string UpDator { get; set; }

        [JsonIgnore]
        public string IsDelete { get; set; }

        [JsonIgnore]
        public string IsSync { get; set; }
    }
}
