using Assignment.DataAccess;
using Assignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentTests
{
    [TestClass]
    public class AddQuantityFormViewModelIntegrationTests
    {
        private IDataGatewayFacade dataGatewayFacade;

        [TestInitialize]
        public void Initialize()
        {
          
            dataGatewayFacade = new DataGatewayFacade();
        
        }
        [TestMethod]
        public async Task AddMultipleItemsSimultaneously_ShouldAddAllItemsToDatabase()
        {
            // Arrange
            var items = new List<Item>();
            var tasks = new List<Task<int>>();
            int numberOfItemsToAdd = 10;
            DateTime creationDate = DateTime.Now;

            // Create multiple item instances
            for (int i = 0; i < numberOfItemsToAdd; i++)
            {
                var item = new Item($"BulkItem{i}", 0.25, 100, creationDate.AddDays(-i));
                items.Add(item);
            }

            // Act
            foreach (var item in items)
            {
                tasks.Add(dataGatewayFacade.AddItem(item));
            }

            // Execute all tasks simultaneously
            var itemIds = await Task.WhenAll(tasks);

            // Assert
            Assert.AreEqual(numberOfItemsToAdd, itemIds.Length, "Number of items added should match the number of items requested.");
            foreach (var id in itemIds)
            {
                Assert.IsTrue(id > 0, $"Each item should have a valid database-generated ID greater than 0. Received ID: {id}");
            }

            // Optional: Verify the items were added correctly
            var addedItems = new List<Item>();
            StringBuilder details = new StringBuilder();
            foreach (var id in itemIds)
            {
                var retrievedItem = await dataGatewayFacade.FindItemById(id);
                addedItems.Add(retrievedItem);
                details.AppendLine($"ID: {retrievedItem.ItemID}, Name: {retrievedItem.ItemName}, Price: {retrievedItem.ItemPrice}, Quantity: {retrievedItem.Quantity}");
            }

            Assert.AreEqual(numberOfItemsToAdd, addedItems.Count, "The number of retrieved items should match the number of added items.");
            for (int i = 0; i < numberOfItemsToAdd; i++)
            {
                Assert.AreEqual(items[i].ItemName, addedItems[i].ItemName, "Item names should match.");
                Assert.AreEqual(items[i].ItemPrice, addedItems[i].ItemPrice, "Item prices should match.");
                Assert.AreEqual(items[i].Quantity, addedItems[i].Quantity, "Item quantities should match.");
            }

            Console.WriteLine("Details of Added Items:");
            Console.WriteLine(details.ToString());
        }
    }

}
