using IntegratedManageMent.Application.Token;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace IntegratedManagement.MiddleBaseAPI.Providers
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 16:50:10
	===============================================================================================================================*/
    public class MidApplicationOAuthProvider: OAuthAuthorizationServerProvider
    {
        ClientApp _clientApp;
        UserApp _userApp;
        public MidApplicationOAuthProvider(ClientApp clientApp,UserApp userApp)
        {
            _clientApp = clientApp;
            _userApp = userApp;
        }
        #region 密码模式
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret)) { return; }
            //如果用户未申请refreshtoken  不进行验证
            var client = await _clientApp.Get(clientId);
            if (client == null) { return; }
            if (client.Secret != clientSecret) { return; }

            context.OwinContext.Set<string>("as:client_id", clientId);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated(clientId);


            await base.ValidateClientAuthentication(context);
        }
        public override async Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);

            context.Validated(oAuthIdentity);
            await base.GrantClientCredentials(context);
        }

        public override async Task GrantResourceOwnerCredentials(
       OAuthGrantResourceOwnerCredentialsContext context)
        {
            //调用后台的登录服务验证用户名与密码
            var user = await _userApp.GetUser(context.UserName, context.Password);
            if (user != null)
            {
                var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                context.Validated(oAuthIdentity);
                await base.GrantResourceOwnerCredentials(context);
            }
            else
                throw new Exception("ERROR Incorrect username or password!");

            
        }
        #endregion

        /// <summary>
        /// 客户端模式 重载RefreshToken分发
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            try
            {
                var newId = new ClaimsIdentity(context.Ticket.Identity);
                newId.AddClaim(new Claim("newClaim", "refreshToken"));

                var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
                context.Validated(newTicket);

                await base.GrantRefreshToken(context);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}