using IntegratedManagement.Entity.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.FinancialModule.JournalRelationMap
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

        /// <summary>
        /// 交易行号
        /// </summary>
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

        public string ProfitCode { get; set; }

        public string OcrCode2 { get; set; }
        public string OcrCode3 { get; set; }
        public string OcrCode4 { get; set; }
        public string OcrCode5 { get; set; }
    
        /// <summary>
        /// 行备注
        /// </summary>
        public string LineMemo { get; set; }
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
