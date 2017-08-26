using IntegratedManagement.Core.DataConvertEx;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManageMent.Application.SalesModule;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon.MergeSalesOrder;
using MagicBox.Log;
using MagicBox.WindowsServices.Common;
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
	*	Create by Fancy at 2017/4/7 17:33:33
    *	销售订单生成日记账分录  业务类型为：CS-代销销售、SP-代销买断、AR-CMS充值 AND 订单类型为：S01
	===============================================================================================================================*/
    public class JE_SalesOrder:IWindowsService
    {
        private readonly ISalesOrderApp _salesOrderApp ;
        SalesOrderService _soService;

        public JE_SalesOrder(ISalesOrderApp ISalesOrderApp)
        {
            _salesOrderApp = ISalesOrderApp;
            _soService = new SalesOrderService(_salesOrderApp);

        }
        public void Run()
        {
            HandleJE_SalesOrder();
        }

        public void Stop()
        {
            
        }
        public  async  Task<List<SalesOrder>> GetJE_SalesOrderList()
        {
            QueryParam queryParam = new QueryParam();
             
            //条件 未同步 未删除
            queryParam.filter = "(IsJESync eq 'N')   and (DocType eq 'S01') and (BusinessType in ('CS','SP','AR-CMS')) and (IsDelete eq 'N')";
            queryParam.limit = DataConvertEx.TryConvertParse(ConfigurationManager.AppSettings["MergeCount"], 300);

            return await _salesOrderApp.GetSalesOrderAsync(queryParam);
        }

        public async void HandleJE_SalesOrder()
        {
            while(true)
            {
                //获取未生成分录的销售订单
                var salesOrderList = await GetJE_SalesOrderList();
                if (salesOrderList.Count == 0) break;
                await HandleMidDocument(salesOrderList);
                Thread.Sleep(3000);
            }
            
           
        }

        public async Task HandleMidDocument(List<SalesOrder> salesOrderList)
        {
            string guid = "SalesOrder_" + Guid.NewGuid();
            Logger.Writer(guid, QueueStatus.Open, $"已获取销售订单{salesOrderList.Count}条，正在处理。。");
            //按条件合并订单
            var newSalesOrderList = _soService.MergeSalesOrder(_soService.PackingSalesOrder(salesOrderList));
            Logger.Writer(guid, QueueStatus.Open, $"已合并成[{newSalesOrderList.Count}[条销售订单");
            foreach (var item in newSalesOrderList)
            {
                //获取合并订单的DocEntry，批量处理订单的生成状态
                StringBuilder str = new StringBuilder();
                item.SalesOrders.ForEach(c => { str.Append("'"); str.Append(c.DocEntry); str.Append("',"); });
                var rt = CreateJournalEntry(item);//销售订单生成分录
                //处理订单生成的结果
                await _soService.HandleSalesOrderResult(rt, str.ToString().Trim(','), false);
            }
            Logger.Writer(guid, QueueStatus.Close, "销售订单已处理完成。");
        }

        public static  Result CreateJournalEntry(MergeSalesOrder salesOrder)
        {
            Result result = new Result();
            SAPbobsCOM.JournalEntries myJE = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
            myJE.ReferenceDate = salesOrder.OMSDocDate;
            myJE.UserFields.Fields.Item("U_DocType").Value = salesOrder.DocType;
            myJE.UserFields.Fields.Item("U_BusinessType").Value = salesOrder.BusinessType;
            myJE.UserFields.Fields.Item("U_PlatformCode").Value = salesOrder.PlatformCode;
            myJE.UserFields.Fields.Item("U_CardCode").Value = salesOrder.CardCode;
            foreach (var item in salesOrder.MergeSalesOrderItems)
            {
                Tuple<string, string> Accout = BOneCommon.GetAccountByPayMethod(salesOrder.PayMethod);
                myJE.Lines.AccountCode = Accout.Item1;
                myJE.Lines.ShortName = Accout.Item2;
                myJE.Lines.Debit = Convert.ToDouble(salesOrder.OrderPaied);
                myJE.Lines.UserFields.Fields.Item("U_PayMethod").Value = salesOrder.PayMethod;
                myJE.Lines.Add();
                
                if(salesOrder.Freight != 0)
                {
                    myJE.Lines.AccountCode = Accout.Item1;
                    myJE.Lines.ShortName = Accout.Item2;
                    myJE.Lines.Debit = Convert.ToDouble(salesOrder.Freight);
                    myJE.Lines.UserFields.Fields.Item("U_PayMethod").Value = salesOrder.PayMethod;
                    myJE.Lines.Add();

                    myJE.Lines.AccountCode = "224101";
                    myJE.Lines.ShortName = "224101";
                    myJE.Lines.Credit = Convert.ToDouble(salesOrder.Freight);
                    myJE.Lines.Add();
                }
               

                myJE.Lines.AccountCode = GetAccountCode(salesOrder.BusinessType, item.ItemCode);
                myJE.Lines.ShortName = GetShortName(salesOrder.BusinessType, item.ItemCode, salesOrder.CardCode);
                myJE.Lines.Credit = Convert.ToDouble( salesOrder.OrderPaied);
                myJE.Lines.UserFields.Fields.Item("U_ItemCode").Value = item.ItemCode;
                myJE.Lines.UserFields.Fields.Item("U_Quantity").Value = Convert.ToDouble( item.Quantity);
                myJE.Lines.UserFields.Fields.Item("U_RefDate").Value = salesOrder.OMSDocDate;
                myJE.Lines.Add();
                
                
            }

            int rtCode = myJE.Add();
            if (rtCode != 0)
            {
                result.ResultCode = SAP.SAPCompany.GetLastErrorCode();
                result.Message = SAP.SAPCompany.GetLastErrorDescription();

            }
            else
            {
                result.ResultCode = 0;
                result.ObjCode = SAP.SAPCompany.GetNewObjectKey();
                result.Message = "Create JournalEntry Successfully.";
            }
            return result;
        }

        private static string GetAccountCode(string businessType, string itemCode)
        {
            switch (businessType)
            {
                case "TS":
                case "AS":
                    return "112201";
                case "CS":
                case "SP":
                case "AR":
                    return BOneCommon.GetAccountByItemCode(itemCode);
                default:
                    throw new Exception($"业务类型[{businessType}]错误，无法处理。");

            }
        }

        private static string GetShortName(string businessType, string itemCode, string cardCode)
        {
            switch (businessType)
            {
                case "TS":
                case "AS":
                    return cardCode;
                case "CS":
                case "SP":
                    return BOneCommon.GetShortNameByItemCode(itemCode);
                case "AR":
                    return BOneCommon.GetAccountByItemCode(itemCode);
                default:
                    throw new Exception($"业务类型[{businessType}]错误，无法处理。");

            }
        }
    }
}
