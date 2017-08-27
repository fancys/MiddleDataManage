using IntegratedManageMent.Application.Token;
using ReportFormManage.Code.DESEncrypt;
using ReportFormManage.Code.Extend;
using ReportFormManage.Code.Json;
using ReportFormManage.Code.VerifyCode;
using ReportFormManage.Code.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserApp _userApp;
        public LoginController(IUserApp IUserApp)
        {
            _userApp = IUserApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 更新验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

        public async  Task<ActionResult> CheckLogin(string username, string password, string code)
        {
            try
            {
                if (Session["nfine_session_verifycode"].IsEmpty() || Md5.md5(code.ToLower(), 16) != Session["nfine_session_verifycode"].ToString())
                {
                    throw new Exception("验证码错误，请重新输入");
                }
                var user =  await _userApp.CheckUser(username, password);
                return Json(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" });
            }
            catch(Exception ex)
            {
                var result = Json(new  { state = ResultType.error.ToString(), message = ex.Message });
                return result;
            }
           
        }
    }
}