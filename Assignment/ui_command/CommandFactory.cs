using Assignment.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Assignment.ui_command
{
    // This class implements the Factory Method design pattern
    public class CommandFactory
    {
        IDataGatewayFacade dataGatewayFacade;

        public CommandFactory(IDataGatewayFacade dataGatewayFacade)
        {
            this.dataGatewayFacade = dataGatewayFacade;
        }


        public UI_Command CreateCommand(int option)
        {
            switch (option)
            {
                case UI_Command.ADD_ITEM_TO_STOCK:
                    return new AddItemToStockCommand(dataGatewayFacade);
                case UI_Command.ADD_QUANTITY_TO_ITEM:
                    return new AddQuantityToItemCommand(dataGatewayFacade);
                case UI_Command.TAKE_QUANTITY_FROM_ITEM:
                    return new TakeQuantityFromItemCommand(dataGatewayFacade);
                case UI_Command.VIEW_INVENTORY_REPORT:
                    return new ViewInventoryReportCommand(dataGatewayFacade);
                case UI_Command.VIEW_FINANCIAL_REPORT:
                    return new ViewFinancialReportCommand(dataGatewayFacade);
                case UI_Command.VIEW_TRANSACTION_LOG:
                    return new ViewTransactionLogCommand(dataGatewayFacade);
                case UI_Command.VIEW_PERSONAL_USAGE_REPORT:
                    return new ViewPersonalReportCommand(dataGatewayFacade);
                case UI_Command.DISPLAY_MENU:
                    return new DisplayMenuCommand();
                case UI_Command.INITIALISE_DATABASE:
                    return new InitialiseDatabaseCommand(dataGatewayFacade);
                default:
                    return new NullCommand();
            }
        }
    }

}
