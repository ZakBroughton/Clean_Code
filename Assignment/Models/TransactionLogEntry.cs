using Assignment.Models;
using System;

namespace Assignment
{
    public class TransactionLogEntry
    {
        public string TypeOfTransaction { get; }
        public int ItemID { get; }
        public string ItemName { get; }
        public double ItemPrice { get; }
        public int Quantity { get; }
        public string EmployeeName { get; }
    
        public DateTime DateAdded { get; }

        public TransactionLogEntry(string type, int itemID, string itemName, double itemPrice, int quantity,  string employeeName, DateTime dateAdded)
        {
            this.TypeOfTransaction = type;
            this.ItemID = itemID;
            this.ItemName = itemName;
            this.ItemPrice = itemPrice;
            this.Quantity = quantity;
            this.EmployeeName = employeeName;
            this.DateAdded = dateAdded;
        }

       
    }
}


