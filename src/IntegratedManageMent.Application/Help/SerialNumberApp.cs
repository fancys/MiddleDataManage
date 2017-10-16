using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Help;
using IntegratedManagement.IRepository.Help;

namespace IntegratedManageMent.Application.Help
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/10/16 17:39:03
	===============================================================================================================================*/
    public class SerialNumberApp : ISerialNumberApp
    {
        private readonly ISerialNumberRepository _serialNumberRepository;
        public SerialNumberApp(ISerialNumberRepository ISerialNumberRepository)
        {
            this._serialNumberRepository = ISerialNumberRepository;
        }
        public async Task<SerialNumber> GetSerialNumberOfDateTimeNow()
        {
            SerialNumber serialNumber = await _serialNumberRepository.GetSerialNumber();
            if(serialNumber != null)
            {
                serialNumber = new SerialNumber();
                serialNumber.Year = DateTime.Now.Year;
                serialNumber.Month = DateTime.Now.Month;
                serialNumber.CurrentNumber = 0;
                var rt = await _serialNumberRepository.Save(serialNumber);
                if (rt.Code != 0)
                    throw new Exception(rt.Message);
            }
            return serialNumber;
        }

        public async Task<bool> UpdateCurrentNumber(SerialNumber serialNumber)
        {
            return await _serialNumberRepository.Update(serialNumber);
        }
    }
}
