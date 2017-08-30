using IntegratedManagement.Entity.BusinessPartnerModule.BusinessPlaces;
using IntegratedManagement.Entity.BusinessPartnerModule.CashFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.B1
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 17:13:34
	===============================================================================================================================*/
    public class BusinessPlacesService
    {
        public static void AddOrUpdateBusinessPlaces(BusinessPlaces BusinessPlaces)
        {
            try
            {
                SAPbobsCOM.BusinessPlaces oBP = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPlaces);
                if(oBP.GetByKey(BusinessPlaces.BPLId))
                {
                    oBP.BPLName = BusinessPlaces.BPLName;
                    oBP.BPLNameForeign = BusinessPlaces.TaxIdNum;
                    oBP.RepName = BusinessPlaces.RepName;
                    oBP.Industry = BusinessPlaces.Industry;
                    oBP.Business = BusinessPlaces.Business;
                    oBP.Address = BusinessPlaces.Address;
                    oBP.MainBPL = SAPBOneCommon.GetEnumYesNo(BusinessPlaces.MainBPL);
                    oBP.TaxOfficeNo = BusinessPlaces.TxOffcNo;
                    oBP.Disabled = SAPBOneCommon.GetEnumYesNo(BusinessPlaces.Disabled);
                    oBP.DefaultCustomerID = BusinessPlaces.DflCust;
                    oBP.DefaultVendorID = BusinessPlaces.DflVendor;
                    oBP.DefaultWarehouseID = BusinessPlaces.DflWhs;
                    oBP.DefaultResourceWarehouseID = BusinessPlaces.DfltResWhs;
                    oBP.AliasName = BusinessPlaces.AliasName;
                    oBP.AddressType = BusinessPlaces.AddrType;
                    oBP.Block = BusinessPlaces.Block;
                    oBP.City = BusinessPlaces.City;
                    oBP.Street = BusinessPlaces.Street;
                    oBP.StreetNo = BusinessPlaces.StreetNo;
                    oBP.Building = BusinessPlaces.Building;
                    oBP.ZipCode = BusinessPlaces.ZipCode;
                    oBP.Country = BusinessPlaces.Country;
                    oBP.County = BusinessPlaces.County;
                    oBP.PaymentClearingAccount = BusinessPlaces.PmtClrAct;
                    oBP.GlobalLocationNumber = BusinessPlaces.GlblLocNum;
                    int rt = oBP.Update();
                    if(rt!=0)
                    {
                        //failed
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
