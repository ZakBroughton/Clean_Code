using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.DTO
{
    [Serializable]
    public class TransactionDTO
    {
        public string TypeOfTransaction { get; }
        public double ItemPrice { get; }
        public int ItemID { get; }
        public string ItemName { get; }
        public int Quantity { get; }
        public string EmployeeName { get; }
        public DateTime DateAdded { get; }
        public double TotalCost { get; internal set; }
        public int LogId { get; }

        public TransactionDTO(string typeOfTransaction, int itemID, string itemName, double itemPrice, int quantity, string employeeName, DateTime dateAdded)
        {
            this.TypeOfTransaction = typeOfTransaction;
            this.ItemID = itemID;
            this.ItemName = itemName;
            this.ItemPrice = itemPrice;
            this.Quantity = quantity;
            this.EmployeeName = employeeName;
            this.DateAdded = dateAdded;
        }

       
     

        public TransactionDTO(int logId, int itemId, string itemName, int quantity, double itemPrice, string employeeName, DateTime dateAdded)
        {
            LogId = logId;
            ItemID = itemId;
            ItemName = itemName;
            Quantity = quantity;
            ItemPrice = itemPrice;
            EmployeeName = employeeName;
            DateAdded = dateAdded;
        }

        public TransactionDTO(string typeOfTransaction, int itemID, string itemName, double itemPrice, int quantity, DateTime dateAdded)
        {
            TypeOfTransaction = typeOfTransaction;
            ItemID = itemID;
            ItemName = itemName;
            ItemPrice = itemPrice;
            DateAdded = dateAdded;
        }

       

        public TransactionDTO(int itemID, string itemName, int quantity, DateTime dateAdded)
        {
            ItemID = itemID;
            ItemName = itemName;
            Quantity = quantity;
            DateAdded = dateAdded;
        }

        public TransactionDTO(int logId, string typeOfTransaction, int itemId, string itemName, int quantity, double itemPrice, string employeeName, DateTime dateAdded)
        {
            LogId = logId;
            TypeOfTransaction = typeOfTransaction;
            ItemID = itemId;
            ItemName = itemName;
            Quantity = quantity;
            ItemPrice = itemPrice;
            EmployeeName = employeeName;
            DateAdded = dateAdded;
        }

        
    }
}
