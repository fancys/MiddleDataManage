using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.B1
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 17:51:32
	===============================================================================================================================*/
    public class SAPBOneCommon
    {
        public static BoYesNoEnum GetEnumYesNo(string YesOrNo)
        {
            switch (YesOrNo)
            {
                case "Y": return BoYesNoEnum.tYES;
                case "N": return BoYesNoEnum.tNO;
                default: throw new Exception("InActive值不正确");
            }
        }
    }
}
