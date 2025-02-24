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
        private void ExecuteViewInventoryReportCommand()
        {
            if (_dataGateway == null)
            {
                MessageBox.Show("Data gateway is not available.");
                return;
            }

            try
            {
                ViewInventoryReportWindow viewInventoryReportWindow = new ViewInventoryReportWindow(_dataGateway);
                viewInventoryReportWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open the inventory report window: " + ex.Message);
            }
        }

        private void ExecuteViewFinancialReportCommand()
        {
            if (_dataGateway == null)
            {
                MessageBox.Show("Data gateway is not available.");
                return;
            }

            try
            {
                Financial_Report financialReportWindow = new Financial_Report(_dataGateway);
                financialReportWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open the financial report window: " + ex.Message);
            }
        }

        private async void ExecuteViewTransactionLogCommand()
        {
            if (_dataGateway == null)
            {
                MessageBox.Show("Data gateway is not available.");
                return;
            }

            try
            {
                TransactionLogWindowViewModel viewModel = new TransactionLogWindowViewModel(_dataGateway);

                viewModel.StartDate = DateTime.Today.AddDays(-7);
                viewModel.EndDate = DateTime.Today;

                var types = await _dataGateway.GetAllTransactionLogServerAsync();
                viewModel.TransactionTypes = types;

                TransactionLogWindow transactionLogWindow = new TransactionLogWindow(viewModel);
                transactionLogWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transaction log: {ex.Message}");
            }
        }

        private void ExecuteViewPersonalUsageReportCommand()
        {
            if (_dataGateway == null)
            {
                MessageBox.Show("Data gateway is not available.");
                return;
            }

            try
            {
                PersonalUsageReportWindow personalReportWindow = new PersonalUsageReportWindow(_dataGateway);
                personalReportWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open the personal usage report window: " + ex.Message);
            }
        }
    }
}