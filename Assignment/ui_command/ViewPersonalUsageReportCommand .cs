using Assignment;
using Assignment.DataAccess;
using Assignment.DTO;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ui_command
{
    public class ViewPersonalReportCommand : UI_Command
    {
        private readonly IDataGatewayFacade dataGatewayFacade;

        public ViewPersonalReportCommand(IDataGatewayFacade dataGatewayFacade)
        {
            this.dataGatewayFacade = dataGatewayFacade;
        }

        public async Task ExecuteAsync()
        {
            string employeeName = ConsoleReader.ReadString("Employee name");

            List<TransactionDTO> transactions = await dataGatewayFacade.FindPersonalUsageReport(employeeName);

            if (transactions.Any())
            {
                Console.WriteLine("\nPersonal Report for {0}", employeeName);
                Console.WriteLine("\t{0, -20} {1, -10} {2, -12} {3, -12}", "Date Taken", "ID", "ItemName", "Quantity Removed");

                foreach (var transaction in transactions)
                {
                    if (transaction.TypeOfTransaction.Equals("Quantity Removed") && transaction.EmployeeName == employeeName)
                    {
                        Console.WriteLine(
                            "\t{0, -20} {1, -10} {2, -12} {3, -12}",
                            transaction.DateAdded,
                            transaction.ItemID,
                            transaction.ItemName,
                            transaction.Quantity);
                    }
                }
            }
            else
            {
                Console.WriteLine("No personal report found for the employee.");
            }
        }
    }
}