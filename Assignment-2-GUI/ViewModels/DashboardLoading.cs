using Assignment.DataAccess;
using Assignment_2_GUI.ApplicationServices.Implementations;
using Assignment_2_GUI.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
namespace Assignment_2_GUI.ViewModels
{
    // Contains asynchronous methods to load dashboard data including recent transactions, low stock items, and total inventory value.
    // LoadDashboardDataAsync: Orchestrates concurrent fetching of transactions, low stock items, and inventory value updates.
    // LoadRecentTransactionsAsync: Retrieves and updates the UI with the list of the most recent transactions using thread-safe operations.
    // LoadLowStockItemsAsync: Fetches items with stock levels below a predefined threshold and updates the UI, handling exceptions with user alerts.
    // LoadTotalInventoryValueAsync: Calculates and updates the total value of the inventory and notifies the UI of changes to display updated data.
    public partial class MainWindowViewModel
    {



        private async Task LoadDashboardDataAsync()
        {
            await Task.WhenAll(
                LoadRecentTransactionsAsync(),
                LoadLowStockItemsAsync(),
                LoadTotalInventoryValueAsync());
        }

        private async Task LoadRecentTransactionsAsync()
        {
            var recentTransactions = await _dashboardService.GetRecentTransactionsAsync(10);
            Application.Current.Dispatcher.Invoke(() =>
            {
                RecentTransactions.Clear();
                foreach (var transaction in recentTransactions)
                {
                    RecentTransactions.Add(transaction);
                }
            });
        }

        private async Task LoadLowStockItemsAsync()
        {
            int thresholdQuantity = 5;
            try
            {
                var lowStockItems = await _dashboardService.GetLowStockItemsAsync(thresholdQuantity);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    LowStockItems.Clear();
                    foreach (var item in lowStockItems)
                    {
                        LowStockItems.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Error loading low stock items: " + ex.Message);
            }
        }

        private async Task LoadTotalInventoryValueAsync()
        {
            TotalInventoryValue = await _dashboardService.CalculateTotalInventoryValueAsync();
            OnPropertyChanged(nameof(TotalInventoryValue)); 
        }
    }
}