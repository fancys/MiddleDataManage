using IntegratedManagement.Entity.Token;
using IntegratedManagement.IRepository.Token;
using IntegratedManagement.RepositoryDapper.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 17:34:28
	===============================================================================================================================*/
    public class RefreshTokenApp
    {
        IRefreshTokenRepository _refreshRepository = new RefreshTokenRepositoryDapper();

        public async Task<RefreshToken> Get(string Id)
        {
            return await _refreshRepository.FindById(Id);
        }

        public async Task<RefreshToken> GetByClientId(string ClientId)
        {
            return await _refreshRepository.FindByClientId(ClientId);
        }

        public async Task<bool> Save(RefreshToken refreshToken)
        {
            return await _refreshRepository.Insert(refreshToken);
        }

        public async Task<bool> Remove(string Id)
        {
            return await _refreshRepository.Delete(Id);
        }
    }
}
