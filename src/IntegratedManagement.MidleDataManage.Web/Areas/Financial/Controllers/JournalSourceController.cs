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
                queryParam.filter = "(IsSyncToCW eq 'Y')";
                queryParam.orderby = "TransId";
                var rt = await _JournalSourceApp.GetJournalSourceAsync(queryParam);
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

        [HttpPost]
        public async Task<ActionResult> SyncSelectedData(string IDs)
        {

            if (string.IsNullOrEmpty(IDs))
                return Json(new { state = ResultType.error.ToString(), message = "选择的数据为空" });
            QueryParam queryParam = new QueryParam();
            queryParam.filter = string.Format("TransId in {0}", IDs.TrimEnd(','));
            queryParam.orderby = "TransId";
            var rt = await _JournalSourceApp.GetJournalSourceAsync(queryParam);
            string errorNum="";
            string errorMsg = "";
            foreach (var item in rt)
            {
                try
                {
                    JournalRelationMap jrMap = new JournalRelationMap();
                    jrMap = ToRelationMap(item);
                    var syncResult = await _JournalRelationMapApp.SaveJournalRelationMapAsync(jrMap);
                    if (syncResult.Code != 0)
                    {
                        errorNum += syncResult.UniqueKey + ",";
                        errorMsg += syncResult.Message + ";";
                    }
                }
                catch(Exception ex)
                {
                    errorNum += item.TransId + ",";
                    errorMsg += ex.Message + ";";
                }
            }
            if (!string.IsNullOrEmpty(errorNum) && !string.IsNullOrEmpty(errorMsg))
                return Json(new { state = ResultType.success.ToString(), message = "" });
            else
                return Json(new { state = ResultType.error.ToString(), message = $"失败单号：{errorNum};失败原因：{errorMsg}" });
        }

        private JournalRelationMap ToRelationMap(JournalSource JRSource)
        {
            JournalRelationMap jrMap = new JournalRelationMap();
            jrMap.TransId = JRSource.TransId;
            jrMap.TransType = JRSource.TransType;
            jrMap.BPLId = JRSource.BPLId;
            jrMap.CreateDate = JRSource.CreateDate;
            jrMap.TaxDate = JRSource.TaxDate;
            jrMap.Number = JRSource.Number;
            jrMap.RefDate = JRSource.RefDate;
            jrMap.DueDate = JRSource.DueDate;
            jrMap.ERPOrderNum = JRSource.ERPOrderNum;
            jrMap.SourceTable = JRSource.SourceTable;

            return jrMap;
        }
    }
}