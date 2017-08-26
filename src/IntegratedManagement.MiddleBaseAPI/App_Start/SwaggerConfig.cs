using System.Web.Http;
using WebActivatorEx;
using IntegratedManagement.MiddleBaseAPI;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace IntegratedManagement.MiddleBaseAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "IntegratedManagement.MiddleBaseAPI");
                        string filePath = string.Format("{0}/bin/IntegratedManagement.MiddleBaseAPI.XML", System.AppDomain.CurrentDomain.BaseDirectory);
                        c.IncludeXmlComments(filePath);
                        
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
