using Assignment.DataAccess;
using Assignment_2_GUI.ViewModels;
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
    /// Interaction logic for TransactionLogWindow.xaml
    /// </summary>
    public partial class TransactionLogWindow : Window
    {
        private readonly TransactionLogWindowViewModel _viewModel;
        private TransactionLogWindowViewModel ViewModel => DataContext as TransactionLogWindowViewModel;



        public TransactionLogWindow(TransactionLogWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            LoadTransactionLog();
        }

        private void LoadTransactionLog()
        {
            try
            {
                _viewModel.LoadTransactionsFromServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transaction log: {ex.Message}");
            }
        }
        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel?.FilterTransactions();
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel?.FilterTransactions();
        }
    }
}