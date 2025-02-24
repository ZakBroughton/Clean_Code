using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment_2_GUI.ViewModels
{
    public partial class MainWindowViewModel
    {
        private void ExecuteAddItem()
        {
            var addItemForm = new AddItemForm(_itemService);
            addItemForm.Closed += async (sender, args) =>
            {
                await UpdateLowStockItems();
                await UpdateTotalInventoryValue();
                await UpdateRecentTransactionsAsync();
            };
            addItemForm.ShowDialog();
        }
        private void ExecuteAddQuantity()
        {
            var addQuantityForm = new AddQuantityForm(_quantityService);
            
            addQuantityForm.Closed += async (sender, args) =>
            {
                await UpdateLowStockItems();
                await UpdateTotalInventoryValue();
                await UpdateRecentTransactionsAsync();
            };
            addQuantityForm.ShowDialog();
        }

        private void ExecuteTakeQuantity()
        {
            var removeQuantityForm = new RemoveQuantityForm(_removalService);
            removeQuantityForm.Closed += async (sender, args) =>
            {
                await UpdateLowStockItems();
                await UpdateTotalInventoryValue();
                await UpdateRecentTransactionsAsync();
            };
            removeQuantityForm.ShowDialog();
        }

        private async Task UpdateLowStockItems()
        {
            var lowStockItems = await _dashboardService.GetLowStockItemsAsync(5);
            Application.Current.Dispatcher.Invoke(() =>
            {
                LowStockItems.Clear();
                foreach (var item in lowStockItems)
                {
                    LowStockItems.Add(item);
                }
            });
        }



        private async Task UpdateTotalInventoryValue()
        {
            TotalInventoryValue = await _dashboardService.CalculateTotalInventoryValueAsync();
            OnPropertyChanged(nameof(TotalInventoryValue));
        }
        private async Task UpdateRecentTransactionsAsync()
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

    }
}