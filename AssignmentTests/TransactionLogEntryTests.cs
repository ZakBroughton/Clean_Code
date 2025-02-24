using Assignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTests
{
    [TestClass]
    public class TransactionLogEntryTests
    {
        [TestMethod]
        public void TransactionLogEntry_WhenInitialized_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var typeOfTransaction = "Quantity Added";
            var itemID = 1;
            var itemName = "TestItem";
            var itemPrice = 10.5;
            var quantity = 5;
            var employeeName = "John Doe";
            var dateAdded = DateTime.Now;

            // Act
            var transactionLogEntry = new TransactionLogEntry(typeOfTransaction, itemID, itemName, itemPrice, quantity, employeeName, dateAdded);

            // Assert
            Assert.AreEqual(typeOfTransaction, transactionLogEntry.TypeOfTransaction);
            Assert.AreEqual(itemID, transactionLogEntry.ItemID);
            Assert.AreEqual(itemName, transactionLogEntry.ItemName);
            Assert.AreEqual(itemPrice, transactionLogEntry.ItemPrice);
            Assert.AreEqual(quantity, transactionLogEntry.Quantity);
            Assert.AreEqual(employeeName, transactionLogEntry.EmployeeName);
            Assert.AreEqual(dateAdded, transactionLogEntry.DateAdded);
        }
    }
}
