using Assignment.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class FindItemByName : DatabaseSelector<Item>
    {
        private readonly string itemName;

        public FindItemByName(string itemName)
        {
            this.itemName = itemName;
        }

        protected override string GetSQL()
        {
            return "SELECT ItemName, ItemPrice, Quantity FROM Items WHERE ItemName = @ItemName";
        }

        protected override async Task<Item> DoSelectAsync(MySqlCommand command)
        {
            try
            {
                command.Parameters.AddWithValue("@ItemName", itemName);
                command.Prepare();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        string name = reader.GetString("ItemName");
                        double itemPrice = reader.GetDouble("ItemPrice");
                        int quantity = reader.GetInt32("Quantity");
                        DateTime dateCreated = DateTime.Now; // Assuming all items have these fields
                        return new Item(name, itemPrice, quantity, dateCreated);
                    }
                    else
                    {
                        return null; // Correctly returns null if no item is found
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Retrieval of item failed: {e.Message}"); // Log detailed error
                throw; // Rethrow the same exception to preserve the stack trace
            }
        }
    }
}