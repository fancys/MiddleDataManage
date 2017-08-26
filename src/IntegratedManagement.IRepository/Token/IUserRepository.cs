using IntegratedManagement.Entity.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/24 9:57:04
	===============================================================================================================================*/
    public interface IUserRepository
    {
        Task<User> GetUser(string UserName, string Password);

        Task<bool> Insert(User User);
    }
}
