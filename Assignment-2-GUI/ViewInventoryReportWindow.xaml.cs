using Assignment;
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
    /// Interaction logic for ViewInventoryReportWindow.xaml
    /// </summary>
    public partial class ViewInventoryReportWindow : Window
    {
        private readonly IDataGatewayFacade dataGateway;
        public ViewInventoryReportWindow(IDataGatewayFacade dataGateway)
        {
            InitializeComponent();
            this.dataGateway = dataGateway;
            Loaded += async (s, e) => await LoadInventoryReport();
        }
        private async Task LoadInventoryReport()
        {
            try
            {
                var inventoryReport = await dataGateway.GetAllItems() ?? new List<Item>(); // Assuming GetAllItems() returns List<Item>

                // Clear existing columns and items
                reportDataGrid.Columns.Clear();
                reportDataGrid.Items.Clear();

                // Define columns for the inventory report
                reportDataGrid.Columns.Add(new DataGridTextColumn { Header = "Item ID", Binding = new Binding("ItemID") });
                reportDataGrid.Columns.Add(new DataGridTextColumn { Header = "Item Name", Binding = new Binding("ItemName") });
                reportDataGrid.Columns.Add(new DataGridTextColumn { Header = "Quantity", Binding = new Binding("Quantity") });

                // Add items to the DataGrid
                foreach (var item in inventoryReport)
                {
                    reportDataGrid.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory report: " + ex.Message);
            }
        }
    }
}