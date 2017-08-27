using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.Areas.Financial
{
    public class FinancialAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Financial";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Financial_default",
                "Financial/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "IntegratedManagement.MidleDataManage.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}