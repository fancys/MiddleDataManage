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
    public class UserApp:IUserApp
    {
        private readonly IUserRepository _UserRepository;
        public UserApp(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public async Task<User> CheckUser(string UserName, string Password)
        {
            try
            {
                var user = await GetUser(UserName);
                if (user != null && user.IsActive == "Y"&& user.IsDelete == "N")
                {
                    if (Password == user.Password)
                        return user;
                    else
                        throw new Exception("用户名或密码不正确");
                }
                else
                {
                    throw new EntryPointNotFoundException("不存在该用户");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<User> GetUser(string UserName)
        {
            return await _UserRepository.GetUser(UserName);
        }

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
