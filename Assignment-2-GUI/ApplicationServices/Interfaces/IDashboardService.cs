using Assignment.DTO;
using Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_GUI.ApplicationServices.Interfaces
{
    // Defines an interface for dashboard-related services in a GUI application.
    // This interface mandates methods for calculating the total value of inventory asynchronously,
    // retrieving a specified number of recent transactions, and identifying items with stock below a certain threshold.
    public interface IDashboardService
    {
        Task<decimal> CalculateTotalInventoryValueAsync();
        Task<IEnumerable<TransactionDTO>> GetRecentTransactionsAsync(int numberOfTransactions);
        Task<IEnumerable<Item>> GetLowStockItemsAsync(int thresholdQuantity);
    }
}