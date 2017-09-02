using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.FinancialModule.JournalSource;
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
        private readonly IJournalRelationMapApp _JournalRelationMapApp;
        public JournalSourceController(IJournalSourceApp IJournalSourceApp, IJournalRelationMapApp IJournalRelationMapApp)
        {
            this._JournalSourceApp = IJournalSourceApp;
            this._JournalRelationMapApp = IJournalRelationMapApp;
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
                queryParam.orderby = "TransId";
                var rt = await _JournalSourceApp.GetSalesOrderAsync(queryParam);
                return Json(new { state = ResultType.success.ToString(), data = Newtonsoft.Json.JsonConvert.SerializeObject(rt) });
            }
            catch(Exception ex)
            {
                return Json(new { state = ResultType.error.ToString(), message = ex.Message });
            }
        }

        

        [HttpPost]
        public async Task<ActionResult> SyncAllData(JournalRelationMapList journalRelationMapList)
        {
           
            string errorData = "";
            foreach (var item in journalRelationMapList.JournalRelationMaps)
            {
                try
                {
                    await _JournalRelationMapApp.SaveJournalRelationMapAsync(item);
                }
                catch (Exception ex)
                {
                    errorData = item.TransId + ",";
                }

            }
            if (!string.IsNullOrEmpty(errorData))
                return Json(new { state = ResultType.error.ToString(), message = "单据：" + errorData.TrimEnd(',') + "同步失败" });
            else
                return Json(new { state = ResultType.success.ToString() });

        }

    }
}