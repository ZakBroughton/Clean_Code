using Assignment;
using Assignment.DataAccess;
using Assignment.DTO;
using Assignment_2_GUI.ApplicationServices.Implementations;
using Assignment_2_GUI.ApplicationServices.Interfaces;
using Assignment_2_GUI.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_2_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Manually instantiate all the services required by MainWindowViewModel
            IDataGatewayFacade dataGateway = new DataGatewayFacade();
            IRequestHandlingService requestHandlingService = new RequestHandlingService(dataGateway); // Make sure to pass necessary parameters to the constructor
            IDashboardService dashboardService = new DashboardService(dataGateway);
            IItemService itemService = new ItemService(dataGateway, requestHandlingService); // Adjusted to include requestHandlingService
            IQuantityService quantityService = new QuantityService(dataGateway, requestHandlingService);
            IRemovalService removalService = new RemovalService(dataGateway, requestHandlingService);

            // Now pass all these services to the MainWindowViewModel
            viewModel = new MainWindowViewModel(dataGateway, itemService, quantityService, removalService, dashboardService);
            DataContext = viewModel;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await viewModel.InitializeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while initializing the main window: " + ex.Message);
            }
        }
    }
}