using System;

namespace Assignment
{
    public class Item
    {
        public int ItemID { get; private set; }
        public string ItemName { get; set; }
        public int Quantity { get;  set; }
        public double ItemPrice { get; set; }
        public DateTime DateCreated { get; }


        public Item( string name, double itemPrice, int quantity, DateTime dateCreated)
        {
            string errorMsg = "";

            

            if (quantity < 1)
            {
                errorMsg += "Quantity below 1; ";
            }

            if (name.Length == 0)
            {
                errorMsg += "Item name is empty; ";
            }

            if (errorMsg.Length > 0)
            {
                throw new Exception("ERROR: Test 4" + errorMsg);
            }

           
            this.ItemName = name;
            this.ItemPrice = itemPrice;
            this.Quantity = quantity;
            this.DateCreated = dateCreated;
        }

        public Item(int itemID, string name, double itemPrice, int quantity, DateTime dateCreated)
        {
            // Set the properties, including the ItemID
            ItemID = itemID;
            ItemName = name;
            ItemPrice = itemPrice;
            Quantity = quantity;
            DateCreated = dateCreated;
        }

        public Item(string name) 
        {
            ItemName = name;

        }   

    }
}
