using IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.ValidEntity;
using IntegratedManageMent.Application.BusinessPartnerModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.BusinessPartnerModule
{
    [AllowAnonymous]
    [Authorize]
    public class BusinessPartnersController : ApiController
    {

        IBusinessPartnerApp _IbusinessPartnerApp;
        //IBusinessPartnerRepository _IBusinessPartnerRepository;
        public BusinessPartnersController(IBusinessPartnerApp IBusinessPartnerApp)
        {
            _IbusinessPartnerApp = IBusinessPartnerApp;
            //_IBusinessPartnerRepository = IBusinessPartnerRepository;
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetBusinessPartner([FromUri]QueryParam queryParam)
        {
            Result<BusinessPartner> result = new Result<BusinessPartner>();
            try
            {
                var PurchaseReturnList = await _IbusinessPartnerApp.GetBusinessPartnerList(queryParam);
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
        public async Task<IHttpActionResult> PostBusinessPartner([FromBody]List<BusinessPartner> businessPartnerList)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "Post BusinessPartners_" + Guid.NewGuid();
            
            try
            {
                if (businessPartnerList == null)
                {
                    result.Code = 10001;
                    result.Message = "请求的内容格式不正确";
                }
                else
                {
                    result.Code = 0;
                    foreach (var item in businessPartnerList)
                    {
                        try
                        {
                            var validationResult = ValidationHelper.ValidateEntity<BusinessPartner>(item);
                            if (validationResult.HasError)
                            {
                                result.Code = 10002;
                                result.Message = "数据校验未通过";
                                result.ResultObject.Add(new SaveResult() { Code = 10002, UniqueKey = item.CardCode.ToString(), Message = validationResult.Errors.ForEachToString() });
                                continue;
                            }
                            try
                            {
                                var rt = await _IbusinessPartnerApp.SaveBusinessPartner(item);
                                if (rt.Code != 0)
                                    result.Message = "failed operation.";
                                result.ResultObject.Add(rt);
                            }
                            catch (Exception ex)
                            {
                                result.Code = 11002;
                                result.Message = "failed operation.";
                                result.ResultObject.Add(new SaveResult() { Code = 1, UniqueKey = item.CardCode.ToString(), Message = ex.Message });
                            }
                            
                        }
                        catch(Exception ex)
                        {
                            result.Code = 11002;
                            result.Message = "failed operation." ;
                            result.ResultObject.Add(new SaveResult() { Code = 1,UniqueKey =item.CardCode, Message = "exception:" + ex.Message });
                        }
                       
                    }
                }
            }
            catch(Exception ex)
            {
                result.Code = 11002;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        /// <summary>
        /// 修改业务伙伴主数据(只能更新一个业务伙伴)
        /// </summary>
        /// <param name="businessPartnerList"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("api/BusinessPartners/{CardCode}")]
        public async Task<IHttpActionResult> PatchBusinessPartner([FromBody]BusinessPartner businessPartner,string CardCode)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "Patch a BusinessPartner_" + Guid.NewGuid();
            
            try
            {
                if (businessPartner == null&&string.IsNullOrEmpty(CardCode))
                {
                    result.Code = 10001;
                    result.Message = "请求的内容格式不正确";
                }
                else
                {
                    var validationResult = ValidationHelper.ValidateEntity<BusinessPartner>(businessPartner);
                    if (validationResult.HasError)
                    {
                        result.Code = 10002;
                        result.Message = "数据校验未通过";
                        result.ResultObject.Add(new SaveResult() { Code = 10002, UniqueKey = businessPartner.CardCode.ToString(), Message = validationResult.Errors.ForEachToString() });

                    }
                    else
                    {
                        businessPartner.CardCode = CardCode;
                        businessPartner.IsSync = "N";
                        businessPartner.UpdateDate = DateTime.Now;
                        var rt = await _IbusinessPartnerApp.PatchBusinessPartner(businessPartner);
                        if (rt)
                        {
                            result.Code = 0;
                            result.Message = "successful operation.";
                        }
                        else
                        {
                            result.Code = 11001;
                            result.Message = "failed operation.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Code = 11002;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IHttpActionResult> PatchBusinessPartner([FromBody]List<BusinessPartner> businessPartnerList)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "Patch BusinessPartners_" + Guid.NewGuid();
            
            if (businessPartnerList == null)
            {
                result.Code = 10001;
                result.Message = "请求的内容格式不正确";
            }
            else
            {
                result.Code = 0;
                result.Message = "successful operation.";

                foreach (var item in businessPartnerList)
                {
                    try
                    {
                        item.IsSync = "N";
                        item.UpdateDate = DateTime.Now;
                        var rt = await _IbusinessPartnerApp.PatchBusinessPartner(item);
                        if (rt)
                        {
                            result.ResultObject.Add(new SaveResult() { Code = 0, UniqueKey = item.CardCode,ReturnUniqueKey = item.CardCode });
                        }
                        else
                        {
                            result.Code = 11001;
                            result.Message = "failed operation.";
                            result.ResultObject.Add(new SaveResult() { Code = 1, UniqueKey = item.CardCode });

                        }
                    }
                    catch (Exception ex)
                    {
                        result.Code = 11002;
                        result.Message = "failed operation.Excetion:" + ex.Message;
                        result.ResultObject.Add(new SaveResult() { Code = 1, UniqueKey = item.CardCode });
                    }
                }
            }
            return Ok(result);
        }

    }
}
