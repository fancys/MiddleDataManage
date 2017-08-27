using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.App_Start
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/27 17:48:32
	===============================================================================================================================*/
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
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

            
            //autofac 注册依赖
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}