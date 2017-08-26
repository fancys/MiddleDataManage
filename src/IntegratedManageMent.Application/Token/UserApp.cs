using IntegratedManagement.Core;
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
	*	Create by Fancy at 2017/3/24 9:55:11
	===============================================================================================================================*/
    public class UserApp
    {
        IUserRepository _UserRepository = new UserDapperRepository();
        public async Task<User> GetUser(string UserName,string Password)
        {
            //Password加密
            return await  _UserRepository.GetUser(UserName, Md5.GetMd5Hash( Password));
        }

        public async Task<bool> Save(User user)
        {
            return await _UserRepository.Insert(user);
        }
    }
}
