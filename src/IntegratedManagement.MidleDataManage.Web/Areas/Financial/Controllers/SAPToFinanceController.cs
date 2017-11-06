using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.Areas.Financial.Controllers
{
    // SAP系统主数据同步到财务帐套
    public class SAPToFinanceController : Controller
    {
        // GET: Financial/SAPToFinance
        public ActionResult Index()
        {
            return View();
        }
    }
}