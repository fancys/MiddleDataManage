using IntegratedManagement.Entity.Param;
using IntegratedManageMent.Application.BusinessPartnerModule;
using IntegratedManageMent.Application.InventoryModule;
using MagicBox.Log;
using ReportFormManage.Code.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.Areas.Financial.Controllers
{
    // SAP系统主数据同步到财务帐套
    public class SAPToFinanceController : Controller
    {
        private readonly IBusinessPartnerApp _BusinessPartnerApp;
        private readonly IProfitCentersApp _ProfitCentersApp;
        private readonly IWarehouseApp _WarehouseApp;
        public SAPToFinanceController(IBusinessPartnerApp IBusinessPartnerApp, IProfitCentersApp IProfitCentersApp, IWarehouseApp IWarehouseApp)
        {
            this._BusinessPartnerApp = IBusinessPartnerApp;
            this._ProfitCentersApp = IProfitCentersApp;
            this._WarehouseApp = IWarehouseApp;
        }
        // GET: Financial/SAPToFinance
        public ActionResult Index()
        {
            return View();
        }

        private async Task<string> SyncBusinessPartner()
        {
            try
            {
                // 查询数据
                QueryParam queryParam = new QueryParam();
                queryParam.orderby = "Code";

                var businessPartnerList = await _BusinessPartnerApp.GetBusinessPartnerList(queryParam);
                if(businessPartnerList.Count == 0)
                {
                    return "未找到业务伙伴数据，请检查视图。";
                }
                Logger.Writer("Search Data:" + Newtonsoft.Json.JsonConvert.SerializeObject(businessPartnerList));

                // 同步数据
                var rtMsg = _BusinessPartnerApp.CreateBusinessPartner(businessPartnerList);
                Logger.Writer("Sync Result:" + Newtonsoft.Json.JsonConvert.SerializeObject(rtMsg));
                return rtMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> SyncWarehouse()
        {
            try
            {
                // 查询数据
                QueryParam queryParam = new QueryParam();
                queryParam.orderby = "Code";

                var warehouseList = await _WarehouseApp.GetWarehouseAsync(queryParam);
                if (warehouseList.Count == 0)
                {
                    return "未找到仓库主数据数据，请检查视图。";
                }
                Logger.Writer("Search Data:" + Newtonsoft.Json.JsonConvert.SerializeObject(warehouseList));

                // 同步数据
                var rtMsg = _WarehouseApp.CreateWarehouse(warehouseList);
                Logger.Writer("Sync Result:" + Newtonsoft.Json.JsonConvert.SerializeObject(rtMsg));
                return rtMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> SyncProfitCenter()
        {
            try
            {
                // 查询数据
                QueryParam queryParam = new QueryParam();
                queryParam.orderby = "Code";

                var profitCenterList = await _ProfitCentersApp.GetProfitCentersList(queryParam);
                if (profitCenterList.Count == 0)
                {
                    return "未找到成本中心主数据数据，请检查视图。";
                }
                Logger.Writer("Search Data:" + Newtonsoft.Json.JsonConvert.SerializeObject(profitCenterList));

                // 同步数据
                var rtMsg = _ProfitCentersApp.CreateProfitCenter(profitCenterList);
                Logger.Writer("Sync Result:" + Newtonsoft.Json.JsonConvert.SerializeObject(rtMsg));
                return rtMsg;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SyncData(string ServiceName)
        {
            switch (ServiceName)
            {
                case "ProfitCenter":
                    {
                        var rt = await SyncProfitCenter();
                        return Json(new { result = 1, message = rt });
                    }
                  
                case "BusinessPartner":
                    {
                        var rt = await SyncBusinessPartner();
                        return Json(new { result = 1, message = rt });
                    }
                case "Warehouse":
                    {
                        var rt = await SyncWarehouse();
                        return Json(new { result = 1, message = rt });
                    }
                default:
                    return Json(new { result = 1, message = "未找到同步服务." });

            }
            
        }
    }
}