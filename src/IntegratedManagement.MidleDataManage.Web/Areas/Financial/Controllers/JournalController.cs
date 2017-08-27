using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.Areas.Financial.Controllers
{
    public class JournalController : Controller
    {
        // GET: Financial/Journal
        public ActionResult Index()
        {
            return View();
        }
    }
}