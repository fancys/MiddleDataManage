using IntegratedManagement.Entity.Document;
using IntegratedManagement.Entity.FinancialModule.JournalRelationMap;
using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrateManagement.MiddleBaseService.B1
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/30 14:54:06
	===============================================================================================================================*/
    public class JournalEntryService
    {
        public static DocumentSync CreateJournal(JournalRelationMap JournalRelationMap)
        {
            DocumentSync rt = new DocumentSync();
            rt.DocEntry = JournalRelationMap.DocEntry.ToString();
            try
            {
                SAPbobsCOM.JournalEntries myJE = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                myJE.ReferenceDate = JournalRelationMap.RefDate;
                myJE.TaxDate = JournalRelationMap.TaxDate;
                myJE.DueDate = JournalRelationMap.DueDate;
                myJE.UserFields.Fields.Item("U_TransId").Value = JournalRelationMap.TransId;
                if (!string.IsNullOrEmpty(JournalRelationMap.Creator))
                    myJE.UserFields.Fields.Item("U_Creator").Value = JournalRelationMap.Creator;
                if (!string.IsNullOrEmpty(JournalRelationMap.Approver))
                    myJE.UserFields.Fields.Item("U_Approver").Value = JournalRelationMap.Approver;
                myJE.UserFields.Fields.Item("U_PrintTransId").Value = JournalRelationMap.SerialNumber;

                foreach (var item in JournalRelationMap.JournalRelationMapLines)
                {
                    myJE.Lines.BPLID = item.BPLId;
                    if (!string.IsNullOrEmpty(item.ShorName))
                    {
                        myJE.Lines.ShortName = item.ShorName;
                    }
                    if(!string.IsNullOrEmpty(item.AcctCode))
                    {
                        myJE.Lines.AccountCode = item.AcctCode;
                    }
                    
                    myJE.Lines.LineMemo = item.LineMemo;
                    myJE.Lines.CostingCode = item.ProfitCode;
                    myJE.Lines.CostingCode2 = item.OcrCode2;
                    myJE.Lines.CostingCode3 = item.OcrCode3;
                    myJE.Lines.CostingCode4 = item.OcrCode4;
                    myJE.Lines.CostingCode5 = item.OcrCode5;
                    myJE.Lines.UserFields.Fields.Item("U_TransId").Value = item.TransId;
                    myJE.Lines.UserFields.Fields.Item("U_LineId").Value = item.LineId;
                    if(!string.IsNullOrEmpty(item.CardCode))
                        myJE.Lines.UserFields.Fields.Item("U_CardCode").Value = item.CardCode;
                    if(!string.IsNullOrEmpty(item.CardName))
                        myJE.Lines.UserFields.Fields.Item("U_CardName").Value = item.CardName;
                    if (item.Debit != 0)
                        myJE.Lines.Debit = Convert.ToDouble(item.Debit);
                    if (item.Credit != 0)
                        myJE.Lines.Credit = Convert.ToDouble(item.Credit);
                    myJE.Lines.Add();

                }
                int rtCode = myJE.Add();
                if (rtCode == 0)
                {
                    rt.SyncResult = "Y";
                    rt.SyncMsg = "sync successfull";
                }
                else
                {
                    rt.SyncResult = "N";
                    rt.SyncMsg = SAP.SAPCompany.GetLastErrorCode() + SAP.SAPCompany.GetLastErrorDescription();
                    rt.SAPDocEntry = SAP.SAPCompany.GetNewObjectKey();
                }
            }
            catch (Exception ex)
            {
                rt.SyncResult = "N";
                rt.SyncMsg = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 拆解负数的分录
        /// </summary>
        /// <param name="JournalRelationMap"></param>
        /// <returns></returns>
        public static DocumentSync ApartMinusJournal(JournalRelationMap JournalRelationMap)
        {
            DocumentSync rt = new DocumentSync();
            rt.DocEntry = JournalRelationMap.DocEntry.ToString();
            try
            {
                SAPbobsCOM.JournalEntries myJE = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                myJE.ReferenceDate = JournalRelationMap.RefDate;
                myJE.TaxDate = JournalRelationMap.TaxDate;
                myJE.DueDate = JournalRelationMap.DueDate;
                myJE.UserFields.Fields.Item("U_TransId").Value = JournalRelationMap.TransId;
                if (!string.IsNullOrEmpty(JournalRelationMap.Creator))
                    myJE.UserFields.Fields.Item("U_Creator").Value = JournalRelationMap.Creator;
                if (!string.IsNullOrEmpty(JournalRelationMap.Approver))
                    myJE.UserFields.Fields.Item("U_Approver").Value = JournalRelationMap.Approver;
                myJE.UserFields.Fields.Item("U_PrintTransId").Value = JournalRelationMap.SerialNumber;

                foreach (var item in JournalRelationMap.JournalRelationMapLines)
                {
                    if(item.Debit < 0 || item.Credit < 0)
                    {
                        myJE.Lines.BPLID = item.BPLId;
                        myJE.Lines.ShortName = item.ShorName;
                        myJE.Lines.AccountCode = item.AcctCode;
                        myJE.Lines.LineMemo = item.LineMemo;
                        myJE.Lines.CostingCode = item.ProfitCode;
                        myJE.Lines.CostingCode2 = item.OcrCode2;
                        myJE.Lines.CostingCode3 = item.OcrCode3;
                        myJE.Lines.CostingCode4 = item.OcrCode4;
                        myJE.Lines.CostingCode5 = item.OcrCode5;
                        myJE.Lines.UserFields.Fields.Item("U_TransId").Value = item.TransId;
                        myJE.Lines.UserFields.Fields.Item("U_LineId").Value = item.LineId;

                        // myJE.Lines.UserFields.Fields.Item("U_PAYCODE").Value = item.PayCode;
                        if (!string.IsNullOrEmpty(item.CardCode))
                            myJE.Lines.UserFields.Fields.Item("U_CardCode").Value = item.CardCode;
                        if (!string.IsNullOrEmpty(item.CardName))
                            myJE.Lines.UserFields.Fields.Item("U_CardName").Value = item.CardName;
                        //myJE.Lines.UserFields.Fields.Item("U_ERPCARDCODE").Value = item.ERPCardCode;
                        //myJE.Lines.UserFields.Fields.Item("U_ERPBASECARDCODE").Value = item.ERPBaseCardCode;
                        //myJE.Lines.PrimaryFormItems.CashFlowLineItemID = item.
                        if (item.Debit < 0 && item.Credit == 0)
                            myJE.Lines.Debit = Convert.ToDouble(item.Debit);
                        if (item.Credit < 0 && item.Debit == 0)
                            myJE.Lines.Credit = Convert.ToDouble(item.Credit);
                        myJE.Lines.Add();
                    }
                }
                int rtCode = myJE.Add();
                if (rtCode == 0)
                {
                    rt.SyncResult = "Y";
                    rt.SyncMsg = "sync successfull";
                }
                else
                {
                    rt.SyncResult = "N";
                    rt.SyncMsg = SAP.SAPCompany.GetLastErrorCode() + SAP.SAPCompany.GetLastErrorDescription();
                    rt.SAPDocEntry = SAP.SAPCompany.GetNewObjectKey();
                }
            }
            catch (Exception ex)
            {
                rt.SyncResult = "N";
                rt.SyncMsg = ex.Message;
            }
            return rt;
        }


        /// <summary>
        /// 拆解正数的分录
        /// </summary>
        /// <param name="JournalRelationMap"></param>
        /// <returns></returns>
        public static DocumentSync ApartPositiveJournal(JournalRelationMap JournalRelationMap)
        {
            DocumentSync rt = new DocumentSync();
            rt.DocEntry = JournalRelationMap.DocEntry.ToString();
            try
             {
                SAPbobsCOM.JournalEntries myJE = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                myJE.ReferenceDate = JournalRelationMap.RefDate;
                myJE.TaxDate = JournalRelationMap.TaxDate;
                myJE.DueDate = JournalRelationMap.DueDate;
                myJE.UserFields.Fields.Item("U_TransId").Value = JournalRelationMap.TransId;
                if(!string.IsNullOrEmpty(JournalRelationMap.Creator))
                    myJE.UserFields.Fields.Item("U_Creator").Value = JournalRelationMap.Creator;
                if (!string.IsNullOrEmpty(JournalRelationMap.Approver))
                    myJE.UserFields.Fields.Item("U_Approver").Value = JournalRelationMap.Approver;
                myJE.UserFields.Fields.Item("U_PrintTransId").Value = JournalRelationMap.SerialNumber;

                foreach (var item in JournalRelationMap.JournalRelationMapLines)
                {
                    if (item.Debit > 0 || item.Credit > 0)
                    {
                        myJE.Lines.BPLID = item.BPLId;
                        myJE.Lines.ShortName = item.ShorName;
                        myJE.Lines.AccountCode = item.AcctCode;
                        myJE.Lines.LineMemo = item.LineMemo;
                        myJE.Lines.CostingCode = item.ProfitCode;
                        myJE.Lines.CostingCode2 = item.OcrCode2;
                        myJE.Lines.CostingCode3 = item.OcrCode3;
                        myJE.Lines.CostingCode4 = item.OcrCode4;
                        myJE.Lines.CostingCode5 = item.OcrCode5;
                        myJE.Lines.UserFields.Fields.Item("U_TransId").Value = item.TransId;
                        myJE.Lines.UserFields.Fields.Item("U_LineId").Value = item.LineId;

                        //myJE.Lines.UserFields.Fields.Item("U_PAYCODE").Value = item.PayCode;
                        if (!string.IsNullOrEmpty(item.CardCode))
                            myJE.Lines.UserFields.Fields.Item("U_CardCode").Value = item.CardCode;
                        if (!string.IsNullOrEmpty(item.CardName))
                            myJE.Lines.UserFields.Fields.Item("U_CardName").Value = item.CardName;
                        //myJE.Lines.UserFields.Fields.Item("U_ERPCARDCODE").Value = item.ERPCardCode;
                        //myJE.Lines.UserFields.Fields.Item("U_ERPBASECARDCODE").Value = item.ERPBaseCardCode;
                        //myJE.Lines.PrimaryFormItems.CashFlowLineItemID = item.
                        if (item.Debit > 0 && item.Credit == 0)
                            myJE.Lines.Debit = Convert.ToDouble(item.Debit);
                        if (item.Credit > 0 && item.Debit == 0)
                            myJE.Lines.Credit = Convert.ToDouble(item.Credit);
                        myJE.Lines.Add();
                    }
                    

                }
                int rtCode = myJE.Add();
                if (rtCode == 0)
                {
                    rt.SyncResult = "Y";
                    rt.SyncMsg = "sync successfull";
                }
                else
                {
                    rt.SyncResult = "N";
                    rt.SyncMsg = SAP.SAPCompany.GetLastErrorCode() + SAP.SAPCompany.GetLastErrorDescription();
                    rt.SAPDocEntry = SAP.SAPCompany.GetNewObjectKey();
                }
            }
            catch (Exception ex)
            {
                rt.SyncResult = "N";
                rt.SyncMsg = ex.Message;
            }
            return rt;
        }

    }
}
