using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PurchaseModule.PurchaseOrder;
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
	*	Create by Fancy at 2017/3/31 15:56:00
	===============================================================================================================================*/
    public class PurchaseOrderService : IWindowsService
    {
        private readonly IPurchaseOrderApp _purchaseOrderApp ;

        public PurchaseOrderService(IPurchaseOrderApp IPurchaseOrderApp)
        {
            _purchaseOrderApp = IPurchaseOrderApp;
        }

        public  void Run()
        {
             PurchaseOrderHandle();
        }

        public void Stop()
        {
            
        }

        public async void PurchaseOrderHandle()
        {
            QueryParam queryParam = new QueryParam();
            

            queryParam.filter = "(IsSync eq 'N') and (IsDelete eq 'N')";
            queryParam.limit = 20;
            var orderList = await _purchaseOrderApp.GetPurchaseOrderAsync(queryParam);
            if (orderList.Count == 0)
                return;
            string guid = "PurchaseOrder_" + Guid.NewGuid().ToString();
            Logger.Writer(guid, QueueStatus.Open, $"已获取[{orderList.Count}]条采购订单，正在处理...");
            Result rt = new Result();
            foreach (var item in orderList)
            {
                try
                {
                    if (item.BusinessType == "I01")
                        rt = CreatePurchaseOrder(item);
                    else
                        rt = CreateGoodsReceipts(item);

                    await UpdateDocument(rt, item.DocEntry);
                }
                catch (Exception ex)
                {
                    Logger.Writer(guid, QueueStatus.Open, $"采购订单【{item.OMSDocEntry}】处理出现异常：{ex.Message}");
                }

            }
            Logger.Writer(guid, QueueStatus.Close, "采购订单处理成功。");
        }

        /// <summary>
        /// 生成采购收货单
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        private Result CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            int DocEntry = default(int);
            if (BOneCommon.IsExistDocument("OPDN", purchaseOrder.OMSDocEntry,out DocEntry))
                return new Result() { ResultCode = 0, ObjCode = DocEntry.ToString(), Message = "has been successfully created documents." };
            Result result = new Result();
            SAPbobsCOM.Documents myDocument = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes);
            myDocument.CardCode = purchaseOrder.CardCode;
            myDocument.DocDate = purchaseOrder.OMSDocDate;
            
            myDocument.UserFields.Fields.Item("U_OMSDocEntry").Value = purchaseOrder.OMSDocEntry;
            myDocument.UserFields.Fields.Item("U_BatchNum").Value = purchaseOrder.BatchNum;
            myDocument.Comments = purchaseOrder.Comments;
            foreach (var item in purchaseOrder.PurchaseOrderItems)
            {
                myDocument.Lines.UserFields.Fields.Item("U_OMSDocEntry").Value = item.OMSDocEntry;
                myDocument.Lines.UserFields.Fields.Item("U_OMSLineNum").Value = item.OMSLineNum;
                myDocument.Lines.ItemCode = item.ItemCode;
                myDocument.Lines.Quantity = Convert.ToDouble( item.Quantity);
                myDocument.Lines.PriceAfterVAT = Convert.ToDouble( item.Price);
                var vatGroup = BOneCommon.GetVatGroupbyItemCode(item.ItemCode);
                myDocument.Lines.VatGroup = vatGroup.Item1;
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
                result.ObjCode = SAP.SAPCompany.GetNewObjectKey() ;
                result.Message = "create purchaseOrder successful.";
            }
            return result;
        }

        /// <summary>
        /// 生成库存收货
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        private Result CreateGoodsReceipts(PurchaseOrder purchaseOrder)
        {
            int DocEntry = default(int);
            if (BOneCommon.IsExistDocument("OIGN", purchaseOrder.OMSDocEntry, out DocEntry))
                return new Result() { ResultCode = 0, ObjCode = DocEntry.ToString(), Message = "has been successfully created document." };
            Result result = new Result();
            SAPbobsCOM.Documents myDocument = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry);
            myDocument.CardCode = purchaseOrder.CardCode;
            myDocument.DocDate = purchaseOrder.OMSDocDate;
            myDocument.UserFields.Fields.Item("U_OMSDocEntry").Value = purchaseOrder.OMSDocEntry;
            myDocument.UserFields.Fields.Item("U_BatchNum").Value = purchaseOrder.BatchNum;
            myDocument.UserFields.Fields.Item("U_InvnTrnsType").Value = purchaseOrder.BusinessType;
            myDocument.Comments = purchaseOrder.Comments;
            foreach (var item in purchaseOrder.PurchaseOrderItems)
            {
                myDocument.Lines.UserFields.Fields.Item("U_OMSDocEntry").Value = item.OMSDocEntry;
                myDocument.Lines.UserFields.Fields.Item("U_OMSLineNum").Value = item.OMSLineNum;
                myDocument.Lines.ItemCode = item.ItemCode;
                myDocument.Lines.Quantity = Convert.ToDouble(item.Quantity);
                myDocument.Lines.PriceAfterVAT = Convert.ToDouble(item.Price);
                myDocument.Lines.AccountCode = BOneCommon.GetAccCodeByBusinessType(purchaseOrder.BusinessType);
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

        private async Task UpdateDocument(Result rt,int DocEntry)
        {
            DocumentSync documentResult = new DocumentSync();

            if (rt.ResultCode == 0)
                documentResult.SyncResult = "Y";
            else
                documentResult.SyncResult = "N";
            documentResult.SyncMsg = rt.Message;
            documentResult.SAPDocEntry = rt.ObjCode;
            documentResult.DocEntry = DocEntry.ToString();
            documentResult.SyncDate = DateTime.Now;
            await _purchaseOrderApp.UpdateSyncDataAsync(documentResult);
        }
    }
}
