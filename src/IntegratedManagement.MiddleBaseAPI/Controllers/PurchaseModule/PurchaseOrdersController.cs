using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseOrder;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.ValidEntity;
using IntegratedManageMent.Application.PurchaseModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.PurchaseModule
{
    [AllowAnonymous]
    [Authorize]
    public class PurchaseOrdersController : ApiController
    {
        private readonly IPurchaseOrderApp purchaseOrderApp;

        public PurchaseOrdersController(IPurchaseOrderApp IPurchaseOrderApp)
        {
            purchaseOrderApp = IPurchaseOrderApp;
        }
        /// <summary>
        /// 查询采购订单集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetPurchaseOrder([FromUri]QueryParam queryParam)
        {
            
            Result<PurchaseOrder> result = new Result<PurchaseOrder>();
            try
            {
                var purchaseOrderList = await purchaseOrderApp.GetPurchaseOrderAsync(queryParam);
                result.Code = 0;
                result.Message = "Successful Operation";
                result.ResultObject = purchaseOrderList;
            }
            catch(Exception ex)
            {
                result.Code = 0;
                result.Message = $"Failed Operation ErrorMessage:{ex.Message}";
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostPurchaseOrder([FromBody]List<PurchaseOrder> purchaseOrderList)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "PurchaseOrder_" + Guid.NewGuid();
           
            if (purchaseOrderList == null)
            {
                result.Code = 10001;
                result.Message = "请求的内容格式不正确";
            }
            else
            {
                result.Code = 0;
                result.Message = "successful operation.";
                foreach (var item in purchaseOrderList)
                {
                    var validationResult = ValidationHelper.ValidateEntity<PurchaseOrder>(item);
                    if (validationResult.HasError)
                    {
                        result.Code = 10002;
                        result.Message = "数据校验未通过";
                        result.ResultObject.Add(new SaveResult() { Code = 10002, UniqueKey = item.OMSDocEntry.ToString(), Message = validationResult.Errors.ForEachToString() });
                        continue;
                    }
                    
                    try
                    {
                        item.CreateDate = DateTime.Now;
                        item.UpdateDate = DateTime.Now;
                        var rt = await purchaseOrderApp.SavePurchaseOrderAsync(item);
                        if (rt.Code != 0)
                        {
                            result.Code = 11001;
                            result.Message = "failed operation.";
                        }
                        result.ResultObject.Add(rt);
                    }
                    catch(Exception ex)
                    {
                        result.Code = 11002;
                        result.Message = "failed operation.";
                        result.ResultObject.Add(new SaveResult() { Code = 11002, UniqueKey = item.OMSDocEntry.ToString(), Message = ex.Message });
                    }
                }
            }
            return Ok(result);
        }

        
    }
}