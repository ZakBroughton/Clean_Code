using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class FindItemById : DatabaseSelector<Item>
    {
        private readonly int itemID;

        public FindItemById(int itemID)
        {
            this.itemID = itemID;
        }

        protected override string GetSQL()
        {
            return "SELECT ItemID, ItemName, ItemPrice, Quantity FROM items WHERE ItemID = @ItemID";
        }

        protected override async Task<Item> DoSelectAsync(MySqlCommand command)
        {
            Item item = null;

            try
            {
                command.Parameters.AddWithValue("@ItemID", itemID);
                command.Prepare();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32("ItemID");
                        string name = reader.GetString("ItemName");
                        double itemprice = reader.GetDouble("ItemPrice");
                        int quantity = reader.GetInt32("Quantity");

                        // Retrieve other properties if needed from the reader
                        DateTime dateCreated = DateTime.Now;
                        item = new Item(id, name, itemprice, quantity, dateCreated);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: Retrieval of item failed", e);
            }

            return item;
        }
    }
}