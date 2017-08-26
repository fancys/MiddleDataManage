using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PaymentReceivedModule.Refund;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.ValidEntity;
using IntegratedManageMent.Application.PaymentReceivedModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.PaymentReceivedModule
{
    [AllowAnonymous]
    [Authorize]
    public class RefundsController : ApiController
    {
        private readonly IRefundApp _refundApp;

        public RefundsController(IRefundApp IRefundApp)
        {
            _refundApp = IRefundApp;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRefunds([FromUri]QueryParam queryParam)
        {
            Result<Refund> result = new Result<Refund>();
            try
            {
                var PurchaseReturnList = await _refundApp.GetRefundListAsync(queryParam);
                result.Code = 0;
                result.Message = "Successful Operation";
                result.ResultObject = PurchaseReturnList;
            }
            catch (Exception ex)
            {
                result.Code = 0;
                result.Message = $"Failed Operation ErrorMessage:{ex.Message}";
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostRefunds([FromBody]List<Refund> refundList)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "Refund_" + Guid.NewGuid();
           
            if (refundList == null)
            {
                result.Code = 10001;
                result.Message = "请求的内容格式不正确";
            }
            else
            {
                result.Code = 0;
                result.Message = "successful operation.";
                foreach (var item in refundList)
                {
                    var validationResult = ValidationHelper.ValidateEntity<Refund>(item);
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
                        var rt = await _refundApp.PostRefundAsync(item);
                        if (rt.Code != 0)
                        {
                            result.Code = 11001;
                            result.Message = "failed operation.";
                            result.ResultObject.Add(new SaveResult() { Code = 11001, UniqueKey = item.OMSDocEntry.ToString() });
                        }
                        result.ResultObject.Add(rt);
                    }
                    catch (Exception ex)
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
