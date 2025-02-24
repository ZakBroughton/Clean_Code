using Assignment.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class GetAllTransactionLogServer : DatabaseSelector<List<TransactionDTO>>
    {
        // Make CreateConnection method static
        private static MySqlConnection CreateConnection()
        {
            string DB_CONNECTION_STRING = "server=127.0.0.1;database=Stock_Items-GUI;uid=root";

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(DB_CONNECTION_STRING);
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: connection to database failed", e);
            }

            return conn;
        }
        public GetAllTransactionLogServer()
        {
        }
     
        protected override string GetSQL()
        {
            return "SELECT LogID, TypeOfTransaction, ItemID, ItemName, Quantity, ItemPrice ,EmployeeName, DateAdded FROM TransactionLogs";
        }

        // Modify GetAllTransactionLogAsync to use CreateConnection()
        public async Task<List<TransactionDTO>> GetAllTransactionLogAsync()
        {
            return await Task.Run(async () =>
            {
                List<TransactionDTO> transactionLogs = new List<TransactionDTO>();

                try
                {
                    using (var connection = CreateConnection()) // Use CreateConnection() directly
                    {
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = GetSQL();
                            command.CommandType = System.Data.CommandType.Text;

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    int logId = reader.GetInt32("LogID");
                                    string typeOfTransaction = reader.GetString("TypeOfTransaction");
                                    int itemId = reader.GetInt32("ItemID");
                                    string itemName = reader.GetString("ItemName");
                                    int quantity = reader.GetInt32("Quantity");
                                    double itemPrice = reader.GetDouble("ItemPrice");
                                    string employeeName = reader.GetString("EmployeeName");
                                    DateTime dateAdded = reader.GetDateTime("DateAdded");

                                    TransactionDTO transaction = new TransactionDTO(logId, typeOfTransaction, itemId, itemName, quantity, itemPrice, employeeName, dateAdded);
                                    transactionLogs.Add(transaction);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving transaction logs: {ex.Message}");
                }

                return transactionLogs;
            });
        }

        protected override async Task<List<TransactionDTO>> DoSelectAsync(MySqlCommand command)
        {
            List<TransactionDTO> transactionLogs = new List<TransactionDTO>();

            try
            {
                // Assuming the connection is already open; if not, you'll need to open it here.
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int logId = reader.GetInt32("LogID");
                        string typeOfTransaction = reader.GetString("TypeOfTransaction");
                        int itemId = reader.GetInt32("ItemID");
                        string itemName = reader.GetString("ItemName");
                        int quantity = reader.GetInt32("Quantity");
                        double itemPrice = reader.GetDouble("ItemPrice");
                        string employeeName = reader.GetString("EmployeeName");
                        DateTime dateAdded = reader.GetDateTime("DateAdded");

                        TransactionDTO transaction = new TransactionDTO(logId, typeOfTransaction, itemId, itemName, quantity, itemPrice, employeeName, dateAdded);
                        transactionLogs.Add(transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving transaction logs asynchronously: {ex.Message}");
                // Depending on your error handling strategy, you might want to rethrow the exception or handle it differently.
            }

            return transactionLogs;
        }
    }
}