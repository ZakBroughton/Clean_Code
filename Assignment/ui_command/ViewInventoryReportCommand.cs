using Assignment.DataAccess;
using Assignment.ui_command;
using Assignment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ui_command
{
    public class ViewInventoryReportCommand : UI_Command
    {
        private readonly IDataGatewayFacade dataGatewayFacade;

        public ViewInventoryReportCommand(IDataGatewayFacade dataGatewayFacade)
        {
            this.dataGatewayFacade = dataGatewayFacade;
        }

        public async Task ExecuteAsync()
        {
            await ExecuteAsync(dataGatewayFacade);
        }

        public async Task ExecuteAsync(IDataGatewayFacade gatewayFacade)
        {
            List<Item> items = await gatewayFacade.GetAllItemsAsync();
            Console.WriteLine("\nAll items");
            Console.WriteLine("\t{0, -4} {1, -20} {2, -20}", "ItemID", "ItemName", "Quantity");
            foreach (Item i in items)
            {
                DisplayItem(i);
            }
        }

        private static void DisplayItem(Item i)
        {
            Console.WriteLine("\t{0, -4} {1, -20} {2, -20}", i.ItemID, i.ItemName, i.Quantity);
        }
    }
}