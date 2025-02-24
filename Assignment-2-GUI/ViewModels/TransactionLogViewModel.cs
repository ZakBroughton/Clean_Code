using Assignment.DataAccess;
using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Assignment_2_GUI.ViewModels
{
    public class TransactionLogWindowViewModel : INotifyPropertyChanged
    {
        private readonly IDataGatewayFacade _dataGatewayFacade;
        private ObservableCollection<TransactionDTO> _allTransactions;
        private ObservableCollection<TransactionDTO> _filteredTransactions;
        private DateTime _startDate = DateTime.Today.AddDays(-8); 
        private DateTime _endDate = DateTime.Today; 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TransactionLogWindowViewModel(IDataGatewayFacade dataGatewayFacade)
        {
            _dataGatewayFacade = dataGatewayFacade;
            LoadTransactionsFromServer();
        }

        public ObservableCollection<TransactionDTO> FilteredTransactions
        {
            get { return _filteredTransactions; }
            set
            {
                if (_filteredTransactions != value)
                {
                    _filteredTransactions = value;
                    OnPropertyChanged(nameof(FilteredTransactions));
                }
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                    FilterTransactions();
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged();
                    FilterTransactions();
                }
            }
        }

        public string SelectedTransactionType { get; set; }
        public List<TransactionDTO> TransactionTypes { get; set; }

        public async Task LoadTransactionsFromServer()
        {
            try
            {
                var transactions = await _dataGatewayFacade.GetAllTransactionLogServerAsync();
                _allTransactions = new ObservableCollection<TransactionDTO>(transactions);
                FilterTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}");
            }
        }

        public void FilterTransactions()
        {
            if (_allTransactions == null) return;

            // Adjust the EndDate to include logs up to the end of the current day
            DateTime adjustedEndDate = EndDate.Date.AddDays(1).AddSeconds(-1);

            var filtered = _allTransactions.Where(t => t.DateAdded >= StartDate && t.DateAdded <= adjustedEndDate);

            if (!string.IsNullOrEmpty(SelectedTransactionType))
            {
                filtered = filtered.Where(t => t.TypeOfTransaction == SelectedTransactionType);
            }

            FilteredTransactions = new ObservableCollection<TransactionDTO>(filtered);
        }
    }
}