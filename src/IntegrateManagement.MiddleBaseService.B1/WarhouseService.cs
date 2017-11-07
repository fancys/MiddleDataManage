using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.InventoryModule.Warehouse;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.B1
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 16:49:52
    *	仓库同步
	===============================================================================================================================*/
    public class WarhouseService
    {
        public static DocumentSync AddOrUpdateWarehouse(Warehouse Warehouse)
        {
            DocumentSync rt = new DocumentSync();
            SAPbobsCOM.Warehouses oWH = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oWarehouses);
            oWH.WarehouseCode = Warehouse.WhsCode;
            oWH.WarehouseName = Warehouse.WhsName;
            oWH.BusinessPlaceID = Warehouse.BPLid;
            oWH.Inactive = SAPBOneCommon.GetEnumYesNo(Warehouse.Inactive);
            oWH.City = Warehouse.City;
            oWH.State = Warehouse.State;
            oWH.UserFields.Fields.Item("U_ContractPerson").Value = Warehouse.ContractPerson;
            oWH.UserFields.Fields.Item("U_TelephoneNum").Value = Warehouse.TelephoneNum;
            oWH.UserFields.Fields.Item("U_WhsClass").Value = Warehouse.WhsClass;
            oWH.UserFields.Fields.Item("U_WhsType").Value = Warehouse.WhsType;
            int rtCode = 1;
            if (oWH.GetByKey(Warehouse.WhsCode))
                rtCode = oWH.Update();
            else
                rtCode = oWH.Add();
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
