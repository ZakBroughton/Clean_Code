using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class GetAllItemsAsync : DatabaseSelector<List<Item>>
    {
        


        public GetAllItemsAsync()
        {
        }
      

        protected override string GetSQL()
        {
            return "SELECT ItemID, ItemName, ItemPrice, Quantity FROM items";
        }
        public async Task<List<Item>> ExecuteAsync()
        {
            // Acquire a connection from the pool
            var connection = DatabaseConnectionPool.GetInstance().AcquireConnection();
            if (connection == null)
            {
                throw new InvalidOperationException("Could not acquire a database connection.");
            }

            try
            {
                using (var command = new MySqlCommand(GetSQL(), connection))
                {
                    // Since you directly use GetSQL() for command.CommandText, there's no need for a separate commandText variable.
                    return await DoSelectAsync(command);
                }
            }
            finally
            {
                // Release the connection back to the pool once done
                DatabaseConnectionPool.GetInstance().ReleaseConnection(connection);
            }
        }

        // Convert DoSelect to async version
        protected override async Task<List<Item>> DoSelectAsync(MySqlCommand command)
        {
            List<Item> items = new List<Item>();

            try
            {
                // No need to add parameters for a simple select all
                MySqlDataReader dr = await command.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    int id = Convert.ToInt32(dr["ItemID"]);
                    string name = Convert.ToString(dr["ItemName"]);
                    double itemprice = Convert.ToDouble(dr["ItemPrice"]);
                    int quantity = Convert.ToInt32(dr["Quantity"]);
                    DateTime dateCreated = DateTime.Now; // Consider if you need this, as it's not selected from the database

                    Item item = new Item(id, name, itemprice, quantity, dateCreated);
                    items.Add(item);
                }
                await dr.CloseAsync();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: retrieval of item failed", e);
            }

            return items;
        }

       
    }
}