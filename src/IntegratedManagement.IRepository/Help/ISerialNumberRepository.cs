using IntegratedManagement.Entity.Help;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.Help
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/10/16 17:36:56
	===============================================================================================================================*/
    public interface ISerialNumberRepository
    {
        Task<SerialNumber> GetSerialNumber();

        Task<SaveResult> Save(SerialNumber serialNumber);

        Task<bool> Update(SerialNumber serialNumber);
    }
}
