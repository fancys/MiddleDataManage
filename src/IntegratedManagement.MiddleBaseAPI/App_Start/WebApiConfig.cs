using IntegratedManagement.MiddleBaseAPI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI
{
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            
            // Web API 配置和服务
            
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //如果启用MessageHandler,swagger 路由无法索引到。。。这里还需要优化处理，或者上线后将注释去掉再发布
            //config.MessageHandlers.Add(new MessageHandler());
           

        }
    }
}
