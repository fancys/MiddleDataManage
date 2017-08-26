using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using IntegratedManagement.IRepository.BusinessPartnerModule;
using IntegratedManagement.MiddleBaseAPI.Controllers.BusinessPartnerModule;
using IntegratedManagement.RepositoryDapper.BusinessPartnerModule;
using IntegratedManageMent.Application.BusinessPartnerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace IntegratedManagement.MiddleBaseAPI.App_Start
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/15 9:24:22
	===============================================================================================================================*/
    public class IocConfig
    {
        public static void RegisterDependencies(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
           
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //注册模块的应用服务
            builder.RegisterAssemblyTypes(Assembly.Load("IntegratedManageMent.Application"))
                    .Where(t => t.Name.EndsWith("App"))
                    .AsImplementedInterfaces()//表示注册的类型，以接口的方式注册
                    .InstancePerLifetimeScope();//在一个生命周期中，每一个依赖或调用公用一个实例
            //注册模块的仓储
            builder.RegisterAssemblyTypes(Assembly.Load("IntegratedManagement.RepositoryDapper"))
                     .Where(t => t.Name.EndsWith("DapperRepository"))
                     .AsImplementedInterfaces()
                     .InstancePerLifetimeScope();

            builder.RegisterWebApiFilterProvider(config);
            //autofac 注册依赖
            IContainer container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        
    }
}