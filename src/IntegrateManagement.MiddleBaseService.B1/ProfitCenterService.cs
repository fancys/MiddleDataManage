using IntegratedManagement.Entity.BusinessPartnerModule.ProfitCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.B1
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 15:44:38
	===============================================================================================================================*/
    public class ProfitCenterService
    {
        /// <summary>
        /// 添加或更新成本中心
        /// </summary>
        /// <param name="ProfitCenter"></param>
        public static void AddOrUpdateProfitCenter(ProfitCenters ProfitCenter)
        {
            try
            {
                var oCmpSrv = SAP.SAPCompany.GetCompanyService();
                SAPbobsCOM.ProfitCentersService oPCService;
                SAPbobsCOM.ProfitCenter oPC;
                SAPbobsCOM.ProfitCenterParams oPCParams;
                
                oPCService = oCmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.ProfitCentersService);
                oPCParams = oPCService.GetDataInterface(SAPbobsCOM.ProfitCentersServiceDataInterfaces.pcsProfitCenterParams);
                oPC = oPCService.GetDataInterface(SAPbobsCOM.ProfitCentersServiceDataInterfaces.pcsProfitCenter);
                oPCParams.CenterCode = ProfitCenter.PrcCode;

                oPC = oPCService.GetProfitCenter(oPCParams);
                oPC.CenterCode = ProfitCenter.PrcCode;
                oPC.CenterName = ProfitCenter.PrcName;
                oPC.UserFields.Item("U_ERPCode").Value = ProfitCenter.ERPCode;
                oPC.UserFields.Item("U_Type").Value = ProfitCenter.Type;
                oPC.InWhichDimension = ProfitCenter.DimCode;
                if (oPC == null || string.IsNullOrEmpty(oPC.CenterCode))
                    oPCParams = oPCService.AddProfitCenter(oPC);
                else
                    oPCService.UpdateProfitCenter(oPC);

            }
            catch(Exception ex)
            {
                throw ex;
            }
           
            
        }
    }
}
