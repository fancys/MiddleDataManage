using IntegratedManagement.Entity.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.Help
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/10/16 17:39:20
	===============================================================================================================================*/
    public interface ISerialNumberApp
    {
        Task<SerialNumber> GetSerialNumberOfDateTimeNow();

        Task<bool> UpdateCurrentNumber(SerialNumber serialNumber);
    }
}
