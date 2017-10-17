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

        public int BtfLine { get; set; }
        /// <summary>
        /// 分支编号
        /// </summary>
        public int BPLId { get; set; }

        /// <summary>
        /// ERP订单号
        /// </summary>
        public string ERPOrderNum { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        public string SourceTable { get; set; }

        /// <summary>
        /// 业务流
        /// </summary>
        public string WorkFlow { get; set; }

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

        public List<JournalRelationMapLine> JournalRelationMapLines { get; set; }


    }
}
