using Assignment.DataAccess;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PersonalUsageReportWindow.xaml
    /// </summary>
    public partial class PersonalUsageReportWindow : Window
    {
        private readonly IDataGatewayFacade dataGateway;

        public PersonalUsageReportWindow(IDataGatewayFacade dataGateway)
        {
            InitializeComponent();
            this.dataGateway = dataGateway;
        }

        private void LoadReportClick(object sender, RoutedEventArgs e)
        {
            LoadPersonalUsageReport();
        }

        private async void LoadPersonalUsageReport()
        {
            string employeeName = employeeNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(employeeName))
            {
                MessageBox.Show("Please enter an employee name.");
                return;
            }

            try
            {
                var personalReport = await dataGateway.FindPersonalUsageReport(employeeName);
                reportDataGrid.Columns.Clear();
                reportDataGrid.Items.Clear();

                if (personalReport.Any())
                {
                    reportDataGrid.Columns.Add(new DataGridTextColumn { Header = "Date Taken", Binding = new Binding("DateAdded") { StringFormat = "dd/MM/yyyy" } });
                    reportDataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("ItemID") });
                    reportDataGrid.Columns.Add(new DataGridTextColumn { Header = "ItemName", Binding = new Binding("ItemName") });
                    reportDataGrid.Columns.Add(new DataGridTextColumn { Header = "Quantity Removed", Binding = new Binding("Quantity") });

                    foreach (var transaction in personalReport)
                    {
                        reportDataGrid.Items.Add(transaction);
                    }
                }
                else
                {
                    MessageBox.Show($"No personal report found for the employee: {employeeName}.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading personal usage report: {ex.Message}");
            }
        }
    }
}
