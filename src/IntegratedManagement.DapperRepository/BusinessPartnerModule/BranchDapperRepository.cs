using IntegratedManagement.IRepository.BusinessPartnerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.BusinessPartnerModule.Branch;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;

namespace IntegratedManagement.RepositoryDapper.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/9/12 20:20:42
	===============================================================================================================================*/
    public class BranchDapperRepository : IBranchRepository
    {
        public async Task<List<Branch>> Fetch(QueryParam queryParam)
        {
            List<Branch> customerList = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  top {queryParam.limit} {queryParam.select} FROM T_VIEW_BRANCH t0  {queryParam.filter + " " + queryParam.orderby} ";
                try
                {
                    var collection = await conn.QueryAsync<Branch>(sql);
                    customerList = collection.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return customerList;
            }
        }
    }
}
