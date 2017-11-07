using IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner;
using IntegratedManagement.Entity.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.B1
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/7 20:59:39
	===============================================================================================================================*/
    public class BusinessPartnerSerivice
    {
        public static DocumentSync AddOrUpdateBusinessPartner(BusinessPartner BusinessPartner)
        {
            DocumentSync rt = new DocumentSync();
            SAPbobsCOM.BusinessPartners oBP = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
            oBP.CardCode = BusinessPartner.CardCode;
            oBP.CardName = BusinessPartner.CardName;
            int rtCode = 1;
            if (oBP.GetByKey(BusinessPartner.CardCode))
                rtCode = oBP.Update();
            else
                rtCode = oBP.Add();
            if (rtCode != 0)
            {
                rt.SyncResult = "N";
                rt.SyncMsg = SAP.SAPCompany.GetLastErrorDescription();
            }
            else
            {
                rt.SyncResult = "Y";
                rt.SyncMsg = "sync successful";
            }
            return rt;
        }
    }
}
