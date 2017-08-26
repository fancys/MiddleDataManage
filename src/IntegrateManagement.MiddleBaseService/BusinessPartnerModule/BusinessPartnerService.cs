using IntegratedManagement.Core.DataConvertEx;
using IntegratedManagement.Entity.BusinessPartnerModule.BusinessPartner;
using IntegratedManagement.Entity.Param;
using IntegratedManageMent.Application.BusinessPartnerModule;
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
	*	Create by Fancy at 2017/3/31 16:08:43
	===============================================================================================================================*/
    public class BusinessPartnerService:IWindowsService
    {
        private readonly IBusinessPartnerApp customerApp;
        public BusinessPartnerService(IBusinessPartnerApp IBusinessPartnerApp)
        {
            customerApp = IBusinessPartnerApp;
        }

        public void Run()
        {
            HandCustomer();
        }

        public void Stop()
        {
            
        }
        public async void HandCustomer()
        {
            QueryParam queryParam = new QueryParam();
            

            queryParam.filter = "(IsSync eq 'N') and (IsDelete eq 'N')";
            queryParam.limit = 20;

            var customerList = await customerApp.GetBusinessPartnerList(queryParam);
            if (customerList.Count == 0)
                return;
            string guid = "Customer_" + Guid.NewGuid().ToString();
            Logger.Writer(guid, QueueStatus.Open, $"已获取[{customerList.Count}]条业务伙伴，正在处理...");
            int successfulCount = 0;
            foreach (var item in customerList)
            {
                try
                {
                    var rt = CreateOrUpdateCustomer(item);
                    if(rt.ResultCode==0)
                        await customerApp.UpdateSyncData(item.CardCode);
                    successfulCount++;
                }
                catch (Exception ex)
                {
                    Logger.Writer(guid, QueueStatus.Open, $"业务伙伴【{item.CardCode}】处理出现异常：{ex.Message}");
                }

            }
            Logger.Writer(guid, QueueStatus.Close, $"[{successfulCount}]条业务伙伴处理成功。");
        }


        public Result CreateOrUpdateCustomer(BusinessPartner businessPartner)
        {
            Result result = new Result();
            SAPbobsCOM.BusinessPartners myBusinessPartner = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

            bool isExist = myBusinessPartner.GetByKey(businessPartner.CardCode);
            myBusinessPartner.CardCode = businessPartner.CardCode;
            myBusinessPartner.CardName = businessPartner.CardName;
            myBusinessPartner.UserFields.Fields.Item("U_PlatformCode").Value = businessPartner.PlatformCode;
            myBusinessPartner.CardType = DataConvertEx.GetCardTypeByPlatformCode(businessPartner.PlatformCode.Substring(0, 1));
            myBusinessPartner.GroupCode = BOneCommon.GetCustomerGroupCodeByPlateformCode(businessPartner.PlatformCode);
            int rtCode = 0;
            if (isExist)
                rtCode = myBusinessPartner.Update();
            else
                rtCode = myBusinessPartner.Add();

            if(rtCode !=0)
            {
                result.ResultCode = -1;
                result.ObjCode = businessPartner.CardCode;
                result.Message = SAP.SAPCompany.GetLastErrorDescription();
            }
            else
            {
                result.ResultCode = 0;
                result.ObjCode = businessPartner.CardCode;
                result.Message = "Updated or saved customer successfully.";
            }
            return result;

        }

       
    }
}
