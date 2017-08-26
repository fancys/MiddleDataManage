using IntegratedManagement.Entity.Token;
using IntegratedManageMent.Application.Token;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace IntegratedManagement.MiddleBaseAPI.Providers
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 16:49:53
    *	get access_token by RefreshToken(Client Credentials)
	===============================================================================================================================*/

    public class MidRefreshTokenProvider: AuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, string> _refreshTokens = new ConcurrentDictionary<string, string>();
        private RefreshTokenApp _refreshTokenApp;
        public MidRefreshTokenProvider(RefreshTokenApp refreshTokenApp)
        {
            _refreshTokenApp = refreshTokenApp;
        }

        public override async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clietId = context.OwinContext.Get<string>("as:client_id");
            if (string.IsNullOrEmpty(clietId)) return;
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");
            if (string.IsNullOrEmpty(refreshTokenLifeTime)) return;

            //根据clientId获取refreshToken，如果refreshToken过期 返回错误
            var refreshToken = await _refreshTokenApp.GetByClientId(clietId);
            if(refreshToken==null ||refreshToken.ExpiresUtc<=DateTime.Now)
            {
                //未获取到refreshToken 则创建新的refreshToken
                refreshToken = new RefreshToken()
                {
                    Id = CreateNewRefreshTokenID(),
                    ClientId = new Guid(clietId).ToString(),
                    UserName = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddSeconds(Convert.ToDouble(refreshTokenLifeTime)),
                    ProtectedTicket = context.SerializeTicket()
                };
                await _refreshTokenApp.Save(refreshToken);
            }
            context.Ticket.Properties.IssuedUtc = refreshToken.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = refreshToken.ExpiresUtc;
            context.SetToken(refreshToken.Id);
        }

        public override async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            try
            {
                var refreshToken = await _refreshTokenApp.Get(context.Token);
                if (refreshToken != null)
                {
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string CreateNewRefreshTokenID()
        {
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[50];
            cryptoRandomDataGenerator.GetBytes(buffer);
            return Convert.ToBase64String(buffer).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }
    }
}