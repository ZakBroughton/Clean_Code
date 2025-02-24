using Assignment;
using Assignment.DataAccess;
using Assignment.DTO;
using Assignment.Models;
using Assignment.ui_command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ui_command
{
    public class AddItemToStockCommand : UI_Command
    {
        private readonly IDataGatewayFacade dataGatewayFacade;

        public AddItemToStockCommand(IDataGatewayFacade dataGatewayFacade)
        {
            this.dataGatewayFacade = dataGatewayFacade;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                string employeeName = ConsoleReader.ReadString("\nEmployee ItemName");
                Employee employee = await dataGatewayFacade.FindEmployee(employeeName);
                if (employee == null)
                {
                    throw new Exception("ERROR: Employee not found");
                }

                // Removed reading itemId from console, as it's auto-incremented by the database.
                string itemName = ConsoleReader.ReadString("Item ItemName");
                Item existingItem = await dataGatewayFacade.FindItemByName(itemName);

                if (existingItem != null)
                {
                    throw new Exception($"ERROR: An item with the name {itemName} already exists.");
                }

                int itemQuantity = ConsoleReader.ReadInteger("Item Quantity");
                double itemPrice = ConsoleReader.ReadDouble("Item Price");
                


                if (itemPrice < 0)
                {
                    throw new Exception("ERROR: Price below 0");
                }

          
                Item itemToAdd = new Item(itemName, itemPrice, itemQuantity, DateTime.Now);

                int addedItemId = await dataGatewayFacade.AddItem(itemToAdd);


                await dataGatewayFacade.AddTransactionLog(new TransactionDTO(
                    "Item Added", addedItemId, itemName, itemPrice, itemQuantity, employeeName, DateTime.Now));

                Console.WriteLine("Item Added");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }
}