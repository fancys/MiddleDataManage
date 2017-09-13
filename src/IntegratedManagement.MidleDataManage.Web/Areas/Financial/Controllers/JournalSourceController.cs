using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.FinancialModule.JournalSource;
using IntegratedManagement.Entity.Param;
using IntegratedManageMent.Application.BusinessPartnerModule;
using IntegratedManageMent.Application.FinancialModule;
using ReportFormManage.Code.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.Areas.Financial.Controllers
{
    public class JournalSourceController : Controller
    {
        private readonly IJournalSourceApp _JournalSourceApp;
        private readonly IJournalRelationMapApp _JournalRelationMapApp;
        private readonly IBranchApp _BranchApp;
        public JournalSourceController(
            IJournalSourceApp IJournalSourceApp, 
            IJournalRelationMapApp IJournalRelationMapApp,
            IBranchApp IBranchApp)
        {
            this._JournalSourceApp = IJournalSourceApp;
            this._JournalRelationMapApp = IJournalRelationMapApp;
            this._BranchApp = IBranchApp;
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
                StringBuilder paramStr = new StringBuilder();
                QueryParam queryParam = new QueryParam();
                if (param.BeginDate != default(DateTime))
                    paramStr.Append($"(CreateDate ge '{param.BeginDate}')");
                if (param.EndDate != default(DateTime))
                    paramStr.Append($"and (CreateDate le '{param.BeginDate}')");
                if (!string.IsNullOrEmpty(param.Creator))
                    paramStr.Append($" and (Creator eq '{param.Creator}')");
                if (param.TransId != default(int))
                    paramStr.Append($" and (TransId eq '{param.TransId}')");
                if (param.TransType != default(int))
                    paramStr.Append($" and (TransType eq '{param.TransType}')");
                if (!string.IsNullOrEmpty(param.BPLName))
                    paramStr.Append($" and (BPLName eq '{param.BPLName}')");
                queryParam.filter = paramStr.ToString();
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

        [HttpPost]
        public async Task<ActionResult> GetBranchData()
        {
            try
            {
                QueryParam queryParam = new QueryParam();
                queryParam.orderby = "BPLId";
                var rt = await _BranchApp.GetBranchList(queryParam);
                return Json(new { state = ResultType.success.ToString(), data = Newtonsoft.Json.JsonConvert.SerializeObject(rt) });
            }
            catch (Exception ex)
            {
                return Json(new { state = ResultType.error.ToString(), message = ex.Message });
            }
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