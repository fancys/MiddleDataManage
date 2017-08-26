using ReportFormManage.Code.Json;
using ReportFormManage.Code.VerifyCode;
using ReportFormManage.Code.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntegratedManagement.MidleDataManage.Web.Controllers
{
    public class LoginController : Controller
    {
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

        public ActionResult CheckLogin(string username, string password, string code)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
        }
    }
}