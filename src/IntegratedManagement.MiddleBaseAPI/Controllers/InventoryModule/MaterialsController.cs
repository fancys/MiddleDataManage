using IntegratedManagement.Entity.InventoryModule.Material;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.Entity.ValidEntity;
using IntegratedManageMent.Application.InventoryModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.InventoryModule
{
   

    [AllowAnonymous]
    [Authorize]
    public class MaterialsController : ApiController
    {
        private readonly IMaterialApp _IMaterialApp;

        public MaterialsController(IMaterialApp IMaterialApp)
        {
            _IMaterialApp = IMaterialApp;
        }
        /// <summary>
        /// 查询物料集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetMaterial([FromUri]QueryParam QuanryParam)
        {
            Result<Material> result = new Result<Material>();
            try
            {
                var materielList = await _IMaterialApp.GetMaterialAsync(QuanryParam);
                result.Code = 0;
                result.Message = "Successful Operation";
                result.ResultObject = materielList;
            }
            catch (Exception ex)
            {
                result.Code = 0;
                result.Message = $"Failed Operation ErrorMessage:{ex.Message}";
            }
            return Json<Result<Material>>(result);
        }

        /// <summary>
        /// 添加物料集合
        /// </summary>
        /// <param name="materialList"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> PostMaterials([FromBody]List<Material> materialList)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "Post  Materials_" + Guid.NewGuid();
           
            if (materialList == null)
            {
                result.Code = 10001;
                result.Message = "请求的内容格式不正确";
            }
            else
            {
                foreach (var item in materialList)
                {
                    try
                    {
                        var validationResult = ValidationHelper.ValidateEntity<Material>(item);
                        if (validationResult.HasError)
                        {
                            result.Code = 11002;
                            result.Message = "数据校验未通过";
                            result.ResultObject.Add(new SaveResult() { Code = 11002, UniqueKey = item.ItemCode, Message = validationResult.Errors.ForEachToString() });
                            continue;
                        }
                        try
                        {
                            var rt = await _IMaterialApp.SaveMaterialAsync(item);
                            if (rt.Code != 0)
                            {
                                result.Code = 11001;
                                result.Message = "failed operation.";
                            }
                            result.ResultObject.Add(rt);
                        }
                        catch (Exception ex)
                        {
                            result.Code = 11002;
                            result.Message = "failed operation.";
                            result.ResultObject.Add(new SaveResult() { Code = 1, UniqueKey = item.ItemCode.ToString(), Message = ex.Message });
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Code = 11002;
                        result.Message = "failed operation.";
                        result.ResultObject.Add(new SaveResult() { Code = 1, Message = ex.Message });
                    }
                    
                }
            }
            return Json(result);
        }

        /// <summary>
        /// 修改一个物料
        /// </summary>
        /// <param name="material"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        [Route("api/Materials/{ItemCode}")]
        [HttpPatch]
        public async Task<IHttpActionResult> PatchMaterial([FromBody]Material material,string ItemCode)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "Patch a Material_" + Guid.NewGuid();
           
            try
            {
                if (material == null && string.IsNullOrEmpty(ItemCode))
                {
                    result.Code = 10001;
                    result.Message = "请求的内容格式不正确";
                }
                else
                {
                    var validationResult = ValidationHelper.ValidateEntity<Material>(material);
                    if (validationResult.HasError)
                    {
                        result.Code = 11002;
                        result.Message = "数据校验未通过";
                        result.ResultObject.Add(new SaveResult() { Code = 11002, UniqueKey = material.ItemCode, Message = validationResult.Errors.ForEachToString() });
                    }
                    else
                    {
                        material.ItemCode = ItemCode;
                        material.IsSync = "N";
                        material.UpdateDate = DateTime.Now;
                        var rt = await _IMaterialApp.PatchMaterialAsync(material);
                        if (rt)
                        {
                            result.Code = 0;
                            result.Message = "successful operation.";
                            result.ResultObject.Add(new SaveResult() { Code = 0, UniqueKey = material.ItemCode, Message = result.Message });
                        }
                        else
                        {
                            result.Code = 11001;
                            result.Message = "failed operation.";
                            result.ResultObject.Add(new SaveResult() { Code = 11002, UniqueKey = material.ItemCode, Message = result.Message });
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


        /// <summary>
        /// 修改物料集合
        /// </summary>
        /// <param name="materialList"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IHttpActionResult> PatchMaterial([FromBody]List<Material> materialList)
        {
            Result<SaveResult> result = new Result<SaveResult>();
            string guid = "Patch  Materials" + Guid.NewGuid();
            
            if (materialList == null)
            {
                result.Code = 10001;
                result.Message = "请求的内容格式不正确";
            }
            else
            {
                result.Code = 0;
                result.Message = "successful operation.";
                foreach (var item in materialList)
                {
                    try
                    {
                        var validationResult = ValidationHelper.ValidateEntity<Material>(item);
                        if (validationResult.HasError)
                        {
                            result.Code = 11002;
                            result.Message = "数据校验未通过";
                            result.ResultObject.Add(new SaveResult() { Code = 11002, UniqueKey = item.ItemCode, Message = validationResult.Errors.ForEachToString() });
                        }
                        else
                        {
                            
                            item.IsSync = "N";
                            item.UpdateDate = DateTime.Now;
                            var rt = await _IMaterialApp.PatchMaterialAsync(item);
                            if (rt)
                            {
                                result.ResultObject.Add(new SaveResult() { Code = 0, UniqueKey = item.ItemCode, ReturnUniqueKey = item.ItemCode, Message = result.Message });
                            }
                            else
                            {
                                result.Code = 11001;
                                result.Message = "failed operation.";
                                result.ResultObject.Add(new SaveResult() { Code = 1, UniqueKey = item.ItemCode, Message = result.Message });
                            }
                        }
                           
                    }
                    catch (Exception ex)
                    {
                        result.Code = 11002;
                        result.Message = "failed operation.Excetion:" + ex.Message;
                        result.ResultObject.Add(new SaveResult() { Code = 1, UniqueKey = item.ItemCode,Message = ex.Message });
                    }
                }
            }
            return Ok(result);
        }

    }
}