using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.RepositoryDapper.BaseRepository
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/17 10:37:14
    *	创建数据库连接
	===============================================================================================================================*/
    public class SqlConnectionFactory
    {
        public static IDbConnection CreateSqlConnection()
        {
            IDbConnection connection = null;
            try
            {
                string strConn = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;
                string DbType = ConfigurationManager.AppSettings["DBType"];
                switch (DbType.ToUpper())
                {
                    case "SQLSERVER": connection = new System.Data.SqlClient.SqlConnection(strConn); break;
                    case "MYSQL":// connection = new MySql.Data.MySqlClient.MySqlConnection(strConn); //break;
                    case "ORACLE": //connection = new System.Data..OracleConnection(strConn); break;
                    case "DB2": connection = new System.Data.OleDb.OleDbConnection(strConn); break;
                }
                return connection;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
