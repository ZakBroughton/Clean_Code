using Assignment.DataAccess;
using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment_2_GUI.ViewModels.MainWindowViewModel;
using System.Windows.Input;
using Assignment;
using Assignment.Models;
using System.Windows;
using System.Runtime.CompilerServices;
using Assignment_2_GUI.ApplicationServices.Interfaces;

namespace Assignment_2_GUI.ViewModels
{
    public class RemoveQuantityFormViewModel : INotifyPropertyChanged
    {
        private readonly IRemovalService _removalService;
        private string _employeeName;
        private int _itemId;
        private int _quantityToRemove;
        private double _itemPrice;
        private string _message;

        public string EmployeeName
        {
            get => _employeeName;
            set => SetProperty(ref _employeeName, value);
        }

        public int ItemId
        {
            get => _itemId; set => SetProperty(ref _itemId, value);
        }

        public int QuantityToRemove
        {
            get => _quantityToRemove;
            set => SetProperty(ref _quantityToRemove, value);
        }

        public double ItemPrice
        {
            get => _itemPrice;
            set => SetProperty(ref _itemPrice, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public event Action RequestClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RemoveQuantityCommand { get; }

        public RemoveQuantityFormViewModel(IRemovalService removalService)
        {
            _removalService = removalService ?? throw new ArgumentNullException(nameof(removalService));
            RemoveQuantityCommand = new RelayCommand(async () => await UpdateQuantity());
        }

        private async Task UpdateQuantity()
        {
            try
            {
                Message = await _removalService.RemoveQuantityAsync(EmployeeName, ItemId, QuantityToRemove, ItemPrice);
                if (Message == "Quantity updated successfully.")
                {
                    RequestClose?.Invoke();
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR: " + ex.Message;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
