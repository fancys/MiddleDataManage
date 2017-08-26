using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.RepositoryDapper.BaseRepository
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/17 15:52:50
	===============================================================================================================================*/
    public class DataServiceRepository : IDataServiceRepository
    {
        public dynamic Insert<T>(T entity, IDbTransaction transaction = null) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Quary<T>(IList<dynamic> ids) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
