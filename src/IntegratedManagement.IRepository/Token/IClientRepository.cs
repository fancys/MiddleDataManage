using IntegratedManagement.Entity.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.Token
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/22 17:38:02
	===============================================================================================================================*/
    public interface IClientRepository
    {
        Task<Client> FindById(string Id);

        Task<bool> Save(Client client);
    }
}
