using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
[assembly: OwinStartup(typeof(IntegratedManagement.MiddleBaseAPI.Startup))]
namespace IntegratedManagement.MiddleBaseAPI
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/23 10:16:50
	===============================================================================================================================*/
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}