using IntegratedManagement.Entity.Param;
using IntegratedManageMent.Application.FinancialModule;
using ReportFormManage.Code.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.Areas.Financial.Controllers
{
    public class JournalSourceController : Controller
    {
        private readonly IJournalSourceApp _JournalSourceApp;
        public JournalSourceController(IJournalSourceApp IJournalSourceApp)
        {
            this._JournalSourceApp = IJournalSourceApp;
        }
        // GET: Financial/JournalSource
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SearchJournalSource(ViewParam param)
        {
            try
            {
                /*
                 * 获取查询条件
                 */
                //if(param.MinCreateDate)

                QueryParam queryParam = new QueryParam();
                queryParam.filter = "(CreateDate gt '20170101')";
                queryParam.orderby = "order by TransId";
                var rt = await _JournalSourceApp.GetSalesOrderAsync(queryParam);
                return Json(new { state = ResultType.success.ToString(), data = Newtonsoft.Json.JsonConvert.SerializeObject(rt) });
            }
            catch(Exception ex)
            {
                return Json(new { state = ResultType.error.ToString(), message = ex.Message });
            }
        }

        public async Task<ActionResult> SearchJournalSource(string filter,string limit)
        {
            try
            {
                /*
                 * 获取查询条件
                 */
                //if(param.MinCreateDate)

                QueryParam queryParam = new QueryParam();
                queryParam.filter = "(CreateDate gt '20170101')";
                queryParam.orderby = "order by TransId";
                var rt = await _JournalSourceApp.GetSalesOrderAsync(queryParam);
                return Json(new { state = ResultType.success.ToString(), data = Newtonsoft.Json.JsonConvert.SerializeObject(rt) });
            }
            catch (Exception ex)
            {
                return Json(new { state = ResultType.error.ToString(), message = ex.Message });
            }
        }
    }
}