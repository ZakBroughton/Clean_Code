using Assignment.Models;
using Assignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTests
{
    [TestClass]
    public class TransactionLogManagerTests
    {
        [TestMethod]
        public void TransactionLogManager_AddTransactionLog_ShouldAddEntry()
        {
            // Arrange
            var transactionLogManager = new TransactionLogManager();
            var transactionLogEntry = new TransactionLogEntry("Quantity Added", 1, "TestItem", 10.5, 5, "John Doe", DateTime.Now);

            // Act
            transactionLogManager.AddTransactionLog(transactionLogEntry);
            var transactions = transactionLogManager.GetTransactionLog();

            // Assert
            Assert.AreEqual(1, transactions.Count);
            Assert.AreEqual(transactionLogEntry, transactions[0]);
        }

       
    }
}