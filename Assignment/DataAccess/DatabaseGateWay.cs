using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    /* 
    This class, DatabaseGateway, exemplifies adherence to SOLID principles:
    - Single Responsibility Principle (SRP) by managing database interactions.
    - Open/Closed Principle (OCP) through the Template Method pattern for extensibility.
    - Potential adherence to Liskov Substitution Principle (LSP) depending on derived class implementations.
    - Considerations for Dependency Inversion Principle (DIP) by suggesting usage of abstractions for dependencies.
    */


    public abstract class DatabaseGateWay
    {
        private DatabaseConnectionPool connectionPool;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(10);

        protected abstract string InsertionSQL { get; }

        public DatabaseGateWay()
        {
            connectionPool = DatabaseConnectionPool.GetInstance();
        }

        protected void CloseMySQLConnection(MySqlConnection conn)
        {
            connectionPool.ReleaseConnection(conn);
        }

        protected MySqlConnection GetMySQLConnection()
        {
            return connectionPool.AcquireConnection();
        }

        // This implements the Template Method design pattern
       
        public async Task InsertAsync(Object objectToInsert, CancellationToken cancellationToken = default)
        {
            {
                MySqlConnection conn = GetMySQLConnection();

                MySqlCommand command = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = InsertionSQL,
                    CommandType = CommandType.Text
                };

                try
                {
                    await DoInsertionAsync(command, objectToInsert, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
                finally
                {
                    CloseMySQLConnection(conn);
                }
            }
        }

        public async Task InsertMultipleAsync(IEnumerable<Object> objectsToInsert)
        {
            var tasks = objectsToInsert.Select(obj => InsertAsync(obj));
            await Task.WhenAll(tasks);
        }

        protected abstract Task DoInsertionAsync(MySqlCommand command, Object objectToInsert, CancellationToken cancellationToken);

      
    }
}




//public void Insert(Object objectToInsert)
//{
//MySqlConnection conn = GetMySQLConnection();
//
//    MySqlCommand command = new MySqlCommand
//    {
//        Connection = conn,
//        CommandText = InsertionSQL,
//        CommandType = CommandType.Text
//    };
//
//    try
//    {
//        DoInsertion(command, objectToInsert);
//    }
//    catch (Exception e)
//    {
//        throw new Exception(e.Message, e);
//    }
//
//    CloseMySQLConnection(conn);
//}