using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IntegratedManagement.MiddleBaseAPI.Tests.OAuthUnitTest
{
    [TestClass]
    public class OAuthClientTest
    {
        /// <summary>
        /// 测试授权 OAuth 客户端模式
        /// </summary>
        private HttpClient _httpClient;

        public OAuthClientTest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:57397");
        }

        [TestMethod]
        public void Get_Accesss_Token_By_Client_Credentials_Grant()
        {
            var clientId = "1234";
            var clientSecret = "5678";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "client_credentials");
            var rt = _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters))
                .Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(rt);
        }

        [TestMethod]
        public async Task Get_Accesss_Token_By_Resource_Owner_Password_Credentials_Grant()
        {
            Console.WriteLine(await GetAccessToken());
        }

        private async Task<string> GetAccessToken()
        {
            var clientId = "7234E65C-12E7-456C-B28B-638AEB91B421";
            var clientSecret = "77E25048-2863-4A3C-892E-283E77F5E3C7";

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "password");
            parameters.Add("username", "12");
            parameters.Add("password", "23.com");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret))
                );

            var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JObject.Parse(responseValue)["refresh_token"].Value<string>();
            }
            else
            {
                Console.WriteLine(responseValue);
                return string.Empty;
            }
        }

        [TestMethod]
        public async Task Call_WebAPI_By_Resource_Owner_Password_Credentials_Grant()
        {
            var token = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine(await (await _httpClient.GetAsync("/api/users/current")).Content.ReadAsStringAsync());
        }

        [TestMethod]
        public async Task GetAccessTokenTest()
        {
            var clientId = "7234E65C-12E7-456C-B28B-638AEB91B421";
            var clientSecret = "77E25048-2863-4A3C-892E-283E77F5E3C7";

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "password");
            parameters.Add("username", "[username]");
            parameters.Add("password", "[password]");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

            var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseValue);
        }
        [TestMethod]
        public async Task GetAccessTokenByRefreshTokenTest()
        {
            var clientId = "29aa448d-5307-4f82-a11b-a9d648ecef68";
            var clientSecret = "d9279474-be78-4080-9f19-93c1511cdbaf";

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "refresh_token");
            parameters.Add("refresh_token", "VzNGYIrk5BlDpI_4V7gWqfiR6rNIYFauSpPlY267tjslwowexTe_Q5Q4K4Iji7MnHF0");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

            var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseValue);
        }

        [TestMethod]
        public async Task Call_WebAPI_By_Access_Token()
        {
            var token = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "_C4oz0AM-oUbqq9CK0uRGdqwgt9nAj5w3E52GIvjzBJRs3ycw4CZsGjBovo50tQLues0ES2HpB4rXGnAVcFNd7uFlF3vhEc1wzNsnk0Mjt6VLNf7rbijdtclgz5OK9gkW9odW2daX0XmoBW23JiXIs9VTC1XgsNUM2tOnKX1jYN9Wc21LKCg0Md_565hcp0SQAOGB_n8pfN7yAjTdku9Cw");
            var rt = await (await _httpClient.GetAsync("/api/values")).Content.ReadAsStringAsync();
            Console.WriteLine(rt);
        }
    }
}
