using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ui_command
{

    // This class and its implementing classes implement the Command design pattern
    public interface UI_Command
    {
        
        Task ExecuteAsync();

        public const int ADD_ITEM_TO_STOCK = 1;
        public const int ADD_QUANTITY_TO_ITEM = 2;
        public const int TAKE_QUANTITY_FROM_ITEM = 3;
        public const int VIEW_INVENTORY_REPORT = 4;
        public const int VIEW_FINANCIAL_REPORT = 5;
        public const int VIEW_TRANSACTION_LOG = 6;
        public const int VIEW_PERSONAL_USAGE_REPORT = 7;
        public const int EXIT = 8;
        public const int DISPLAY_MENU = 9;
        public const int INITIALISE_DATABASE = 10;

        
    }
}
