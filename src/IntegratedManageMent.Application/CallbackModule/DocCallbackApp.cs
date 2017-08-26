using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManageMent.Application.CallbackModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/29 17:08:37
	===============================================================================================================================*/
    public class DocCallbackApp
    {
        public async Task HandDocument(List<CallbackParam> CallbackParamList)
        {
            foreach (var item in CallbackParamList)
            {
                switch (item.ObjType)
                {
                    case 4:
                        await DocumentCallback();
                        break;
                    case 15:
                        await DocumentCallback();
                        break;
                    case 16:
                        await DocumentCallback();
                        break;
                    default:
                        continue;


                }
            }
        }

        private async Task DocumentCallback()
        {
             
        }
    }
}
