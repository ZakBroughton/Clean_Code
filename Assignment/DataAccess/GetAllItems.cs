using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class GetAllItems : DatabaseSelector<List<Item>>
    {

        private int itemID;
        public GetAllItems()
        {
            
        }

        protected override string GetSQL()
        {
           
            return "SELECT ItemID, ItemName, ItemPrice ,Quantity FROM items";
        }

        protected override async Task<List<Item>> DoSelectAsync(MySqlCommand command)
        {
            List<Item> items = new List<Item>();

            try
            {
                command.Parameters.AddWithValue("@ItemID", itemID);
                command.Prepare();
                MySqlDataReader dr = await command.ExecuteReaderAsync();
                while (dr.Read())
                {
                    int id = Convert.ToInt32(dr["ItemID"]);
                    string name = Convert.ToString(dr["ItemName"]);
                    double itemprice = Convert.ToDouble(dr["ItemPrice"]);
                    int quantity = Convert.ToInt32(dr["Quantity"]);
                    DateTime dateCreated = DateTime.Now;

                    Item item = new Item(id, name, itemprice, quantity, dateCreated);
                    items.Add(item);
                }
                dr.Close(); 
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: retrieval of item failed", e);
              
            }

            return items;
        }

       
    }
}