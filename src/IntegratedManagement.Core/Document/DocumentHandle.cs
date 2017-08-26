using IntegratedManagement.Entity.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Core.Document
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/27 10:17:15
	===============================================================================================================================*/
    public class DocumentItemHandle<T> where T: IDocumentItemBase
    {
        /// <summary>
        /// 通过主表的DocEntry 给子表的DocEntry和LineNum赋值
        /// </summary>
        /// <param name="DocumentItemList"></param>
        /// <param name="DocEntry"></param>
        /// <returns></returns>
        public static List<T> GetDocumentItems(List<T> DocumentItemList,int DocEntry)
        {
            for (int i = 1; i < DocumentItemList.Count + 1; i++)
            {
                DocumentItemList[i - 1].DocEntry = DocEntry;
                DocumentItemList[i - 1].LineNum = i;
            }
            return DocumentItemList;
        }

    }
}
