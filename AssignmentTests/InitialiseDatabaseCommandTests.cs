using Assignment.DataAccess;
using Assignment.DTO;
using Assignment.Models;
using Assignment.ui_command;
using AssignmentTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Tests
{
    [TestClass]
    public class InitialiseDatabaseCommandTests
    {
        [TestClass]
        public class DataGatewayFacadeTests
        {

            private IDataGatewayFacade dataGatewayFacade;

            [TestInitialize]
            public async Task TestInitialize()
            {
                // Initialize your data gateway facade
                dataGatewayFacade = new DataGatewayFacade();

                // Execute the command to populate the database with specific data
                var initialiseCommand = new InitialiseDatabaseCommand(dataGatewayFacade);
                await initialiseCommand.ExecuteAsync();
            }







            [TestMethod]
            public async Task AddEmployee_ShouldAddEmployeeToDatabase()
            {
                // Arrange
                var employee = new Employee("John Doe");

                // Act
                int employeeId = await dataGatewayFacade.AddEmployee(employee);

                // Assert
                Assert.IsTrue(employeeId > 0);
            }



            [TestMethod]
            public async Task AddItem_ShouldAddItemToDatabase()
            {
                // Arrange
                var dateCreated = DateTime.Now;
                var item = new Item( "TestItem",0.20 , 5, dateCreated); 

                // Act
                int itemId = await dataGatewayFacade.AddItem(item);

                // Assert
                Assert.IsTrue(itemId > 0); 
            }

            [TestMethod]
            public async Task GetAllItems_ShouldReturnAllItems()
            {
                // Arrange
               
                var dateCreated1 = DateTime.Now;
                var item1 = new Item("TestItem1",0.20 ,5, dateCreated1);
                var dateCreated2 = DateTime.Now.AddDays(-1);
                var item2 = new Item("TestItem2", 0.20, 10, dateCreated2);
                await dataGatewayFacade.AddItem(item1);
                await dataGatewayFacade.AddItem(item2);

                // Act
                List<Item> retrievedItems = await dataGatewayFacade.GetAllItems();

                // Assert
                Assert.IsNotNull(retrievedItems);
               

               
            }

            [TestMethod]
            public async Task GetAllEmployee_ShouldReturnAllEmployees()
            {
                // Arrange
               
                

                // Act
                List<Employee> retrievedEmployee = await dataGatewayFacade.GetAllEmployee();

                // Assert
                Assert.IsNotNull(retrievedEmployee);
               

               
            }

            [TestMethod]
            public async Task AddTransactionLog_ShouldAddTransactionLog()
            {
                // Arrange
                var dataGatewayFacade = new DataGatewayFacade();
                var transactionDTO = new TransactionDTO("Quantity Removed", 1, "ItemName", 10.5, 5, "Employee", DateTime.Now);

                // Act
                try
                {
                    await dataGatewayFacade.AddTransactionLog(transactionDTO);
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Adding transaction log failed: {ex.Message}");
                }

                // Assert
               
            }

            [TestMethod]
            public async Task FindEmployee_ShouldReturnMatchingEmployee()
            {
                // Arrange
               

                // Act
                var retrievedEmployee = await dataGatewayFacade.FindEmployee("Graham");

                // Assert
                Assert.IsNotNull(retrievedEmployee);
                Assert.AreEqual("Graham",  retrievedEmployee.EmployeeName);
            
            }

            [TestMethod]
            public async Task FindItemById_ShouldReturnMatchingItem()
            {
                // Arrange
               

                // Act
                var retrievedItem = await dataGatewayFacade.FindItemById(1);

                // Assert
                Assert.IsNotNull(retrievedItem);
                Assert.AreEqual(1, retrievedItem.ItemID);
               
               
            }

            [TestMethod]
            public async Task GetAllTransactionLog_ShouldReturnAllTransactions()
            {
                // Arrange
              

                // Act
                List<TransactionDTO> retrievedTransactions = await dataGatewayFacade.GetAllTransactionLog();

                // Assert
                Assert.IsNotNull(retrievedTransactions);
              
            }

            [TestMethod]
            public async Task FindPersonalUsageReport_ShouldReturnEmployeeTransactions()
            {
                // Arrange
                string employeeName = "Graham"; 

                // Act
                List<TransactionDTO> personalReport = await dataGatewayFacade.FindPersonalUsageReport(employeeName);

                // Assert
                Assert.IsNotNull(personalReport);

             
                Assert.IsTrue(personalReport.All(t => t.EmployeeName == employeeName));
           
            }

            [TestMethod]
            public async Task FindInventoryReport_ShouldReturnInventoryReport()
            {
                // Arrange
               

                // Act
                TransactionDTO inventoryReport = await dataGatewayFacade.FindInventoryReport();

                // Assert
                Assert.IsNotNull(inventoryReport);
                Assert.AreEqual("TypeOfTransaction", inventoryReport.TypeOfTransaction);
                
            }
            [TestMethod]
            public async Task FindFinancialReport_ShouldReturnFinancialReport()
            {
                // Arrange
                

                // Act
                List<TransactionDTO> financialReport = await dataGatewayFacade.FindFinancialReport();

                // Assert
                Assert.IsNotNull(financialReport);

                
                Assert.IsTrue(financialReport.Any(t => t.TypeOfTransaction == "TypeOfTransaction" && t.Quantity > 0 && t.ItemPrice > 0));
                
            }
            [TestMethod]
            public async Task AddQuantityToItemCommand_ShouldIncreaseItemQuantity()
            {
                // Arrange
                var initialQuantity = 10; 
                var itemId = 1; 
                var employeeName = "Graham"; 
                var quantityToAdd = 5; 
                var itemPrice = 2.5; 

                // Simulate user input using your ConsoleReader class
               
                // Act
                var addQuantityCommand = new AddQuantityToItemCommand(dataGatewayFacade);
                FakeConsoleReader.SimulateUserInput(employeeName, itemId.ToString(), quantityToAdd.ToString(), itemPrice.ToString());
                await addQuantityCommand.ExecuteAsync();

                // Assert
                var retrievedItem = await  dataGatewayFacade.FindItemById(itemId);

               
                Assert.IsNotNull(retrievedItem);
                Assert.AreEqual(initialQuantity + quantityToAdd, retrievedItem.Quantity);

               
                FakeConsoleReader.ClearInput();
            }


            [TestMethod]
            public async Task TakeQuantityFromItemCommand_ShouldDecreaseItemQuantity()
            {   // Arrange
                var initialQuantity = 10; 
                var itemId = 1; 
                var employeeName = "Graham"; 
                var quantityToRemove = 2; 

                // Act
                var takeQuantityCommand = new TakeQuantityFromItemCommand(dataGatewayFacade);
                FakeConsoleReader.SimulateUserInput(employeeName, itemId.ToString(), quantityToRemove.ToString());
                await takeQuantityCommand.ExecuteAsync();

                // Assert
                var retrievedItem = await dataGatewayFacade.FindItemById(itemId);

                
                Assert.IsNotNull(retrievedItem);
                Assert.AreEqual(initialQuantity - quantityToRemove, retrievedItem.Quantity);

                // Clean up simulated input
                FakeConsoleReader.ClearInput();
            }
        }


    }
 }



