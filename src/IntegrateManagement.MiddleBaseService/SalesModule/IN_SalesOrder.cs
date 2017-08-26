using IntegratedManagement.Core.DataConvertEx;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManageMent.Application.SalesModule;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon.MergeSalesOrder;
using MagicBox.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.SalesModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/4/7 18:05:02
    *	销售订单生成应收发票  订单类型：销售单；业务类型：OS 线下销售 || 订单类型：退货单；业务类型：OS 线下销售、TS 经销销售、AS-CMS 销售
	===============================================================================================================================*/

    public  class IN_SalesOrder
    {
        private readonly ISalesOrderApp _salesOrderApp ;
        SalesOrderService soService;
        public IN_SalesOrder(ISalesOrderApp SalesOrderApp)
        {
            _salesOrderApp = SalesOrderApp;
            soService = new SalesOrderService(_salesOrderApp);
        }
       
        public async Task<List<SalesOrder>> GetIN_SalesOrderList()
        {
            QueryParam queryParam = new QueryParam();

            //条件 未同步 未删除 退货
            queryParam.filter = "(IsINSync eq 'N') and (((DocType eq 'S03') and (BusinessType in ('OS','TS','AS-CMS'))) or ((DocType eq 'S01') and (BusinessType eq 'OS'))) and (IsDelete eq 'N')";
            queryParam.limit = DataConvertEx.TryConvertParse(ConfigurationManager.AppSettings["MergeCount"], 300);//从配置文件中获取合并条数，没有默认为300

            return await _salesOrderApp.GetSalesOrderAsync(queryParam);
        }

        public async void HandleIn_SalesOrder()
        {
           while(true)
            {
                var salesOrderList = await GetIN_SalesOrderList();
                if (salesOrderList.Count <= 0) return;
                await HandleMidDocument(salesOrderList);
                Thread.Sleep(3000);
            }
        }

        public async Task HandleMidDocument(List<SalesOrder> salesOrderList)
        {
            string guid = "SalesOrder_" + Guid.NewGuid();
            Logger.Writer(guid, QueueStatus.Open, $"已获取销售订单{salesOrderList.Count}条，正在处理。。");
            var newSalesOrderList = soService.MergeSalesOrder(soService.PackingSalesOrder(salesOrderList));
            Logger.Writer(guid, QueueStatus.Open, $"已合并成[{newSalesOrderList.Count}[条销售订单");
            foreach (var item in newSalesOrderList)
            {
                StringBuilder str = new StringBuilder();
                try
                {
                    item.SalesOrders.ForEach(c => { str.Append("'"); str.Append(c.DocEntry); str.Append("',"); });
                    var rt = CreateInvoice(item);
                    await soService.HandleSalesOrderResult(rt, str.ToString().Trim(','), true);
                }
                catch (Exception ex)
                {
                    Logger.Writer(guid, QueueStatus.Open, $"销售订单[{item.DocEntry}]生成单据发生异常:{ex.Message}");
                    await soService.HandleSalesOrderResult(new Result() { ResultCode = -1, Message = ex.Message }, str.ToString().Trim(','), true);
                }

            }
            Logger.Writer(guid, QueueStatus.Close, "销售订单已处理完成。");
        }

        public static Result CreateInvoice(MergeSalesOrder salesOrder)
        {
            Result result = new Result();
            SAPbobsCOM.Documents myDocuments = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
            myDocuments.DocDate = salesOrder.OMSDocDate;
            myDocuments.DocDueDate = salesOrder.OMSDocDate;
            myDocuments.TaxDate = salesOrder.OMSDocDate;
            myDocuments.UserFields.Fields.Item("U_DocType").Value = salesOrder.DocType;
            myDocuments.UserFields.Fields.Item("U_BusinessType").Value = salesOrder.BusinessType;
            myDocuments.UserFields.Fields.Item("U_PlatformCode").Value = salesOrder.PlatformCode;
            myDocuments.CardCode = salesOrder.CardCode;
            
            foreach (var item in salesOrder.MergeSalesOrderItems)
            {
                myDocuments.Lines.ItemCode = item.ItemCode;
                if(salesOrder.DocType == "S01")
                    myDocuments.Lines.Quantity = Convert.ToDouble(item.Quantity);
                else
                    myDocuments.Lines.Quantity = -Convert.ToDouble(item.Quantity);

                myDocuments.Lines.VatGroup = BOneCommon.GetVatGroupbyItemCode(item.ItemCode).Item2;
                myDocuments.Lines.PriceAfterVAT = Convert.ToDouble(item.ItemPaied) / myDocuments.Lines.Quantity;

                myDocuments.Lines.Add();
            }

            int rtCode = myDocuments.Add();
            if(rtCode!=0)
            {
                result.ResultCode = -1;
                result.Message = SAP.SAPCompany.GetLastErrorDescription();

            }
            else
            {
                result.ResultCode = 0;
                result.ObjCode = SAP.SAPCompany.GetNewObjectKey();
                result.Message = "Create Invoice Successfully.";
            }


            return result;
        }
    }
}

