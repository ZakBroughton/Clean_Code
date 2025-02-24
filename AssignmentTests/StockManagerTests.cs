using Assignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTests
{
    [TestClass]
    public class StockManagerTests
    {
        

        [TestMethod]
        public void AddItem_WhenDuplicateItemAdded_ShouldThrowException()
        {
            // Arrange
            var stockManager = new StockManager();
            var itemId = 1;
            var itemName = "TestItem";
            var quantity = 5;
            var itemPrice = 0.25;
            // Act
            stockManager.AddItem(itemId, itemName, itemPrice, quantity);

            // Assert
            Assert.ThrowsException<System.ArgumentException>(() => stockManager.AddItem(itemId, itemName, itemPrice, quantity));
        }
    


        [TestMethod]
        public void FindItem_WhenItemDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var stockManager = new StockManager();
            var itemId = 1;

            // Act
            var foundItem = stockManager.FindItem(itemId);

            // Assert
            Assert.IsNull(foundItem);
        }
    }
}