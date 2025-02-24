using Assignment.ui_command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class DisplayMenuCommand : UI_Command
    {
        public DisplayMenuCommand() { }

        public void Execute()
        {
            ShowMenu();
        }

        public async Task ExecuteAsync()
        {
            ShowMenu();
        }

        public static void ShowMenu()
        {
            Console.WriteLine("\n{0}. Add item to stock", UI_Command.ADD_ITEM_TO_STOCK);
            Console.WriteLine("{0}. Add quantity to item", UI_Command.ADD_QUANTITY_TO_ITEM);
            Console.WriteLine("{0}. Take quantity from item", UI_Command.TAKE_QUANTITY_FROM_ITEM);
            Console.WriteLine("{0}. View inventory report", UI_Command.VIEW_INVENTORY_REPORT);
            Console.WriteLine("{0}. View financial report", UI_Command.VIEW_FINANCIAL_REPORT);
            Console.WriteLine("{0}. View transaction log", UI_Command.VIEW_TRANSACTION_LOG);
            Console.WriteLine("{0}. View personal usage report", UI_Command.VIEW_PERSONAL_USAGE_REPORT);
            Console.WriteLine("{0}. Exit", UI_Command.EXIT);
            Console.WriteLine("{0}. Display Menu", UI_Command.DISPLAY_MENU);
            Console.WriteLine("{0}. Initialse Database", UI_Command.INITIALISE_DATABASE);
        }
    }
}
