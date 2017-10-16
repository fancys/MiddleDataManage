using IntegratedManagement.IRepository.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedManagement.Entity.Help;
using IntegratedManagement.Entity.Result;
using IntegratedManagement.RepositoryDapper.BaseRepository;
using Dapper;

namespace IntegratedManagement.RepositoryDapper.Help
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/10/16 17:53:37
	===============================================================================================================================*/
    public class SerialNumberDapperRepository : ISerialNumberRepository
    {
        public async Task<SerialNumber> GetSerialNumber()
        {
            List<SerialNumber> customerList = null;
            using (var conn = SqlConnectionFactory.CreateSqlConnection())
            {
                conn.Open();
                string sql = $"SELECT  *  FROM T_SerialNumber where Year = {DateTime.Now.Year} and Month = {DateTime.Now.Month}";
                try
                {
                    var collection = await conn.QueryAsync<SerialNumber>(sql);
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
                return customerList.FirstOrDefault();
            }
        }

        public Task<SaveResult> Save(SerialNumber serialNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(SerialNumber serialNumber)
        {
            throw new NotImplementedException();
        }
    }
}
