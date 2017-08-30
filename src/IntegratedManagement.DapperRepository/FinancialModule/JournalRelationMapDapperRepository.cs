using IntegratedManagement.IRepository.FinancialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.FinancialModule.JournalRalationMap;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.RepositoryDapper.BaseRepository;

namespace IntegratedManagement.RepositoryDapper.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:45:54
	===============================================================================================================================*/
    public class JournalRelationMapDapperRepository : IJournalRelationMapRepository
    {
        public async Task<List<JournalRelationMap>> GetJournalRelationMapList(QueryParam Param)
        {
            List<JournalRelationMap> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();

                string sql = $"SELECT  top {Param.limit} {Param.select} FROM T_JournalRelationMap t0 left JOIN T_JournalRelationMapItem t1 on t0.DocEntry = t1.DocEntry {Param.filter + " " + Param.orderby} ";
                try
                {
                    var coll = await conn.QueryParentChildAsync<JournalRelationMap, JournalRelationMapLine, int>(sql, p => p.DocEntry, p => p.JournalRelationMapLines, splitOn: "DocEntry");
                    collection = coll.ToList();
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

        public Task SaveJournalRelationMap(JournalRelationMap JournalSource)
        {
            throw new NotImplementedException();
        }
    }
}
