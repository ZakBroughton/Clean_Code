using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Assignment.DataAccess
{

    /* 
    This class, DatabaseInitialiser, demonstrates adherence to SOLID principles:
    - Single Responsibility Principle (SRP) by managing database table initialization and cleanup.
    - Interface Segregation Principle (ISP) by maintaining a focused set of responsibilities.
    - Considerations for Dependency Inversion Principle (DIP) could further enhance abstraction of database-specific dependencies.
    */

    public class DatabaseInitialiser
    {
        private readonly MySqlCommand createEmployeeTable = new MySqlCommand
        {
            CommandText = "CREATE TABLE Employee (EmployeeID INT AUTO_INCREMENT PRIMARY KEY, EmployeeName VARCHAR(255))",
            CommandType = CommandType.Text
        };

        private readonly MySqlCommand createItemsTable = new MySqlCommand
        {
            CommandText = "CREATE TABLE Items (ItemID INT AUTO_INCREMENT PRIMARY KEY, ItemName VARCHAR(255), Quantity INT,ItemPrice DECIMAL(10, 2) )",
            CommandType = CommandType.Text
        };
        private readonly MySqlCommand createTransactionLogsTable = new MySqlCommand
        {
            CommandText = "CREATE TABLE TransactionLogs (" +
        "LogID INT AUTO_INCREMENT PRIMARY KEY," +
        "TypeOfTransaction VARCHAR(255)," +
        "ItemID INT," + // Note: ItemID is still included but without FOREIGN KEY constraint
        "ItemName VARCHAR(255)," +
        "Quantity INT," +
        "ItemPrice DECIMAL(10, 2)," +
        "EmployeeName VARCHAR(255), " +
        "DateAdded DATETIME)", // Removed the FOREIGN KEY constraint line
            CommandType = CommandType.Text
        };
        private readonly MySqlCommand dropEmployeeTable = new MySqlCommand
        {
            CommandText = "DROP TABLE Employee",
            CommandType = CommandType.Text
        };
        private readonly MySqlCommand dropItemsTable = new MySqlCommand
        {
            CommandText = "DROP TABLE Items",
            CommandType = CommandType.Text

        };
        private readonly MySqlCommand dropTransactionLogsTable = new MySqlCommand
        {
            CommandText = "DROP TABLE TransactionLogs",
            CommandType = CommandType.Text
        };




        private readonly List<MySqlCommand> commandSequence;

        public DatabaseInitialiser()
        {
            commandSequence = new List<MySqlCommand>()
            { 
                dropTransactionLogsTable,
                dropItemsTable,
                dropEmployeeTable,
                
                
                      

                createEmployeeTable,
                createItemsTable,
                createTransactionLogsTable
            };
        }

        public void Initialise()
        {
            string connectionString = "server=127.0.0.1;database=Stock_Items-GUI;uid=root;password=";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    foreach (MySqlCommand c in commandSequence)
                    {
                        try
                        {
                            c.Connection = conn;
                            c.ExecuteNonQuery();
                        }
                        catch (MySqlException e)
                        {
                            // Handle foreign key constraints
                            if (e.Number == 1215)
                            {
                                Console.WriteLine($"Skipping table creation: {e.Message}");
                                continue;
                            }

                            // Log or handle other errors appropriately
                            Console.WriteLine($"Error executing SQL command: {e.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to the database: {ex.Message}");
                    // Log the error or handle it appropriately
                }
            }
        }



        public void CleanUp()
        {
            string connectionString = "server=127.0.0.1;database=stock_items;uid=root;password=";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Drop or truncate the tables to clean up after tests
                    MySqlCommand[] cleanupCommands = {
                new MySqlCommand("TRUNCATE TABLE TransactionLogs", conn),
                new MySqlCommand("TRUNCATE TABLE Items", conn),
                new MySqlCommand("TRUNCATE TABLE Employee", conn)
                // Add more commands for other cleanup operations if needed
            };

                    foreach (var cmd in cleanupCommands)
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error cleaning up the database: {ex.Message}");
                    // Log the error or handle it appropriately
                }
            }
        }
    }
}
