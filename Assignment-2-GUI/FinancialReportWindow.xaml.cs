using Assignment.DataAccess;
using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment_2_GUI
{
    /// <summary>
    /// Interaction logic for Financial_Report.xaml
    /// </summary>
    public partial class Financial_Report : Window
    {
        private readonly IDataGatewayFacade dataGateway;
        public Financial_Report(IDataGatewayFacade dataGateway)
        {
            InitializeComponent();
            this.dataGateway = dataGateway ?? throw new ArgumentNullException(nameof(dataGateway));
            Loaded += Financial_Report_Loaded;
        }
        private async void Financial_Report_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadFinancialReport();
        }

        private async Task LoadFinancialReport()
        {
            try
            {
                var financialReport = await dataGateway.FindFinancialReport();
                if (financialReport == null)
                {
                    throw new InvalidOperationException("FindFinancialReport returned null.");
                }

                var observableFinancialReport = new ObservableCollection<TransactionDTO>(financialReport);
                financialReportDataGrid.ItemsSource = observableFinancialReport;

                // Assuming TransactionDTO includes a TotalCost property
                var totalCost = financialReport.Sum(r => r.TotalCost);
                totalPriceTextBlock.Text = $"Total price of all items: £{totalCost:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading financial report: {ex.Message}");
            }
        }
    }
}