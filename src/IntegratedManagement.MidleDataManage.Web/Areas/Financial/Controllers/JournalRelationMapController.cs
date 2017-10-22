using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.Help;
using IntegratedManagement.Entity.Param;
using IntegratedManageMent.Application.FinancialModule;
using IntegratedManageMent.Application.Help;
using IntegrateManagement.MiddleBaseService.B1;
using MagicBox.Log;
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
    public class JournalRelationMapController : Controller
    {
        IJournalRelationMapApp _journalRelationMapApp;
        ISerialNumberApp _serialNumberApp;
        public JournalRelationMapController(IJournalRelationMapApp IJournalRelationMapApp, ISerialNumberApp ISerialNumberApp)
        {
            this._journalRelationMapApp = IJournalRelationMapApp;
            this._serialNumberApp = ISerialNumberApp;
        }
        // GET: Financial/JournalRalationMap
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SearchJournalRelationMapList(ViewParam param)
        {
            try
            {
                /*
                 * 获取查询条件
                 */
                //if(param.MinCreateDate)
                StringBuilder paramStr = new StringBuilder();
                QueryParam queryParam = new QueryParam();
                if (param.BeginDate == default(DateTime) || param.BeginDate == null)
                    paramStr.Append($"(CreateDate ge '{DateTime.Now.AddDays(1-DateTime.Now.Day).ToShortDateString()}')");
                else
                    paramStr.Append($" (CreateDate ge '{param.BeginDate.Date.ToShortDateString()}')");
                if (param.EndDate == default(DateTime)||param.EndDate == null)
                    paramStr.Append($" and (CreateDate le '{DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToShortDateString()}')");
                else
                    paramStr.Append($" and (CreateDate le '{param.EndDate.Date.ToShortDateString()}')");
                if (!string.IsNullOrEmpty(param.Creator))
                    paramStr.Append($" and (Creator eq '{param.Creator}')");
                if(param.TransId != default(int))
                    paramStr.Append($" and (TransId eq '{param.TransId}')");
                if (param.TransType != default(int))
                    paramStr.Append($" and (TransType eq '{param.TransType}')");
                if (param.HandleStatu != default(int))
                    paramStr.Append($" and (HandleResult eq '{param.HandleStatu}')");
                if(!string.IsNullOrEmpty(param.BPLName))
                    paramStr.Append($" and (BPLName eq '{param.BPLName}')");
                if (param.BPLId != 0)
                    paramStr.Append($" and (BPLId eq '{param.BPLId}')");
                queryParam.filter = paramStr.ToString();
                queryParam.orderby = "BPLId,CreateDate,TransId";
                var rt = await _journalRelationMapApp.GetJournalRelationMapListAsync(queryParam);
                
                return Json(new { state = ResultType.success.ToString(), data = Newtonsoft.Json.JsonConvert.SerializeObject(JournalRelationMap.Hanlde(rt)) });
            }
            catch (Exception ex)
            {
                return Json(new { state = ResultType.error.ToString(), message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateJournal(JournalRelationMapList journalRelationMapList)
        {
            if(journalRelationMapList == null)
                return Json(new { state = ResultType.error.ToString(), message = "未选择需要同步的数据" });

            return await this.CreateJournal(journalRelationMapList.JournalRelationMaps);
        }
        [HttpPost]
        public async Task<ActionResult> SelectAndCreateJournal(string IDs)
        {
            if(string.IsNullOrEmpty(IDs))
                return Json(new { state = ResultType.error.ToString(), message = "选择的数据为空" });
            QueryParam queryParam = new QueryParam();
            queryParam.filter = string.Format("TransId in ({0})", IDs.TrimEnd(','));
            queryParam.orderby = "BPLId,CreateDate,TransId";
            string syncResult;
            try
            {
                var rt = await _journalRelationMapApp.GetJournalRelationMapListAsync(queryParam);
                if (rt.Count != 0)
                    //syncResult = await _journalRelationMapApp.CreateJournalEntry(rt);
                    return await this.CreateJournal(rt);
                else
                    syncResult = "数据查询出错";
            }
            catch(Exception ex)
            {
                Logger.Writer(ex);
                syncResult = ex.Message;
            }
            return Json(new { state = ResultType.success.ToString(), message = syncResult });
        }

        private async Task<ActionResult> CreateJournal(List<JournalRelationMap> journalRelations)
        {
            foreach (var item in journalRelations)
            {
                var serialNumber = await _serialNumberApp.GetSerialNumberOfDateTimeNow();
                item.SerialNumber =  GetSerialNumber(serialNumber);
                serialNumber.CurrentNumber++;
                try
                {
                    #region 生成分录条件
                    //1 需要拆分且拆分标识字段为N的
                    if (item.IsApart == "Y")
                    {
                        if (item.IsPositiveSync == "N")
                        {
                            //拆分正数单据
                            var rt = JournalEntryService.ApartPositiveJournal(item);
                            var result = await _journalRelationMapApp.UpdateJournalRelationMapPositiveStatuAsync(rt);
                            if (rt.SyncResult == "Y")
                                await this._serialNumberApp.UpdateCurrentNumber(serialNumber);
                            
                        }
                        if (item.IsMinusSync == "N")
                        {
                            //拆分负数单据
                            var rt = JournalEntryService.ApartMinusJournal(item);
                            await _journalRelationMapApp.UpdateJournalRelationMapMinusStatuAsync(rt);
                            if (rt.SyncResult == "Y")
                                await this._serialNumberApp.UpdateCurrentNumber(serialNumber);
                        }
                    }
                    //2 不需要拆分且生成标识字段为N的
                    else if (item.IsApart == "N" && item.IsSync == "N")
                    {
                        var rt = JournalEntryService.CreateJournal(item);
                        await _journalRelationMapApp.UpdateJournalRelationMapStatuAsync(rt);
                        if (rt.SyncResult == "Y")
                            await this._serialNumberApp.UpdateCurrentNumber(serialNumber);
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    Logger.Writer(ex);
                    await _journalRelationMapApp.UpdateJournalRelationMapStatuAsync(
                        new Entity.Document.DocumentSync()
                        {
                            DocEntry = item.DocEntry.ToString(),
                            SyncResult = "N",
                            SyncMsg = ex.Message
                        });
                }
            }
            return Json(new { state = ResultType.success.ToString(), message = "同步完成" });
        }

        private string GetSerialNumber(SerialNumber SerialNumber)
        {
            return SerialNumber.Year + SerialNumber.Month + SerialNumber.CurrentNumber.ToString("000");
        }
    }
}