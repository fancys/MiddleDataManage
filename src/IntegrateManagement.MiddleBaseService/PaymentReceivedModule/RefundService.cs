using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.Entity.PaymentReceivedModule.Refund;
using IntegratedManageMent.Application.PaymentReceivedModule;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon;
using IntegrateManagement.MiddleBaseService.SAPBOneCommon.MergeRefund;
using MagicBox.Log;
using MagicBox.WindowsServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/31 16:08:24
    *	function: create JE document 
	===============================================================================================================================*/
    public class RefundService:IWindowsService
    {
        private readonly IRefundApp refundApp ;

        public RefundService(IRefundApp IRefundApp)
        {
            refundApp = IRefundApp;
        }
        public void Run()
        {
            HandleRefund();
        }

        public void Stop()
        {
            
        }

        public async void HandleRefund()
        {
            while(true)
            {
                var refundList = await GetRefundList();
                if (refundList.Count == 0) break;
                await HandleMidDocument(refundList);
                Thread.Sleep(3000);
            }
        }


        private async Task HandleMidDocument(List<Refund> refundList)
        {
            string guid = "Refund " + Guid.NewGuid();
            Logger.Writer(guid, QueueStatus.Open, $"已查询[{refundList.Count}]条退款单。");
            var newRefundList = MergeRefund(PackingRefund(refundList));
            //string testMergeDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(newRefundList);
            Logger.Writer(guid, QueueStatus.Open, $"[{refundList.Count}]条退款单已成功合并成[{newRefundList.Count}]条。");
            foreach (var item in newRefundList)
            {
                try
                {
                    StringBuilder str = new StringBuilder();
                    item.Refunds.ForEach(c => { str.Append("'"); str.Append(c.DocEntry); str.Append("',"); });
                    var rt = CreateJournalEntry(item);
                    await HandleRefundResult(rt, str.ToString().Trim(','));
                }
                catch (Exception ex)
                {
                    Logger.Writer($"退款单生成发生异常：{ex.Message}");
                }

            }
            Logger.Writer(guid, QueueStatus.Close, "退出单处理完成.");
        }

        public Result CreateJournalEntry(MergeRefund refund)
        {
            Result result = new Result();
            SAPbobsCOM.JournalEntries JE = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
            string refundType = BOneCommon.GetRefundReason(refund.RefundReason);
            JE.ReferenceDate = refund.OMSDocDate;
            JE.UserFields.Fields.Item("U_DocType").Value = refund.DocType;
            JE.UserFields.Fields.Item("U_BusinessType").Value = refund.BusinessType;
            JE.UserFields.Fields.Item("U_PlatformCode").Value = refund.PlatformCode;
            JE.UserFields.Fields.Item("U_CardCode").Value = refund.CardCode;
            JE.UserFields.Fields.Item("U_RefundReason").Value = refund.RefundReason;
            JE.UserFields.Fields.Item("U_RefundType").Value = refund.RefundType;



            Tuple<string, string> tuple = BOneCommon.GetAccountByPayMethod(refund.PayMethod);
            JE.Lines.AccountCode = tuple.Item1;
            JE.Lines.ShortName = tuple.Item2;
            JE.Lines.UserFields.Fields.Item("U_PayMethod").Value = refund.PayMethod;
            if (refundType == "N")
                JE.Lines.Debit = Convert.ToDouble(refund.GrossRefund);
            else
                JE.Lines.Credit = Convert.ToDouble(refund.GrossRefund);
            JE.Lines.Add();
            foreach (var item in refund.MergeRefundItems)
            {
                JE.Lines.AccountCode = GetAccountCode(refund.BusinessType, refundType, refund.RefundReason, item.ItemCode);
                JE.Lines.ShortName = GetShortName(refund.BusinessType, refundType, refund.RefundReason, item.ItemCode, refund.CardCode);
                JE.Lines.UserFields.Fields.Item("U_ItemCode").Value = item.ItemCode;
                JE.Lines.UserFields.Fields.Item("U_Quantity").Value = item.Quantity;
                JE.Lines.UserFields.Fields.Item("U_RefDate").Value = refund.OMSDocDate;
                if (refundType == "N")
                    JE.Lines.Credit = Convert.ToDouble(item.ItemRefund);
                else
                    JE.Lines.Debit = Convert.ToDouble(item.ItemRefund);
                JE.Lines.Add();
                
            }
            int rtCode = JE.Add();
            if(rtCode!= 0)
            {
                result.ResultCode = -1;
                result.Message = SAP.SAPCompany.GetLastErrorDescription();
            }
            else
            {
                result.ResultCode = 0;
                result.ObjCode = SAP.SAPCompany.GetNewObjectKey();
                result.Message = "create JE document successfully.";
            }

            return result;
        }

        private string GetAccountCode(string businessType,string refundType,string refundReason,string itemCode)
        {
            switch(businessType)
            {
                case "TS":
                case "AS":
                    if (refundType == "N") return "112201";
                    else return BOneCommon.GetAccountByRefundReason(refundReason);
                case "CS":
                case "SP":
                case "AR":
                    return BOneCommon.GetAccountByItemCode(itemCode);
                default:
                    throw new Exception($"业务类型[{businessType}]错误，无法处理。");

            }
        }

        private string GetShortName(string businessType, string refundType, string refundReason, string itemCode,string cardCode)
        {
            switch (businessType)
            {
                case "TS":
                case "AS":
                    if (refundType == "N") return cardCode;
                    else return BOneCommon.GetAccountByRefundReason(refundReason);
                case "CS":
                case "SP":
                    return BOneCommon.GetShortNameByItemCode(itemCode);
                case "AR":
                    return BOneCommon.GetAccountByItemCode(itemCode);
                default:
                    throw new Exception($"业务类型[{businessType}]错误，无法处理。");

            }
        }

        public async Task<List<Refund>> GetRefundList()
        {
                QueryParam queryParam = new QueryParam();
          
                //条件 未同步 未删除
                queryParam.filter = "(IsSync eq 'N') and (IsDelete eq 'N')";
            queryParam.limit = 1000;

            return await refundApp.GetRefundListAsync(queryParam);
        }

        /// <summary>
        /// 合并补退款单
        /// </summary>
        /// <param name="refundList"></param>
        /// <returns></returns>
        public List<MergeRefund> MergeRefund(List<MergeRefund> refundList)
        {
            List<MergeRefund> newRefundList = new List<MergeRefund>();
            foreach (var item in refundList)
            {
                if (newRefundList.Where(x =>x.OMSDocDate == item.OMSDocDate &&
                                            x.DocType == item.DocType &&
                                            x.CardCode == item.CardCode &&
                                            x.PlatformCode == item.PlatformCode &&
                                            x.PayMethod == item.PayMethod &&
                                            x.BusinessType == item.BusinessType &&
                                            x.RefundReason == item.RefundReason &&
                                            x.RefundType == item.RefundType).ToList().Count <= 0)
                    newRefundList.Add(item);
                else
                {
                    var newRefund = newRefundList.Where(x => x.OMSDocDate == item.OMSDocDate &&
                                            x.DocType == item.DocType &&
                                            x.CardCode == item.CardCode &&
                                            x.PlatformCode == item.PlatformCode &&
                                            x.PayMethod == item.PayMethod &&
                                            x.BusinessType == item.BusinessType &&
                                            x.RefundReason == item.RefundReason &&
                                            x.RefundType == item.RefundType).ToList().FirstOrDefault();
                    newRefund.GrossRefund += item.GrossRefund;
                    newRefund.Freight += item.Freight;
                    newRefund.Refunds.Add(new OriginRefund() { DocEntry = item.DocEntry });
                    foreach (var line in item.MergeRefundItems)
                    {
                        if (newRefund.MergeRefundItems.Where(c => c.ItemCode == line.ItemCode).ToList().Count <= 0)
                            newRefund.MergeRefundItems.Add(line);
                        else
                        {
                            var tmpLine = newRefund.MergeRefundItems.Where(c => c.ItemCode == line.ItemCode).ToList().FirstOrDefault();
                            tmpLine.Quantity += line.Quantity;
                            tmpLine.ItemRefund += line.ItemRefund;
                        }
                    }
                }
                
            }

            return newRefundList;
        }

        private async Task HandleRefundResult(Result rt,string DocEntryList)
        {
            DocumentSync documentResult = new DocumentSync();

            if (rt.ResultCode == 0)
                documentResult.SyncResult = "Y";
            else
                documentResult.SyncResult = "N";
            documentResult.SyncMsg = rt.Message;
            documentResult.SAPDocEntry = rt.ObjCode;
            documentResult.DocEntry = DocEntryList;
             await refundApp.UpdateSyncDataAsync(documentResult);

        }

        public List<MergeRefund> PackingRefund(List<Refund> refundList)
        {
            List<MergeRefund> mergeRefundList = new List<SAPBOneCommon.MergeRefund.MergeRefund>();
            foreach (var item in refundList)
            {
                MergeRefund mergeRefund = new MergeRefund();
                mergeRefund.DocEntry = item.DocEntry;
                mergeRefund.BusinessType = item.BusinessType;
                mergeRefund.CardCode = item.CardCode;
                mergeRefund.DocType = item.DocType;
                mergeRefund.Freight = item.Freight;
                mergeRefund.GrossRefund = item.GrossRefund;
                mergeRefund.OMSDocDate = item.OMSDocDate;
                mergeRefund.PlatformCode = item.PlatformCode;
                mergeRefund.RefundReason = item.RefundReason;
                mergeRefund.RefundType = item.RefundType;
                mergeRefund.PayMethod = item.PayMethod;
                mergeRefund.Refunds.Add(new OriginRefund() { DocEntry = item.DocEntry });
                foreach(var line in item.RefundItems)
                {
                    MergeRefundItem mergeRefundItem = new MergeRefundItem();
                    mergeRefundItem.ItemCode = line.ItemCode;
                    mergeRefundItem.Quantity = line.Quantity;
                    mergeRefundItem.Price = line.Price;
                    mergeRefundItem.ItemRefund = line.ItemRefund;
                    mergeRefund.MergeRefundItems.Add(mergeRefundItem);
                }
                mergeRefundList.Add(mergeRefund);
            }
            return mergeRefundList;
        }

       
    }
}
