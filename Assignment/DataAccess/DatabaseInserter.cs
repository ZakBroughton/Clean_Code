using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment.DataAccess
{
    // This class and its subclasses implement the Table Data Gateway 
    // and Template Method design patterns
    public abstract class DatabaseInserter<T> : IInserter<T>
    {
        
        public async Task<int> InsertAsync(T itemToInsert, CancellationToken cancellationToken = default)
        {
            int numRowsInserted = 0;
            DatabaseConnectionPool connectionPool = DatabaseConnectionPool.GetInstance();
            MySqlConnection conn = connectionPool.AcquireConnection();

            if (conn == null)
            {
                throw new InvalidOperationException("Failed to acquire a database connection.");
            }

            MySqlCommand command = new MySqlCommand
            {
                Connection = conn,
                CommandText = GetSQL(),
                CommandType = CommandType.Text
            };

            try
            {
                // Asynchronously execute DoInsertAsync
                numRowsInserted = await DoInsertAsync(command, itemToInsert, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during insert operation: {e.Message}");
                // Consider whether to rethrow the exception or handle it here
            }
            finally
            {
                connectionPool.ReleaseConnection(conn);
            }

            return numRowsInserted;
        }


        protected abstract Task<int> DoInsertAsync(MySqlCommand command, T itemToInsert, CancellationToken cancellationToken);

        protected abstract string GetSQL();
    }
}