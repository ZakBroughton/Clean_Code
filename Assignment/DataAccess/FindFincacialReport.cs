using Assignment.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class FindFincacialReport : DatabaseSelector<List<TransactionDTO>>
    {
        public FindFincacialReport() { }

        protected override string GetSQL()
        {
            return "SELECT ItemName, Quantity, ItemPrice FROM Items";
        }

        

        protected override async Task<List<TransactionDTO>> DoSelectAsync(MySqlCommand command)
        {
            List<TransactionDTO> inventoryReports = new List<TransactionDTO>();

            try
            {
                using (MySqlDataReader dr = await command.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        string itemName = dr.GetString("ItemName");
                        int quantity = dr.GetInt32("Quantity");
                        double itemPrice = dr.GetDouble("ItemPrice");

                        double totalCost = itemPrice * quantity;

                        TransactionDTO inventoryReport = new TransactionDTO("TypeOfTransaction", 0, itemName, itemPrice, quantity, "", DateTime.Now);
                        inventoryReport.TotalCost = totalCost;

                        inventoryReports.Add(inventoryReport);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching inventory report asynchronously", e);
            }

            return inventoryReports;
        }
    }
}