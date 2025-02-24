using Assignment.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class GetAllTransactionLog : DatabaseSelector<List<TransactionDTO>>
    {
        public GetAllTransactionLog()
        {
        }

        protected override string GetSQL()
        {
            return "SELECT LogID, TypeOfTransaction, ItemID, ItemName, Quantity, ItemPrice ,EmployeeName, DateAdded FROM TransactionLogs";
        }

        protected override async Task<List<TransactionDTO>> DoSelectAsync(MySqlCommand command)
        {
            List<TransactionDTO> transactionLogs = new List<TransactionDTO>();

            try
            {
                command.CommandText = GetSQL();
                command.CommandType = System.Data.CommandType.Text;

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
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
                
                Console.WriteLine($"Error retrieving transaction logs: {ex.Message}");
            }

            return transactionLogs;
        }

        
    }
}
