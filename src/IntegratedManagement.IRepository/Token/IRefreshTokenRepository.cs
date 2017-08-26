using IntegratedManagement.Entity.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 17:35:14
	===============================================================================================================================*/
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> FindById(string Id);

        Task<RefreshToken> FindByClientId(string ClientId);
        Task<bool> Insert(RefreshToken refreshToken);
        Task<bool> Delete(string Id);
        Task<bool> Update(RefreshToken refreshToken);
    }
}
