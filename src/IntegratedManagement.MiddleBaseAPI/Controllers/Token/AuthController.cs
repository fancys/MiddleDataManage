using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace IntegratedManagement.MiddleBaseAPI.Controllers.Token
{
    [AllowAnonymous]
    public class AuthController : ApiController
    {
        /// <summary>
        /// 根据refresh_token获取access_token(不需要用户名和密码)
        /// </summary>
        /// <param name="client_id"></param>
        /// <param name="client_secret"></param>
        /// <param name="refresh_token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetToken(string client_id,string client_secret,string refresh_token)
        {
            var clientId = client_id;
            var clientSecret = client_secret;
            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:57395");
            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "refresh_token");
            parameters.Add("refresh_token", refresh_token);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

            var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();
            return Json(responseValue);
        }
    }
}
