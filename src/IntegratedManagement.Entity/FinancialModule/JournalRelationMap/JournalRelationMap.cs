using IntegratedManagement.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.FinancialModule.JournalRelationMap
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 9:17:30
	===============================================================================================================================*/
    public class JournalRelationMap: ResultObject
    {
        public JournalRelationMap()
        {
            this.JournalRelationMapLines = new List<JournalRelationMapLine>();
        }
        public int DocEntry { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 交易号
        /// </summary>
        public int TransId { get; set; }

        public int TransType { get; set; }

        public int BaseRef { get; set; }
        public int BtfLine { get; set; }
        /// <summary>
        /// 分支编号
        /// </summary>
        public int BPLId { get; set; }

        public string BPLName { get; set; }

        /// <summary>
        /// 过账日期
        /// </summary>
        public DateTime RefDate { get; set; }
        /// <summary>
        /// 到期日
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime TaxDate { get; set; }


        public string Memo { get; set; }

        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }

        public string Series { get; set; }

        /// <summary>
        /// 是否拆分单据
        /// </summary>
        public string IsApart { get; set; }

        /// <summary>
        /// 是否同步至财务系统
        /// </summary>
        public string IsSyncToCW { get; set; }
        ///// <summary>
        ///// 创建人
        ///// </summary>
        //public string Creator { get; set; }

        public string Approver { get; set; }

        ///// <summary>
        ///// 不需要拆分的分录是否生成成功
        ///// </summary>
        //public string IsSync { get; set; }

        public DateTime SyncDate { get; set; }

        public int NewTransId { get; set; }

        public string SyncMessage { get; set; }

        /// <summary>
        /// 需要拆分负数单据是否生成成功
        /// </summary>
        public string IsMinusSync { get; set; }

        public DateTime MinusSyncDate { get; set; }

        public int NewMinusTransId { get; set; }

        public string MinusSyncMessage { get; set; }

        /// <summary>
        /// 需要拆分的正数单据是否生成成功
        /// </summary>
        public string IsPositiveSync { get; set; }

        public DateTime PositiveSyncDate { get; set; }

        public int NewPositiveTransId { get; set; }
        public string PositiveSyncMessage { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string HandleResult { get; set; }

        public DateTime HandleDate { get; set; }

        public string HandleMessage { get; set; }

        public string SerialNumber { get; set; }

        public List<JournalRelationMapLine> JournalRelationMapLines { get; set; }


        public static JournalRelationMap Create(JournalSource.JournalSource JRSource)
        {
            JournalRelationMap jrMap = new JournalRelationMap();
            jrMap.JournalRelationMapLines = new List<JournalRelationMapLine>();
            jrMap.TransId = JRSource.TransId;
            jrMap.TransType = JRSource.TransType;
            jrMap.BPLId = JRSource.BPLId;
            jrMap.CreateDate = JRSource.CreateDate;
            jrMap.TaxDate = JRSource.TaxDate;
            jrMap.Number = JRSource.Number;
            jrMap.RefDate = JRSource.RefDate;
            jrMap.DueDate = JRSource.DueDate;
            jrMap.BPLName = JRSource.BPLName;
            jrMap.BaseRef = JRSource.BaseRef;
            jrMap.Ref1 = JRSource.Ref1;
            jrMap.Ref2 = JRSource.Ref2;
            jrMap.Ref3 = JRSource.Ref3;
            jrMap.Memo = JRSource.Memo;
            jrMap.Creator = JRSource.Creator;
            jrMap.Approver = JRSource.Approver;
            jrMap.IsApart = JRSource.IsApart;
            jrMap.Series = JRSource.Series;
            jrMap.BtfLine = JRSource.BtfLine;
            foreach(var item in JRSource.JournalSourceLines)
            {
                JournalRelationMapLine line = new JournalRelationMapLine();
                line.BPLId = item.BPLId;
                line.CardCode = item.CardCode;
                line.CardName = item.CardName;
                line.AcctCode = item.AcctCode;
                line.Credit = item.Credit;
                line.Debit = item.Debit;
                line.LineMemo = item.LineMemo;
                line.LineId = item.LineId;
                line.TransId = item.TransId;
                line.OcrCode2 = item.OcrCode2;
                line.OcrCode3 = item.OcrCode3;
                line.OcrCode4 = item.OcrCode4;
                line.OcrCode5 = item.OcrCode5;
                line.ShorName = item.ShortName;
                line.ProfitCode = item.ProfitCode;
                jrMap.JournalRelationMapLines.Add(line);
            }
            return jrMap;
        }

        public static List<JournalRelationMap> Hanlde(List<JournalRelationMap> JournalRelationMaps)
        {
            if(JournalRelationMaps.Count != 0)
            {
                foreach (var item in JournalRelationMaps)
                {
                    if(item.IsApart == "N")
                    {
                        if (item.IsSync == "Y")
                        {
                            item.HandleResult = "生成成功";
                        }
                        else if (item.IsSync == "N")
                        {
                            item.HandleResult = "未生成";
                            item.HandleMessage = item.SyncMessage;
                        }
                        item.HandleDate = item.SyncDate;
                    }
                    else
                    {
                        if(item.IsMinusSync == "N" && item.IsPositiveSync == "N")
                        {
                            item.HandleResult = "未生成";
                        }
                        else if (item.IsMinusSync == "Y" && item.IsPositiveSync == "Y")
                        {
                            item.HandleResult = "生成成功";
                            item.HandleDate = item.MinusSyncDate;
                        }
                        else if(item.IsMinusSync == "Y" && item.IsPositiveSync == "N")
                        {
                            item.HandleResult = "正向拆分生成失败";
                            item.HandleMessage = item.PositiveSyncMessage;
                            item.HandleDate = item.PositiveSyncDate;
                        }
                        else if (item.IsMinusSync == "N" && item.IsPositiveSync == "Y")
                        {
                            item.HandleResult = "负向拆分生成失败";
                            item.HandleMessage = item.MinusSyncMessage;
                            item.HandleDate = item.MinusSyncDate;
                        }
                    }
                }
            }
            return JournalRelationMaps;
        }
    }
}
