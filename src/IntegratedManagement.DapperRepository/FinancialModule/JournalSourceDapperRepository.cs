using IntegratedManagement.IRepository.FinancialModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.FinancialModule.JournalSource;
using IntegratedManagement.Entity.Param;
using IntegratedManagement.RepositoryDapper.BaseRepository;

namespace IntegratedManagement.RepositoryDapper.FinancialModule
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/8/29 16:46:21
	===============================================================================================================================*/
    public class JournalSourceDapperRepository : IJournalSourceRepository
    {
        public async Task<List<JournalSource>> GetJournalSourceList(QueryParam Param)
        {
            List<JournalSource> collection = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();

                string sql = $"SELECT  top {Param.limit} {Param.select} FROM T_VIEW_JOURNAL_SOURCE t0 left JOIN T_View_JournalSourceItem t1 on t0.TransId = t1.TransId {Param.filter + " " + Param.orderby} ";
                try
                {
                    var coll = await conn.QueryParentChildAsync<JournalSource, JournalSourceLine, int>(sql, p => p.TransId, p => p.JournalSourceLines, splitOn: "TransId");
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
    }
}
