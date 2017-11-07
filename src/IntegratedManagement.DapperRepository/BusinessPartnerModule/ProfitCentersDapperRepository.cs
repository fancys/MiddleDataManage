using IntegratedManagement.IRepository.BusinessPartnerModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.BusinessPartnerModule.ProfitCenter;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;

namespace IntegratedManagement.RepositoryDapper.BusinessPartnerModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/11/6 23:17:51
	===============================================================================================================================*/
    public class ProfitCentersDapperRepository : IProfitCentersRepository
    {
        public async Task<List<ProfitCenters>> GetProfitCenters(QueryParam queryParam)
        {
            List<ProfitCenters> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  top {queryParam.limit} {queryParam.select} FROM T_VIEW_ProfitCenters t0  {queryParam.filter + " " + queryParam.orderby} ";
                try
                {
                    var profitCentersList = await conn.QueryAsync<ProfitCenters>(sql);
                    collection = profitCentersList.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return collection;
            }
        }
    }
}
