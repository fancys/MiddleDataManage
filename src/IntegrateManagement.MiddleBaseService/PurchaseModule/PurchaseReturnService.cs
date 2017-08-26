using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseReturn;
using IntegratedManagement.Entity.Result;
using IntegratedManageMent.Application.PurchaseModule;
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
	*	Create by Fancy at 2017/3/31 15:56:16
	===============================================================================================================================*/
    public class PurchaseReturnService : IWindowsService
    {
        private readonly IPurchaseReturnApp _purchaseReturnApp ;

        public PurchaseReturnService(IPurchaseReturnApp IPurchaseReturnApp)
        {
            _purchaseReturnApp = IPurchaseReturnApp;
        }

        public void Run()
        {
             PurchaseReturnHandle();
        }

        public void Stop()
        {
            
        }

        public async void PurchaseReturnHandle()
        {
            QueryParam queryParam = new QueryParam();

            queryParam.filter = "(IsSync eq 'N') and (IsDelete eq 'N')";
            queryParam.limit = 20;
            var orderList = await _purchaseReturnApp.GetPurchaseReturnAsync(queryParam);
            if (orderList.Count == 0)
                return;
            string guid = "PurchaseReturn_" + Guid.NewGuid().ToString();
            Logger.Writer(guid, QueueStatus.Open, $"已获取[{orderList.Count}]条采购退货/出库单，正在处理...");
            Result rt = new Result();
            foreach (var item in orderList)
            {
                try
                {
                    if (item.BusinessType == "001")
                        rt = CreatePurchaseReturn(item);
                    else
                        rt = CreateGoodsIssues(item);
                    
                  await HandleResult(rt,item.DocEntry);
                    
                }
                catch (Exception ex)
                {
                    Logger.Writer(guid, QueueStatus.Open, $"采购退货/出库单【{item.OMSDocEntry}】处理出现异常：{ex.Message}");
                }

            }
            Logger.Writer(guid, QueueStatus.Close, "采购退货/出库单处理成功。");
        }


        /// <summary>
        /// 生成采购退货单
        /// </summary>
        /// <param name="purchaseReturnOrder"></param>
        /// <returns></returns>
        private Result CreatePurchaseReturn(PurchaseReturn purchaseReturnOrder)
        {
            int DocEntry = default(int);
            if (BOneCommon.IsExistDocument("ORPD", purchaseReturnOrder.OMSDocEntry, out DocEntry))
                return new Result() { ResultCode = 0, ObjCode = DocEntry.ToString(), Message = "has been successfully created documents." };
            Result result = new Result();
            SAPbobsCOM.Documents myDocument = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseReturns);
            myDocument.CardCode = purchaseReturnOrder.CardCode;
            myDocument.DocDate = purchaseReturnOrder.OMSDocDate;
            myDocument.UserFields.Fields.Item("U_OMSDocEntry").Value = purchaseReturnOrder.OMSDocEntry;
            myDocument.UserFields.Fields.Item("U_BatchNum").Value = purchaseReturnOrder.BatchNum;
            myDocument.Comments = purchaseReturnOrder.Comments;
            foreach (var item in purchaseReturnOrder.PurchaseReturnItems)
            {
                myDocument.Lines.UserFields.Fields.Item("U_OMSDocEntry").Value = item.OMSDocEntry;
                myDocument.Lines.UserFields.Fields.Item("U_OMSLineNum").Value = item.OMSLineNum;
                myDocument.Lines.ItemCode = item.ItemCode;
                myDocument.Lines.Quantity = Convert.ToDouble(item.Quantity);
                myDocument.Lines.PriceAfterVAT = Convert.ToDouble(item.Price);
                myDocument.Lines.VatGroup = BOneCommon.GetVatGroupbyItemCode(item.ItemCode).Item1;
                myDocument.Lines.Add();
            }
            int rt = myDocument.Add();
            if (rt != 0)
            {
                result.ResultCode = -1;
                result.Message = SAP.SAPCompany.GetLastErrorDescription();
            }
            else
            {
                result.ResultCode = 0;
                result.ObjCode = SAP.SAPCompany.GetNewObjectKey();
                result.Message = "create purchaseReturnOrder successful.";
            }
            return result;
        }

        /// <summary>
        /// 生成库存发货
        /// </summary>
        /// <param name="purchaseReturnOrder"></param>
        /// <returns></returns>
        private Result CreateGoodsIssues(PurchaseReturn purchaseReturnOrder)
        {
            int DocEntry = default(int);
            if (BOneCommon.IsExistDocument("OIGE", purchaseReturnOrder.OMSDocEntry, out DocEntry))
                return new Result() { ResultCode = 0, ObjCode = DocEntry.ToString(), Message = "has been successfully created document." };
            Result result = new Result();
            SAPbobsCOM.Documents myDocument = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenExit);
            myDocument.CardCode = purchaseReturnOrder.CardCode;
            myDocument.DocDate = purchaseReturnOrder.OMSDocDate;
            myDocument.UserFields.Fields.Item("U_OMSDocEntry").Value = purchaseReturnOrder.OMSDocEntry;
            myDocument.UserFields.Fields.Item("U_BatchNum").Value = purchaseReturnOrder.BatchNum;
            myDocument.UserFields.Fields.Item("U_InvnTrnsType").Value = purchaseReturnOrder.BusinessType;
            myDocument.Comments = purchaseReturnOrder.Comments;
            foreach (var item in purchaseReturnOrder.PurchaseReturnItems)
            {
                myDocument.Lines.UserFields.Fields.Item("U_OMSDocEntry").Value = item.OMSDocEntry;
                myDocument.Lines.UserFields.Fields.Item("U_OMSLineNum").Value = item.OMSLineNum;
                myDocument.Lines.ItemCode = item.ItemCode;
                myDocument.Lines.Quantity = Convert.ToDouble(item.Quantity);
                myDocument.Lines.PriceAfterVAT = Convert.ToDouble(item.Price);
                myDocument.Lines.AccountCode = BOneCommon.GetAccCodeByBusinessType(purchaseReturnOrder.BusinessType);
                myDocument.Lines.Add();
            }
            int rt = myDocument.Add();
            if (rt != 0)
            {
                result.ResultCode = -1;
                result.Message = SAP.SAPCompany.GetLastErrorDescription();
            }
            else
            {
                result.ResultCode = 0;
                result.ObjCode = SAP.SAPCompany.GetNewObjectKey();
                result.Message = "create document successful.";
            }
            return result;
        }

        private async Task HandleResult(Result rt,int DocEntry)
        {
            DocumentSync documentResult = new DocumentSync();

            if (rt.ResultCode == 0)
                documentResult.SyncResult = "Y";
            else
                documentResult.SyncResult = "N";
            documentResult.SyncMsg = rt.Message;
            documentResult.SAPDocEntry = rt.ObjCode;
            documentResult.DocEntry = DocEntry.ToString();
            await _purchaseReturnApp.UpdateSyncDataAsync(documentResult);
        }
    }
}
