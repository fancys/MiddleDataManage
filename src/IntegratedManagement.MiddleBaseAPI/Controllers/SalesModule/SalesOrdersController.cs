using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManagement.Entity.ValidEntity;
using IntegratedManageMent.Application.SalesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.SalesModule
{
    /// <summary>
    /// 
    /// </summary>
    [AllowAnonymous]
    [Authorize]
    public class SalesOrdersController : ApiController
    {
        private readonly ISalesOrderApp salesOrderApp;

        public SalesOrdersController(ISalesOrderApp ISalesOrderApp)
        {
            salesOrderApp = ISalesOrderApp;
        }
        /// <summary>
        /// 查询销售订单集合
        /// </summary>
        /// <param name="QuanryParam"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSalesOrders([FromUri]QueryParam QuanryParam)

        {
            Result<SalesOrder> result = new Result<SalesOrder>();
            try
            {
                var salesOrderList =await salesOrderApp.GetSalesOrderAsync(QuanryParam);
                result.Code = 0;
                result.Message = "Successful Operation";
                result.ResultObject = salesOrderList;
            }
            catch (Exception ex)
            {
                result.Code = 1001;
                result.Message = $"Failed Operation.ErrorMessage:{ex.Message}";
            }
            return Json(result);

        }

        /// <summary>
        /// 查询一条销售订单
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="QuaryParam"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSalesOrder(int Id, [FromUri]QueryParam QuaryParam)
        {
            Result<SalesOrder> result = new Result<SalesOrder>();
            try
            {
                var salesOrderList = await salesOrderApp.GetSalesOrderAsync(QuaryParam);
                result.Code = 0;
                result.Message = "successful operation";
                result.ResultObject = salesOrderList;
            }
            catch(Exception ex)
            {
                result.Code = 11002;
                result.Message = $"failed operation.ErrorMessage:{ex.Message}";
            }
            return Json(result);
        }

        /// <summary>
        /// 新建销售订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostSalesOrder([FromBody]List<SalesOrder> salesOrderList)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "SalesOrder_" + Guid.NewGuid();
            if (salesOrderList == null)
            {
                result.Code = 10001;
                result.Message = "请求的内容格式不正确";
            }
            else
            {
                foreach (var item in salesOrderList)
                {
                    //对销售订单的验证（只能验证主表，子表暂时不能验证）
                    var validationResult = ValidationHelper.ValidateEntity<SalesOrder>(item);
                    if (validationResult.HasError)
                    {
                        result.Code = 10002;
                        result.Message = "数据校验未通过";
                        result.ResultObject.Add(new SaveResult() { Code = 10002, UniqueKey = item.OMSDocEntry.ToString(), Message = validationResult.Errors.ForEachToString() });
                        continue;
                    }
                    try
                    {
                        var rt = await salesOrderApp.PostSalesOrderAsync(item);
                        if (rt.Code != 0)
                            result.Message = "failed operation.";
                        result.ResultObject.Add(rt);
                    }
                    catch (Exception ex)
                    {
                        result.Code = 11002;
                        result.Message = "failed operation.";
                        result.ResultObject.Add(new SaveResult() { Code = 1, UniqueKey = item.OMSDocEntry.ToString(), Message = ex.Message });
                    }
                }
            }
            return Json(result);
        }

        /// <summary>
        /// 部分更新销售订单
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        public IHttpActionResult PathSalesOrder()
        {
            return Ok();
        }

        /// <summary>
        /// 更新销售订单
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult PutSalesOrder()
        {
            return Ok();
        }

        /// <summary>
        /// 取消销售订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SalesOrders/{id}/Cancel")]
        public IHttpActionResult CancelSalesOrder()
        {
            return Ok();
        }
        
    }
}
