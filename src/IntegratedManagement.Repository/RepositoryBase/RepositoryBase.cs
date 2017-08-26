using IntegratedManagement.Entity.Param;
using IntegratedManagement.IRepository.IRepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Repository.RepositoryBase
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/20 9:22:11
	===============================================================================================================================*/
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public List<T> Fetch(QueryParam Param)
        {
            throw new NotImplementedException();
        }

        public T FetchByID(object ID)
        {
            throw new NotImplementedException();
        }

        public void Save(T Entity)
        {
            throw new NotImplementedException();
        }
    }
}
