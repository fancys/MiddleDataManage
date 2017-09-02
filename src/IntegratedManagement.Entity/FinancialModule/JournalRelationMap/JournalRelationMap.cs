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

        /// <summary>
        /// 分支编号
        /// </summary>
        public int BPLId { get; set; }

        /// <summary>
        /// ERP订单号
        /// </summary>
        public int ERPOrderNum { get; set; }

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

        public List<JournalRelationMapLine> JournalRelationMapLines { get; set; }


    }
}
