using MySqlConnector;
using System;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class RemoveQuantity : DatabaseUpdater<Item>
    {
        
        private int quantityToRemove;

        

        public RemoveQuantity(int quantityToRemove)
        {
          
            this.quantityToRemove = quantityToRemove;
        }

        protected override async Task<int> DoUpdateAsync(MySqlCommand command, Item itemToUpdate)
        {
            try
            {
                if (itemToUpdate != null)
                {

                    Console.WriteLine($"Updated ItemID: {itemToUpdate.ItemID} with new quantity.");

                    command.Parameters.AddWithValue("@QuantityToRemove", quantityToRemove);
                    command.Parameters.AddWithValue("@ItemID", itemToUpdate.ItemID);

                    return await command.ExecuteNonQueryAsync();
                }
                else
                {
                    Console.WriteLine("Item to update is null");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred: " + ex.Message);
                return -1;
            }
        }

        protected override string GetSQL()
        {
            return "UPDATE Items SET Quantity = Quantity - @QuantityToRemove WHERE ItemID = @ItemID";
        }
    }
}

