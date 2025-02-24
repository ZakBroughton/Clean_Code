using Assignment.DataAccess;
using Assignment.DTO;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ui_command
{
    public class ViewTransactionLogCommand : UI_Command
    {

        private readonly IDataGatewayFacade dataGatewayFacade;

        public ViewTransactionLogCommand(IDataGatewayFacade dataGatewayFacade)
        {
            this.dataGatewayFacade = dataGatewayFacade;
        }

        public async Task ExecuteAsync()
        {
            List<TransactionDTO> tls = await dataGatewayFacade.GetAllTransactionLog();

            Console.WriteLine("\nTransaction Log:");
            Console.WriteLine("\t{0, -20} {1, -16} {2, -6} {3, -12} {4, -10} {5, -12} {6, -12}",
                "Date", "TypeOfTransaction", "ItemID", "ItemName", "Quantity", "EmployeeName", "Price");

            foreach (var entry in tls)
            {
                Console.WriteLine("\t{0, -20} {1, -16} {2, -6} {3, -12} {4, -10} {5, -12} {6, -12}",
                    entry.DateAdded.ToString("dd/MM/yyyy"), entry.TypeOfTransaction, entry.ItemID, entry.ItemName,
                    entry.Quantity, entry.EmployeeName,
                    string.Format("{0:C}", entry.ItemPrice));
            }
        }
    }
}
