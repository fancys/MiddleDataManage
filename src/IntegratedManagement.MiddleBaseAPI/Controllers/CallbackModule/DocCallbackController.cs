using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.CallbackModule
{
    [AllowAnonymous]
    [Authorize]
    public class DocCallbackController : ApiController
    {
        public Task<IHttpActionResult> Callback([FromBody]List<CallbackParam> CallbackParamList)
        {
           
            if (CallbackParamList == null)
            {
                //result.Code = 10001;
                //result.Message = "请求的内容格式不正确";
            }
            else
            {
                //foreach (var item in salesOrderList)
                //{
                //    var rt = await salesOrderApp.PostSalesOrder(item);
                //    result.ResultObject.Add(rt);
                //}
            }

            return null;
        }
    }
}
