using Autofac;
using IntegratedManagement.IRepository.PurchaseModule;
using IntegratedManagement.RepositoryDapper.PurchaseModule;
using IntegratedManageMent.Application.PurchaseModule;
using IntegrateManagement.MiddleBaseService.Job.PurchaseModule;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Topshelf;
using Topshelf.Autofac;
using Topshelf.ServiceConfigurators;

namespace IntegrateManagement.MiddleBaseService.Service
{
    class Program
    {
        static void Main(string[] args)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

            //var builder = new ContainerBuilder();

            //builder.RegisterAssemblyTypes(Assembly.Load("IntegrateManagement.MiddleBaseService.Job")).Where(t => t.Name.EndsWith("Job"));

            //builder.RegisterType<PurchaseOrderJob>();
            
            //builder.RegisterType<PurchaseOrderApp>().As<IPurchaseOrderApp>();
            //builder.RegisterType<PurchaseOrderDapperRepository>().As<IPurchaseOrderRepository>();

            //builder.RegisterType<SampleDependency>().As<ISampleDependency>();
            //builder.RegisterType<SampleService>();

            ////注册模块的应用服务
            //builder.RegisterAssemblyTypes(Assembly.Load("IntegratedManageMent.Application"))
            //        .Where(t => t.Name.EndsWith("App"))
            //        .AsImplementedInterfaces()//标识注册的类型，以接口的方式注册
            //        .InstancePerLifetimeScope();//在一个生命周期中，每一个依赖或调用公用一个实例
            ////注册模块的仓储
            //builder.RegisterAssemblyTypes(Assembly.Load("IntegratedManagement.RepositoryDapper"))
            //         .Where(t => t.Name.EndsWith("DapperRepository"))
            //         .AsImplementedInterfaces()
            //         .InstancePerLifetimeScope();

            
            ////autofac 注册依赖
            //IContainer container = builder.Build();

            

            HostFactory.Run(x =>
                                {
                                   // x.UseAutofacContainer(container);
                                    x.RunAsLocalSystem();

                                    x.SetDescription(Configuration.ServiceDescription);
                                    x.SetDisplayName(Configuration.ServiceDisplayName);
                                    x.SetServiceName(Configuration.ServiceName);
                                    x.Service<QuartzServer>(factory =>
                                    {
                                       
                                        //factory.ConstructUsingAutofacContainer();
                                        QuartzServer server = QuartzServerFactory.CreateServer();
                                        server.Initialize();
                                        return server;
                                    });

                                });
        }
    }
}
