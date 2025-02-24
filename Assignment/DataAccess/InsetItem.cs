using Assignment.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment.DataAccess
{
    public class InsertItem : DatabaseInserter<Item>
    {
        protected override string GetSQL()
        {
            // Note: You don't need to specify ItemID here since it's auto-incremented by the database.
            return "INSERT INTO Items (ItemName, Quantity, ItemPrice) VALUES (@name, @quantity, @itemPrice);";
        }

       

        protected override async Task<int> DoInsertAsync(MySqlCommand command, Item itemToInsert, CancellationToken cancellationToken)
        {
            try
            {

                // Add parameters to the command object
                command.Parameters.AddWithValue("@name", itemToInsert.ItemName);
                command.Parameters.AddWithValue("@quantity", itemToInsert.Quantity);
                command.Parameters.AddWithValue("@itemPrice", itemToInsert.ItemPrice);

                // Prepare command for execution
                command.Prepare();

                // Execute the INSERT command
                command.ExecuteNonQuery();

                // Retrieve the ID of the inserted item
                command.CommandText = "SELECT LAST_INSERT_ID();";
                var itemId = Convert.ToInt32(command.ExecuteScalar());

                // Check if an ID was successfully retrieved
                if (itemId < 1)
                {
                    throw new Exception("ERROR: Unable to retrieve ItemID after insertion.");
                }

                // Return the auto-generated ItemID
                return itemId;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR occurred during item insertion: " + ex.Message, ex);
            }
        }
    }
}