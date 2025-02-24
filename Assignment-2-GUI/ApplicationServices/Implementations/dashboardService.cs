using Assignment.DataAccess;
using Assignment.DTO;
using Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_2_GUI.ApplicationServices.Interfaces;

namespace Assignment_2_GUI.ApplicationServices.Implementations
{
    // Provides functionalities specific to the dashboard operations as per IDashboardService interface.
    // This service utilizes a data gateway facade to interact with the data layer, performing operations like
    // calculating the total inventory value, fetching recent transaction logs, and identifying low stock items.
    // Methods are asynchronous, ensuring non-blocking operations suitable for UI responsiveness in a GUI application.
    public class DashboardService : IDashboardService
    {
        private readonly IDataGatewayFacade _dataGateway;

        public DashboardService(IDataGatewayFacade dataGateway)
        {
            _dataGateway = dataGateway;
        }

        public async Task<decimal> CalculateTotalInventoryValueAsync()
        {
            try
            {
                var items = await _dataGateway.GetAllItemsAsync();
                return (decimal)items.Sum(item => item.ItemPrice * item.Quantity);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error calculating total inventory value: " + ex.Message);
                return 0;
            }
        }

        public async Task<IEnumerable<TransactionDTO>> GetRecentTransactionsAsync(int numberOfTransactions)
        {
            var transactions = await _dataGateway.GetAllTransactionLogServerAsync();
            return transactions.OrderByDescending(t => t.DateAdded).Take(numberOfTransactions);
        }

        public async Task<IEnumerable<Item>> GetLowStockItemsAsync(int thresholdQuantity)
        {
            try
            {
                var items = await _dataGateway.GetAllItemsAsync();

              
                Console.WriteLine($"Retrieved {items.Count()} items from data gateway.");

              
                var lowStockItems = items.Where(item => item.Quantity <= thresholdQuantity).ToList();

               
                Console.WriteLine($"Found {lowStockItems.Count()} low stock items.");

                return lowStockItems;
            }
            catch (Exception ex)
            {
             
                Console.WriteLine("Error getting low stock items: " + ex.Message);
                throw;
            }
        }
    }
}