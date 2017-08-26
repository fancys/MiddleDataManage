using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManageMent.Application.SalesModule;
using IntegrateManagement.MiddleBaseService.SalesModule;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon.MergeSalesOrder;
using MagicBox.WindowsServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/31 16:09:04
	===============================================================================================================================*/
    public class SalesOrderService : IWindowsService
    {
        private readonly ISalesOrderApp _salesOrderApp ;
        JE_SalesOrder jes;
        IN_SalesOrder ins;
        JE_IN_SalesOrder je_ins;

        public SalesOrderService(ISalesOrderApp ISalesOrderApp)
        {
            _salesOrderApp = ISalesOrderApp;
            jes = new JE_SalesOrder(_salesOrderApp);
            ins = new IN_SalesOrder(_salesOrderApp);
            je_ins = new JE_IN_SalesOrder(_salesOrderApp);
        }
        /*======================================================||
        ||                 |     销售订单    |     退货单       ||
        ||------------------------------------------------------||
        || CS-代销销售     |                 |                  ||
        || SP-代销买断     |  日记账分录     |       无         ||
        || AR-CMS充值      |                 |                  ||
        ||------------------------------------------------------||
        || OS-线下销售     |  形式应收发票   |  形式应收发票    ||
        ||------------------------------------------------------||
        || TS-经销销售     |  形式应收发票   |  形式应收发票    ||
        || AS-CMS销售      |  日记账分录     |                  ||
        ||======================================================*/
        public void Run()
        {
            

          

            //只生成分录
            jes.HandleJE_SalesOrder();
            //只生成应收发票
            ins.HandleIn_SalesOrder();
            //生成分录和发票
            je_ins.HandleJE_IN_SalesOrder();
        }

        public void Stop()
        {
            
        }

        public  List<MergeSalesOrder> PackingSalesOrder(List<SalesOrder> salesOrderList)
        {
            List<MergeSalesOrder> mergeSalesOrderList = new List<MergeSalesOrder>();
            foreach (var item in salesOrderList)
            {
                MergeSalesOrder mergeSalesOrder = new MergeSalesOrder();
                mergeSalesOrder.DocEntry = item.DocEntry;
                mergeSalesOrder.BusinessType = item.BusinessType;
                mergeSalesOrder.CardCode = item.CardCode;
                mergeSalesOrder.DocType = item.DocType;
                mergeSalesOrder.Freight = item.Freight;
                mergeSalesOrder.OMSDocDate = item.OMSDocDate;
                mergeSalesOrder.PlatformCode = item.PlatformCode;
                mergeSalesOrder.PayMethod = item.PayMethod;
                mergeSalesOrder.OrderPaied = item.OrderPaied;
                mergeSalesOrder.SalesOrders.Add(new OriginSalesOrder() { DocEntry = item.DocEntry });
                foreach (var line in item.SalesOrderItems)
                {
                    MergeSalesOrderItem mergeSalesOrderItem = new MergeSalesOrderItem();
                    mergeSalesOrderItem.ItemCode = line.ItemCode;
                    mergeSalesOrderItem.Quantity = line.Quantity;
                    mergeSalesOrderItem.Price = line.Price;
                    mergeSalesOrderItem.ItemPaied = line.ItemPaied;
                    mergeSalesOrder.MergeSalesOrderItems.Add(mergeSalesOrderItem);
                }
                mergeSalesOrderList.Add(mergeSalesOrder);
            }
            return mergeSalesOrderList;
        }

        public List<MergeSalesOrder> MergeSalesOrder(List<MergeSalesOrder> salesOrderList)
        {
            List<MergeSalesOrder> newSalesOrderList = new List<MergeSalesOrder>();
            foreach (var item in salesOrderList)
            {
                if (newSalesOrderList.Where(x => x.OMSDocDate.Date == item.OMSDocDate.Date &&
                                            x.DocType == item.DocType &&
                                            x.CardCode == item.CardCode &&
                                            x.PlatformCode == item.PlatformCode &&
                                            x.PayMethod == item.PayMethod &&
                                            x.BusinessType == item.BusinessType).ToList().Count <= 0)
                    newSalesOrderList.Add(item);
                else
                {
                    var newRefund = newSalesOrderList.Where(x => x.OMSDocDate.Date == item.OMSDocDate.Date &&
                                            x.DocType == item.DocType &&
                                            x.CardCode == item.CardCode &&
                                            x.PlatformCode == item.PlatformCode &&
                                            x.PayMethod == item.PayMethod &&
                                            x.BusinessType == item.BusinessType ).ToList().FirstOrDefault();
                    newRefund.OrderPaied += item.OrderPaied;
                    newRefund.Freight += item.Freight;
                    newRefund.SalesOrders.Add(new OriginSalesOrder() { DocEntry = item.DocEntry });
                    foreach (var line in item.MergeSalesOrderItems)
                    {
                        if (newRefund.MergeSalesOrderItems.Where(c => c.ItemCode == line.ItemCode).ToList().Count <= 0)
                            newRefund.MergeSalesOrderItems.Add(line);
                        else
                        {
                            var tmpLine = newRefund.MergeSalesOrderItems.Where(c => c.ItemCode == line.ItemCode).ToList().FirstOrDefault();
                            tmpLine.Quantity += line.Quantity;
                            tmpLine.ItemPaied += line.ItemPaied;
                        }
                    }
                }

            }

            return newSalesOrderList;
        }


        public async Task HandleSalesOrderResult(Result rt, string DocEntryList,bool IsIN)
        {
            
            DocumentSync documentResult = new DocumentSync();

            if (rt.ResultCode == 0)
                documentResult.SyncResult = "Y";
            else
                documentResult.SyncResult = "N";
            documentResult.SyncMsg = rt.Message;
            documentResult.SAPDocEntry = rt.ObjCode;
            documentResult.DocEntry = DocEntryList;
            if (IsIN)
                await _salesOrderApp.UpdateINSyncDataBatchAsync(documentResult);
            else
                await _salesOrderApp.UpdateJESyncDataBatchAsync(documentResult);
        }
        
    }
}
