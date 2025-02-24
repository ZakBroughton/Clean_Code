using Assignment.DataAccess;
using Assignment.DTO;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ui_command
{
    public class AddQuantityToItemCommand : UI_Command
    {
       
        
        private readonly IDataGatewayFacade dataGatewayFacade;



        public AddQuantityToItemCommand(IDataGatewayFacade dataGatewayFacade)
        {
            this.dataGatewayFacade = dataGatewayFacade;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                string employeeName = ConsoleReader.ReadString("\nEmployee Name");
                Employee employee = await dataGatewayFacade.FindEmployee(employeeName);
                if (employee == null)
                {
                    throw new Exception("ERROR: Employee not found");
                }

                int itemId = ConsoleReader.ReadInteger("Item ID");
                Item item = await dataGatewayFacade.FindItemById(itemId);
                if (item == null)
                {
                    throw new Exception("ERROR: Item not found");
                }

                int quantityToAdd = ConsoleReader.ReadInteger("How many items would you like to add?");
                double itemPrice = ConsoleReader.ReadDouble("Item Price");

                if (itemPrice < 0)
                {
                    throw new Exception("ERROR: Price below 0");
                }

                await dataGatewayFacade.AddQuantity(itemId, quantityToAdd);
                TransactionDTO transactionLog = new TransactionDTO("Quantity Added", itemId,item.ItemName,  itemPrice ,quantityToAdd, employeeName,DateTime.Now);

                await dataGatewayFacade.AddTransactionLog(transactionLog);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: Test 2",e.Message);
            }
        }
    }
}