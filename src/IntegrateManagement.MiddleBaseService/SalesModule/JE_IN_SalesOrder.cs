using IntegratedManagement.Core.DataConvertEx;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.SalesModule.SalesOrder;
using IntegratedManageMent.Application.SalesModule;
using IntegrateManagement.MiddleBaseService.SalesModule;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon;
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
	*	Create by Fancy at 2017/4/10 9:41:40
    *	销售订单生成一张应收发票和日记账分录  订单类型：S01；业务类型：TS 经销销售、AS-CMS销售
	===============================================================================================================================*/
    public class JE_IN_SalesOrder
    {
        private readonly ISalesOrderApp _salesOrderApp;
        SalesOrderService soService;

        public JE_IN_SalesOrder(ISalesOrderApp ISalesOrderApp)
        {
            _salesOrderApp = ISalesOrderApp;
            soService = new SalesOrderService(_salesOrderApp);

        }
        public async Task<List<SalesOrder>> GetJE_IN_SalesOrderList()
        {
            QueryParam queryParam = new QueryParam();
            //条件 未同步 未删除 
            queryParam.filter = "((IsINSync eq 'N') or (IsJESync eq 'N'))  and (DocType eq 'S01') and (BusinessType in ('TS','AS-CMS')) and (IsDelete eq 'N')";
            queryParam.limit = DataConvertEx.TryConvertParse(ConfigurationManager.AppSettings["MergeCount"], 300);

            return await _salesOrderApp.GetSalesOrderAsync(queryParam);
        }

        public async void HandleJE_IN_SalesOrder()
        {
            while(true)
            {
                var salesOrderList = await GetJE_IN_SalesOrderList();
                if (salesOrderList.Count <= 0) break;
                await HandleMidDocument(salesOrderList);
                Thread.Sleep(3000);
            }
            
        }

        private async Task HandleMidDocument(List<SalesOrder> salesOrderList)
        {
            string guid = "SalesOrder_" + Guid.NewGuid();
            Logger.Writer(guid, QueueStatus.Open, $"已获取{salesOrderList.Count}条销售订单，正在处理。。");

            #region 应收发票
            var newINSalesOrderList = soService.MergeSalesOrder(soService.PackingSalesOrder(salesOrderList.Where(c => c.IsINSync == "N").ToList()));
            Logger.Writer(guid, QueueStatus.Open, $"已合并成[{newINSalesOrderList.Count}[条应生成发票的销售订单");
            foreach (var item in newINSalesOrderList)
            {
                StringBuilder str = new StringBuilder();
                try
                {
                    item.SalesOrders.ForEach(c => { str.Append("'"); str.Append(c.DocEntry); str.Append("',"); });
                    var rt = IN_SalesOrder.CreateInvoice(item);
                    await soService.HandleSalesOrderResult(rt, str.ToString().Trim(','), true);
                }
                catch (Exception ex)
                {
                    Logger.Writer(guid, QueueStatus.Open, $"create new Invoice document error:{ex.Message}");
                    await soService.HandleSalesOrderResult(new Result() { ResultCode = -1, Message = ex.Message }, str.ToString().Trim(','), true);
                }

            }
            #endregion

            #region 分录
            var newJESalesOrderList = soService.MergeSalesOrder(soService.PackingSalesOrder(salesOrderList.Where(c => c.IsJESync == "N").ToList()));
            Logger.Writer(guid, QueueStatus.Open, $"已合并成[{newINSalesOrderList.Count}[条应生成分录的销售订单");
            foreach (var item in newJESalesOrderList)
            {
                StringBuilder str = new StringBuilder();
                try
                {
                    item.SalesOrders.ForEach(c => { str.Append("'"); str.Append(c.DocEntry); str.Append("',"); });
                    var rt = JE_SalesOrder.CreateJournalEntry(item);
                    await soService.HandleSalesOrderResult(rt, str.ToString().Trim(','), false);
                }
                catch (Exception ex)
                {
                    Logger.Writer(guid, QueueStatus.Open, $"create journal entry document error:{ex.Message}");
                    await soService.HandleSalesOrderResult(new Result() { ResultCode = -1, Message = ex.Message }, str.ToString().Trim(','), false);
                }
            }
            #endregion

            Logger.Writer(guid, QueueStatus.Close, "sales order has been handled.");
        }
    }
}
