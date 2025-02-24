using Assignment.DataAccess;
using Assignment.DTO;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ui_command
{
    public class TakeQuantityFromItemCommand : UI_Command
    {
        private readonly IDataGatewayFacade dataGatewayFacade;

        public TakeQuantityFromItemCommand(IDataGatewayFacade dataGatewayFacade)
        {
            this.dataGatewayFacade = dataGatewayFacade;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                string employeeName = ConsoleReader.ReadString("\nEmployee Name");
                Console.WriteLine("Employee Name entered: " + employeeName);

                Employee employee = await dataGatewayFacade.FindEmployee(employeeName);
                if (employee == null)
                {
                    throw new Exception("ERROR: Employee not found");
                }

                int itemId = ConsoleReader.ReadInteger("Item ID");
                Console.WriteLine("Item ID entered: " + itemId);

                Item item = await dataGatewayFacade.FindItemById(itemId);
                if (item == null)
                {
                    throw new Exception("ERROR: Item not found");
                }

                int quantityToRemove = ConsoleReader.ReadInteger("How many items would you like to remove?");
                Console.WriteLine("Quantity to remove entered: " + quantityToRemove);

                await dataGatewayFacade.RemoveQuantity(itemId, quantityToRemove);
                Console.WriteLine(
                    "{0} has removed {1} of Item ItemID: {2} on {3}",
                    employeeName,
                    quantityToRemove,
                    itemId,
                    DateTime.Now.ToString("dd/MM/yyyy"));

                dataGatewayFacade.AddTransactionLog(
                    new TransactionDTO("Quantity Removed", itemId, item.ItemName, 0, quantityToRemove, employeeName, DateTime.Now));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred: " + e.Message);
            }
        }
    }
}