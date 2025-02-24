
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Assignment.DataAccess
{
    // This class implements the Object Pool and Singleton design patterns
    // Manages a pool of MySQL database connections using the Object Pool and Singleton design patterns.
    // This class ensures efficient database connection management by limiting the maximum number of simultaneous connections
    // and reusing connections from the pool. It provides thread-safe operations to acquire and release connections,
    // handling initialization and disposal of connections to prevent leaks. The Singleton pattern ensures a single,
    // globally accessible instance of the connection pool throughout the application.
    public class DatabaseConnectionPool : IDisposable
    {

        private static readonly object _lock = new object();
        private static Lazy<DatabaseConnectionPool> instance = new Lazy<DatabaseConnectionPool>(() => new DatabaseConnectionPool(1));

        private static readonly int MaxPoolSize = 10;
        private readonly SemaphoreSlim poolSemaphore = new SemaphoreSlim(1, MaxPoolSize);
        private readonly List<MySqlConnection> availableConnections;
        private readonly List<MySqlConnection> busyConnections;
        private bool disposed = false;

        public static DatabaseConnectionPool GetInstance()
        {
            return instance.Value;
        }


        protected DatabaseConnectionPool(int initialPoolSize)
        {
            availableConnections = new List<MySqlConnection>(initialPoolSize);
            busyConnections = new List<MySqlConnection>(initialPoolSize);
            //Console.WriteLine($"Initializing SemaphoreSlim with MaxPoolSize: {MaxPoolSize}");

            poolSemaphore = new SemaphoreSlim(1, MaxPoolSize);


            for (int i = 0; i < initialPoolSize; i++)
            {
                availableConnections.Add(CreateNewConnection());
            }
        }

        public MySqlConnection AcquireConnection()
        {
            poolSemaphore.Wait();

            lock (_lock)
            {

                MySqlConnection conn = availableConnections[0];
                availableConnections.RemoveAt(0);
                busyConnections.Add(conn);
                return conn;
            }
        }


        private MySqlConnection CreateNewConnection()
        {
            string DB_CONNECTION_STRING = "server=127.0.0.1;database=Stock_Items-GUI;uid=root";
            var conn = new MySqlConnection(DB_CONNECTION_STRING);
            conn.Open();
            return conn;
        }

        public void ReleaseConnection(MySqlConnection conn)
        {
            lock (_lock)
            {
                if (busyConnections.Remove(conn)) 
                {
                    availableConnections.Add(conn);
                    poolSemaphore.Release(); 
                }
                else
                {
                    throw new InvalidOperationException("The connection was not in the busy list.");
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Not strictly necessary now, but good practice.
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                    foreach (var conn in availableConnections)
                    {
                        conn?.Dispose();
                    }
                    availableConnections.Clear();

                    foreach (var conn in busyConnections)
                    {
                        conn?.Dispose();
                    }
                    busyConnections.Clear();

                    poolSemaphore.Dispose();
                }

                disposed = true;
            }
        }
    }
}
