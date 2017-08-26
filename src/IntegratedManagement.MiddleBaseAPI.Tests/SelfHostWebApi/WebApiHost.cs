using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace IntegratedManagement.MiddleBaseAPI.Tests.SelfHostWebApi
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/7/14 14:43:13
	===============================================================================================================================*/
    public class WebApiHost
    {
        private static   HttpSelfHostConfiguration configuration;
        public static readonly string baseHttpAddr = "http://localhost:57397/";

        private WebApiHost() { }

        public static HttpSelfHostConfiguration HostWebAPI()
        {
            if (configuration == null)
            {
                Assembly.Load("IntegratedManagement.MiddleBaseAPI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                configuration = new HttpSelfHostConfiguration(baseHttpAddr);
                using (HttpSelfHostServer httpServer = new HttpSelfHostServer(configuration))
                {
                    httpServer.Configuration.Routes.MapHttpRoute(
                          name: "DefaultApi",
                       routeTemplate: "api/{controller}/{id}",
                         defaults: new { id = RouteParameter.Optional });

                    httpServer.OpenAsync();
                }
            }
            return configuration;
        }
    }
}
