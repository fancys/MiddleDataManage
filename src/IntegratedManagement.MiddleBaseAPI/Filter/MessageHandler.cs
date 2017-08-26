using MagicBox.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace IntegratedManagement.MiddleBaseAPI.Filter
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/7/6 15:31:23
	===============================================================================================================================*/
    public class MessageHandler: DelegatingHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 在请求到达action之前 可处理一些事情，比如 身份校验，内容校验、日志记录
            //  handle for reques
            request.Headers.Add("X-Custom-Header", "This is my custom header for request.");

            string controller = new DefaultHttpControllerSelector(GlobalConfiguration.Configuration).SelectController(request).ControllerName;
            string guid = controller + Guid.NewGuid();

            if (request.Method == HttpMethod.Post)
            {
                string requestBody = await request.Content.ReadAsStringAsync();
                Logger.Writer(guid, QueueStatus.Open, $"the body of request:\r\n{requestBody}");
            }
            var response = await base.SendAsync(request, cancellationToken);

            response.Headers.Add("X-Custom-Header", "This is my custom header for response.");

            // 在请求到达action之后，返回结果之前 可对结果进行处理
            // handle for response
            string responseText = await response.Content.ReadAsStringAsync();
            Logger.Writer(guid, QueueStatus.Close, $"the result of response:\r\n{responseText}");
            return response;
        }

    }
}