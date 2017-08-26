using IntegratedManagement.Entity.Token;
using IntegratedManageMent.Application.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.Token
{
    [System.Web.Mvc.AllowAnonymous]
    public class ApplyTokenController : Controller
    {
        // GET: ApplyToken
        public ActionResult Index()
        {
            return View();
        }
        
        public async Task<ActionResult> RefreshToken(string UserName,string Password)
        {
            //接收用户名
            //用户名
            //密码
            //clientId = new Guid
            //clientSecret = new Guid
            //clientId、clientSecret保存数据库
            //获取refreshToken 返回获取refreshToken、clientId、clientSecret
            string clientId = Guid.NewGuid().ToString();
            string clientSecret = Guid.NewGuid().ToString();

            //保存clientId和clientSecret
            var client = new Client() { Id = clientId, Secret = clientSecret.ToString(), IsActive = "Y", RefreshTokenLifeTime = 36000,DateAdded=DateTime.Now };
            ClientApp _clientApp = new ClientApp();
            if (!await _clientApp.Save(client))
                return null;


            try
            {
                //调用password模式 oauth验证
                var parameters = new Dictionary<string, string>();
                parameters.Add("grant_type", "password");
                parameters.Add("username", UserName);
                parameters.Add("password", Password);
                HttpClient _httpClient;
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("http://localhost:57397");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret))
                        );

                var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
                var responseValue = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(responseValue);
                    return Json(new { RefreshToken = accessToken.refresh_token,ClientId =clientId,ClientSecret =clientSecret});
                }
                else
                {
                    return Json("error request.");
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
    }
}