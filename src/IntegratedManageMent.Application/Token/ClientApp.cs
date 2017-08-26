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
	*	Create by Fancy at 2017/3/22 17:32:36
	===============================================================================================================================*/
    public class ClientApp
    {
        IClientRepository _clientRepository = new ClientDapperRepository();

        public async Task<Client> Get(string clientId)
        {
            return await _clientRepository.FindById(clientId);
        }

        public async Task<bool> Save(Client client)
        {
            return await _clientRepository.Save(client);
        }

    }
}
