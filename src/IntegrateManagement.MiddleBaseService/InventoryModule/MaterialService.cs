using IntegratedManagement.Core.DataConvertEx;
using IntegratedManagement.Entity.InventoryModule.Material;
using IntegratedManagement.Entity.Param;
using IntegratedManageMent.Application.InventoryModule;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon;
using MagicBox.Log;
using MagicBox.WindowsServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/31 15:41:07
	===============================================================================================================================*/
    public class MaterialService:IWindowsService
    {
        private readonly  IMaterialApp materialApp;

        public MaterialService(IMaterialApp IMaterialApp)
        {
            materialApp = IMaterialApp;
        }

        public void Run()
        {
            HandMaterial();
        }

        public void Stop()
        {

        }
        public async void HandMaterial()
        {
            QueryParam queryParam = new QueryParam();

            queryParam.filter = "(IsSync eq 'N') and (IsDelete eq 'N')";
            queryParam.limit = 20;

            var materialList = await materialApp.GetMaterialAsync(queryParam);
            if (materialList.Count == 0)
                return;
            string guid = "Material_" + Guid.NewGuid().ToString();
            Logger.Writer(guid, QueueStatus.Open, $"已获取[{materialList.Count}]条物料，正在处理...");
            int successfulCount = 0;
            foreach(var item in materialList)
            {
                try
                {
                    var rt = CreateOrUpdateMaterial(item);
                    if (rt.ResultCode == 0)
                        await materialApp.UpdateSyncDataAsync(item.ItemCode);
                    successfulCount++;
                }catch(Exception ex)
                {
                    Logger.Writer(guid, QueueStatus.Open, $"物料【{item.ItemCode}】处理出现异常：{ex.Message}");
                }

            }
            Logger.Writer(guid, QueueStatus.Close, $"[{successfulCount}]条物料处理成功。");
        }

        public Result CreateOrUpdateMaterial(Material material)
        {
            Result result = new Result();
            SAPbobsCOM.Items myItem = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);

            ///检查物料是否创建
            bool IsExist = myItem.GetByKey(material.ItemCode);
            myItem.ItemCode = material.ItemCode;
            myItem.ItemName = material.ItemName;
            myItem.SalesVATGroup = material.VatGourpSa;
            myItem.PurchaseVATGroup = material.VatGourpPu;
            myItem.UserFields.Fields.Item("U_InitialCost").Value = Convert.ToDouble(material.InitialCost);
            myItem.UserFields.Fields.Item("U_RealCost").Value = Convert.ToDouble(material.InitialCost);
            myItem.UserFields.Fields.Item("U_SalesPrice").Value = Convert.ToDouble(material.SalesPrice);
            myItem.InventoryItem = DataConvertEx.GetSAPValue(material.InvntItem);
            myItem.UserFields.Fields.Item("U_Consignment").Value = material.Consignment;
            myItem.UserFields.Fields.Item("U_Vendor").Value = material.Vendor;
            //myItem.PrchseItem = 'Y';
            //myItem.SellItem = 'Y';
            myItem.ItemsGroupCode = BOneCommon.GetItemGroupCodeByOMSGroupNum(material.OMSGroupNum);

            int ResultCode = 0;
            if (IsExist)
                ResultCode = myItem.Update();
            else
                ResultCode = myItem.Add();

            if(ResultCode != 0)
            {
                result.ResultCode = -1;
                result.ObjCode = material.ItemCode;
                result.Message = SAP.SAPCompany.GetLastErrorDescription();
            }
            else
            {
                result.ResultCode = 0;
                result.ObjCode = material.ItemCode;
                result.Message = "Saved or Updated successfully.";
            }
            return result;
        }

      
    }
}
