using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseReturn;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.ValidEntity;
using IntegratedManageMent.Application.PurchaseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.PurchaseModule
{
    [AllowAnonymous]
    [Authorize]
    public class PurchaseReturnsController : ApiController
    {
        private readonly IPurchaseReturnApp _purchaseReturnApp;

        public PurchaseReturnsController(IPurchaseReturnApp IPurchaseReturnApp)
        {
            _purchaseReturnApp = IPurchaseReturnApp;
        }
        /// <summary>
        /// 查询采购退货单集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetPurchaseReturns([FromUri]QueryParam queryParam)
        {
            Result<PurchaseReturn> result = new Result<PurchaseReturn>();
            try
            {
                var PurchaseReturnList = await _purchaseReturnApp.GetPurchaseReturnAsync(queryParam);
                result.Code = 0;
                result.Message = "Successful Operation";
                result.ResultObject = PurchaseReturnList;
            }
            catch (Exception ex)
            {
                result.Code = 0;
                result.Message = $"Failed Operation ErrorMessage:{ex.Message}";
            }
            return Json<Result<PurchaseReturn>>(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostPurchaseReturn([FromBody]List<PurchaseReturn> purchaseReturnList)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "PurchaseReturn_" + Guid.NewGuid();
            if (purchaseReturnList == null)
            {
                result.Code = 10001;
                result.Message = "请求的内容格式不正确";
            }
            else
            {
                result.Code = 0;
                result.Message = "successful operation.";
                foreach (var item in purchaseReturnList)
                {
                    var validationResult = ValidationHelper.ValidateEntity<PurchaseReturn>(item);
                    if (validationResult.HasError)
                    {
                        result.Code = 10002;
                        result.Message = "数据校验未通过";
                        result.ResultObject.Add(new SaveResult() { Code = 10002,  Message = validationResult.Errors.ForEachToString() });
                        continue;
                    }
                    
                    try
                    {
                        item.CreateDate = DateTime.Now;
                        item.UpdateDate = DateTime.Now;
                        var rt = await _purchaseReturnApp.SavePurchaseReturnAsync(item);
                        if(rt.Code != 0)
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
                        result.ResultObject.Add(new SaveResult() { Code = 1, Message = ex.Message });
                    }
                    
                }
            }
            return Ok(result);
        }
        
    }
}