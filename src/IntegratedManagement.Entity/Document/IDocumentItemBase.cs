using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.Document
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/27 10:13:40
    *	单据类型实体子表基类
	===============================================================================================================================*/
    public interface IDocumentItemBase
    {
        int DocEntry { get; set; }
        int LineNum { get; set; }
    }
}
