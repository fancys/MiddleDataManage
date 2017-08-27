using IntegratedManagement.MiddleBaseAPI.Providers;
using IntegratedManagement.RepositoryDapper.Token;
using IntegratedManageMent.Application.Token;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegratedManagement.MiddleBaseAPI
{
	/*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 17:03:27
	===============================================================================================================================*/
	public partial class Startup
	{
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new MidApplicationOAuthProvider(new ClientApp(),new UserApp(new UserDapperRepository())),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //在生产模式下设 AllowInsecureHttp = false
                AllowInsecureHttp = true,
                RefreshTokenProvider = new MidRefreshTokenProvider(new RefreshTokenApp()),
                // AccessTokenFormat = new MidSecureDataFormat()
            };

            // 使应用程序可以使用不记名令牌来验证用户身份
            app.UseOAuthBearerTokens(OAuthOptions);

        }
    }
}