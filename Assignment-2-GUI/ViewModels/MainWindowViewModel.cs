using Assignment.DataAccess;
using Assignment.DTO;
using Assignment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Assignment_2_GUI.ApplicationServices.Interfaces;
using Assignment_2_GUI.ApplicationServices.Implementations;

namespace Assignment_2_GUI.ViewModels
{
    // ViewModel for the MainWindow in a GUI application, implementing the INotifyPropertyChanged interface to support data binding.
    // This ViewModel manages data related to transactions, inventory items, and various reports, providing a connection between the UI and the data layer.
    // It initializes commands for UI actions like exiting the application, viewing different reports, and modifying inventory items.
    // It uses a DashboardPollingService to periodically refresh dashboard data, ensuring that the displayed information is up to date.
    // This class encapsulates all necessary services for item management, quantity adjustments, and removal operations, initialized through dependency injection in the constructor.

    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TransactionDTO> RecentTransactions { get; private set; }
        public ObservableCollection<Item> LowStockItems { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand ViewInventoryReportCommand { get; private set; }
        public ICommand ViewFinancialReportCommand { get; private set; }
        public ICommand ViewTransactionLogCommand { get; private set; }
        public ICommand ViewPersonalUsageReportCommand { get; private set; }
        public ICommand AddItemCommand { get; private set; }
        public ICommand AddQuantityCommand { get; private set; }
        public ICommand TakeQuantityCommand { get; private set; }
        public ICommand StartDateChangedCommand { get; private set; }
        public ICommand EndDateChangedCommand { get; private set; }
        public decimal TotalInventoryValue { get; set; }

        private readonly IDataGatewayFacade _dataGateway;
        private readonly IItemService _itemService;
        private readonly IQuantityService _quantityService;
        private readonly IRemovalService _removalService;
        private readonly IDashboardService _dashboardService;
        private DashboardPollingService _dashboardPollingService;

        // Corrected constructor with all dependencies
        public MainWindowViewModel(
            IDataGatewayFacade dataGateway,
            IItemService itemService,
            IQuantityService quantityService,
            IRemovalService removalService,
            IDashboardService dashboardService)
        {
            _dataGateway = dataGateway ?? throw new ArgumentNullException(nameof(dataGateway));
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
            _quantityService = quantityService ?? throw new ArgumentNullException(nameof(quantityService));
            _removalService = removalService ?? throw new ArgumentNullException(nameof(removalService));
            _dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService)); ;
           

            RecentTransactions = new ObservableCollection<TransactionDTO>();
            LowStockItems = new ObservableCollection<Item>();
            _dashboardPollingService = new DashboardPollingService(LoadDashboardDataAsync, TimeSpan.FromSeconds(10)); 
            _dashboardPollingService.Start();


            InitializeCommands();
        }
        private void InitializeCommands()
        {
            ExitCommand = new RelayCommand(ExecuteExitCommand);
            ViewInventoryReportCommand = new RelayCommand(ExecuteViewInventoryReportCommand);
            ViewFinancialReportCommand = new RelayCommand(ExecuteViewFinancialReportCommand);
            ViewTransactionLogCommand = new RelayCommand(ExecuteViewTransactionLogCommand);
            ViewPersonalUsageReportCommand = new RelayCommand(ExecuteViewPersonalUsageReportCommand);
            AddItemCommand = new RelayCommand(ExecuteAddItem);
            AddQuantityCommand = new RelayCommand(ExecuteAddQuantity);
            TakeQuantityCommand = new RelayCommand(ExecuteTakeQuantity);
            // Initialize other ICommand properties as needed
        }

        public async Task InitializeAsync()
        {
            if (_dashboardService == null)
            {
                throw new InvalidOperationException("_dashboardService is not initialized.");
            }
            await LoadDashboardDataAsync();
        }

        public void Dispose()
        {
            _dashboardPollingService?.Stop();
        }

    }
}