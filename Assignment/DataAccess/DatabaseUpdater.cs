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
    public abstract class DatabaseUpdater<T> : IUpdater<T>
    {

        // This method is a Template Method
        
        public async Task<int> UpdateAsync(T itemToUpdate)
        {
            int numRowsUpdated = 0;
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
                numRowsUpdated = await DoUpdateAsync(command, itemToUpdate);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

            connectionPool.ReleaseConnection(conn);
            return await Task.FromResult(numRowsUpdated);
        }

      
        protected abstract string GetSQL();
        protected abstract Task<int> DoUpdateAsync(MySqlCommand command, T itemToUpdate);

    }
}