using IntegratedManagement.Entity.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.FinancialModule.JournalRalationMap
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 18:01:00
	===============================================================================================================================*/
    public class JournalRelationMapLine: IDocumentItemBase
    {
        /// <summary>
        /// 交易号
        /// </summary>
        public int TransId { get; set; }

        public int LineId { get; set; }

        public string AcctCode { get; set; }

        public string ShorName { get; set; }

        /// <summary>
        /// 分支
        /// </summary>
        public int BPLId { get; set; }
        /// <summary>
        /// 贷
        /// </summary>
        public decimal Credit { get; set; }
        /// <summary>
        /// 借
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// 报销明细类型
        /// </summary>
        public string ExpenseType { get; set; }

        /// <summary>
        /// 付款编码
        /// </summary>
        public string PayCode { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WhsCode { get; set; }
        /// <summary>
        /// ERP客户编码
        /// </summary>
        public string ERPCardCode { get; set; }
        /// <summary>
        /// ERP原单据客户编码
        /// </summary>
        public string ERPBaseCardCode { get; set; }
        /// <summary>
        /// ERP单据号
        /// </summary>
        public string ERPDocEntry { get; set; }
        /// <summary>
        /// ERP原单据行号
        /// </summary>
        public string ERPBaseNum { get; set; }
        /// <summary>
        /// 贸易伙伴代码
        /// </summary>
        public string CardCode { get; set; }
        /// <summary>
        /// 贸易伙伴名称
        /// </summary>
        public string CardName { get; set; }

        public int DocEntry
        {
            get;
            set;
        }

        public int LineNum
        {
            get;
            set;
        }
    }
}
