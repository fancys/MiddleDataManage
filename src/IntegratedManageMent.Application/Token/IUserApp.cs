using IntegratedManagement.Entity.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/27 17:41:57
	===============================================================================================================================*/
    public interface IUserApp
    {
        Task<User> GetUser(string UserName, string Password);

        Task<User> GetUser(string UserName);

        Task<bool> Save(User user);

        Task<User> CheckUser(string UserName, string Password);
    }
}
