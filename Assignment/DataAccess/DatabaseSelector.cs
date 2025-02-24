using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    // This class and its subclasses implement the Table Data Gateway 
    // and Template Method design patterns
    public abstract class DatabaseSelector<T> : ISelector<T>
    {

        // This method is a Template Method
     

        
        protected abstract string GetSQL();
        protected abstract Task<T> DoSelectAsync(MySqlCommand command);

        public async Task<T> SelectAsync()
        {
            DatabaseConnectionPool connectionPool = DatabaseConnectionPool.GetInstance();
            MySqlConnection conn = connectionPool.AcquireConnection();

            MySqlCommand command = new MySqlCommand
            {
                Connection = conn,
                CommandText = GetSQL(),
                CommandType = CommandType.Text
            };

            try
            {
                return await DoSelectAsync(command);
            }
            finally
            {
                connectionPool.ReleaseConnection(conn);
            }
        }
    }
}