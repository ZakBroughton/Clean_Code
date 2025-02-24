using Assignment.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class FindInventoryReport : DatabaseSelector<TransactionDTO>
    {
        public FindInventoryReport()
        {
            
        }


        protected override async Task<TransactionDTO> DoSelectAsync(MySqlCommand command)
        {
            TransactionDTO inventoryReport = null;

            try
            {
                MySqlDataReader dr = await command.ExecuteReaderAsync();

                if (dr.Read())
                {
                    string itemName = dr.GetString("ItemName");
                    int quantity = dr.GetInt32("Quantity");
                    double itemPrice = dr.GetDouble("ItemPrice");

                    inventoryReport = new TransactionDTO("TypeOfTransaction", 0, itemName, itemPrice, quantity, "", DateTime.Now);
            
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching personal usage report", e);
            }

            return inventoryReport;
        }

      

        protected override string GetSQL()
        {
            return "SELECT ItemName, ItemPrice ,Quantity FROM TransactionLogs";
        }

    }
}