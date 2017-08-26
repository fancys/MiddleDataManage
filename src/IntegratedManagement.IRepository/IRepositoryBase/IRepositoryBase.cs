using IntegratedManagement.Entity.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.IRepository.IRepositoryBase
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/17 17:07:06
	===============================================================================================================================*/
    public interface IRepositoryBase<T> where T:class
    {
        List<T> Fetch(QueryParam Param);

        void Save(T Entity);

        //Task<bool> UpdateDocument();
    }
}
