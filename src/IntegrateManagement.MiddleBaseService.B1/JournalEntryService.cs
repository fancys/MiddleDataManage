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
        public static SaveResult CreateJournal(JournalRelationMap JournalRelationMap)
        {
            SaveResult rt = new SaveResult();
            rt.UniqueKey = JournalRelationMap.DocEntry.ToString();
            try
            {
                SAPbobsCOM.JournalEntries myJE = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                myJE.ReferenceDate = JournalRelationMap.RefDate;
                myJE.TaxDate = JournalRelationMap.TaxDate;
                myJE.DueDate = JournalRelationMap.DueDate;
                myJE.UserFields.Fields.Item("U_TransId").Value = JournalRelationMap.TransId;
                foreach (var item in JournalRelationMap.JournalRelationMapLines)
                {
                    myJE.Lines.BPLID = item.BPLId;
                    myJE.Lines.ShortName = item.ShorName;
                    myJE.Lines.AccountCode = item.AcctCode;
                    myJE.Lines.LineMemo = item.LineMemo;
                    myJE.Lines.CostingCode = item.OrcCode;
                    myJE.Lines.CostingCode2 = item.OrcCode2;
                    myJE.Lines.CostingCode2 = item.OrcCode3;
                    myJE.Lines.UserFields.Fields.Item("U_TransId").Value = item.TransId;
                    myJE.Lines.UserFields.Fields.Item("U_LineId").Value = item.LineId;

                    myJE.Lines.UserFields.Fields.Item("U_PAYCODE").Value = item.PayCode;
                    myJE.Lines.UserFields.Fields.Item("U_CardCode").Value = item.CardCode;
                    myJE.Lines.UserFields.Fields.Item("U_CardName").Value = item.CardName;
                    myJE.Lines.UserFields.Fields.Item("U_ERPCARDCODE").Value = item.ERPCardCode;
                    myJE.Lines.UserFields.Fields.Item("U_ERPBASECARDCODE").Value = item.ERPBaseCardCode;
                    //myJE.Lines.PrimaryFormItems.CashFlowLineItemID = item.
                    if (item.Debit != 0)
                        myJE.Lines.Debit = Convert.ToDouble(item.Debit);
                    if (item.Credit != 0)
                        myJE.Lines.Credit = Convert.ToDouble(item.Credit);
                    myJE.Lines.Add();

                }
                int rtCode = myJE.Add();
                rt.Code = rtCode;
                rt.Message = SAP.SAPCompany.GetLastErrorCode() + SAP.SAPCompany.GetLastErrorDescription();
                if (rtCode == 0)
                    rt.ReturnUniqueKey = SAP.SAPCompany.GetNewObjectKey();

            }
            catch (Exception ex)
            {
                rt.Code = 110001;
                rt.Message = ex.Message;
            }
            return rt;
        }

        /// <summary>
        /// 拆解负数的分录
        /// </summary>
        /// <param name="JournalRelationMap"></param>
        /// <returns></returns>
        private static SaveResult ApartMinusJournal(JournalRelationMap JournalRelationMap)
        {
            SaveResult rt = new SaveResult();
            rt.UniqueKey = JournalRelationMap.DocEntry.ToString();
            try
            {
                SAPbobsCOM.JournalEntries myJE = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                myJE.ReferenceDate = JournalRelationMap.RefDate;
                myJE.TaxDate = JournalRelationMap.TaxDate;
                myJE.DueDate = JournalRelationMap.DueDate;
                myJE.UserFields.Fields.Item("U_TransId").Value = JournalRelationMap.TransId;
                foreach (var item in JournalRelationMap.JournalRelationMapLines)
                {
                    myJE.Lines.BPLID = item.BPLId;
                    myJE.Lines.ShortName = item.ShorName;
                    myJE.Lines.AccountCode = item.AcctCode;
                    myJE.Lines.LineMemo = item.LineMemo;
                    myJE.Lines.CostingCode = item.OrcCode;
                    myJE.Lines.CostingCode2 = item.OrcCode2;
                    myJE.Lines.CostingCode2 = item.OrcCode3;
                    myJE.Lines.UserFields.Fields.Item("U_TransId").Value = item.TransId;
                    myJE.Lines.UserFields.Fields.Item("U_LineId").Value = item.LineId;

                    myJE.Lines.UserFields.Fields.Item("U_PAYCODE").Value = item.PayCode;
                    myJE.Lines.UserFields.Fields.Item("U_CardCode").Value = item.CardCode;
                    myJE.Lines.UserFields.Fields.Item("U_CardName").Value = item.CardName;
                    myJE.Lines.UserFields.Fields.Item("U_ERPCARDCODE").Value = item.ERPCardCode;
                    myJE.Lines.UserFields.Fields.Item("U_ERPBASECARDCODE").Value = item.ERPBaseCardCode;
                    //myJE.Lines.PrimaryFormItems.CashFlowLineItemID = item.
                    if (item.Debit < 0 && item.Credit == 0)
                        myJE.Lines.Debit = Convert.ToDouble(item.Debit);
                    if (item.Credit < 0 && item.Debit == 0)
                        myJE.Lines.Credit = Convert.ToDouble(item.Credit);
                    myJE.Lines.Add();

                }
                int rtCode = myJE.Add();
                rt.Code = rtCode;
                rt.Message = SAP.SAPCompany.GetLastErrorCode() + SAP.SAPCompany.GetLastErrorDescription();
                if (rtCode == 0)
                    rt.ReturnUniqueKey = SAP.SAPCompany.GetNewObjectKey();

            }
            catch (Exception ex)
            {
                rt.Code = 110001;
                rt.Message = ex.Message;
            }
            return rt;
        }


        /// <summary>
        /// 拆解正数的分录
        /// </summary>
        /// <param name="JournalRelationMap"></param>
        /// <returns></returns>
        private static SaveResult ApartPositiveJournal(JournalRelationMap JournalRelationMap)
        {
            SaveResult rt = new SaveResult();
            rt.UniqueKey = JournalRelationMap.DocEntry.ToString();
            try
            {
                SAPbobsCOM.JournalEntries myJE = SAP.SAPCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                myJE.ReferenceDate = JournalRelationMap.RefDate;
                myJE.TaxDate = JournalRelationMap.TaxDate;
                myJE.DueDate = JournalRelationMap.DueDate;
                myJE.UserFields.Fields.Item("U_TransId").Value = JournalRelationMap.TransId;
                foreach (var item in JournalRelationMap.JournalRelationMapLines)
                {
                    myJE.Lines.BPLID = item.BPLId;
                    myJE.Lines.ShortName = item.ShorName;
                    myJE.Lines.AccountCode = item.AcctCode;
                    myJE.Lines.LineMemo = item.LineMemo;
                    myJE.Lines.CostingCode = item.OrcCode;
                    myJE.Lines.CostingCode2 = item.OrcCode2;
                    myJE.Lines.CostingCode2 = item.OrcCode3;
                    myJE.Lines.UserFields.Fields.Item("U_TransId").Value = item.TransId;
                    myJE.Lines.UserFields.Fields.Item("U_LineId").Value = item.LineId;

                    myJE.Lines.UserFields.Fields.Item("U_PAYCODE").Value = item.PayCode;
                    myJE.Lines.UserFields.Fields.Item("U_CardCode").Value = item.CardCode;
                    myJE.Lines.UserFields.Fields.Item("U_CardName").Value = item.CardName;
                    myJE.Lines.UserFields.Fields.Item("U_ERPCARDCODE").Value = item.ERPCardCode;
                    myJE.Lines.UserFields.Fields.Item("U_ERPBASECARDCODE").Value = item.ERPBaseCardCode;
                    //myJE.Lines.PrimaryFormItems.CashFlowLineItemID = item.
                    if (item.Debit > 0 && item.Credit == 0)
                        myJE.Lines.Debit = Convert.ToDouble(item.Debit);
                    if (item.Credit > 0 && item.Debit == 0)
                        myJE.Lines.Credit = Convert.ToDouble(item.Credit);
                    myJE.Lines.Add();

                }
                int rtCode = myJE.Add();
                rt.Code = rtCode;
                rt.Message = SAP.SAPCompany.GetLastErrorCode() + SAP.SAPCompany.GetLastErrorDescription();
                if (rtCode == 0)
                    rt.ReturnUniqueKey = SAP.SAPCompany.GetNewObjectKey();

            }
            catch (Exception ex)
            {
                rt.Code = 110001;
                rt.Message = ex.Message;
            }
            return rt;
        }

    }
}
