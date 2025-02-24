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
    public class AddQuantityFormViewModel : INotifyPropertyChanged
    {
        private readonly IQuantityService _quantityService;
        private string _employeeName;
        private int _itemId;
        private int _quantityToAdd;
        private double _itemPrice;
        private string _message;

        public string EmployeeName
        {
            get => _employeeName;
            set => SetProperty(ref _employeeName, value);
        }

        public int ItemId
        {
            get => _itemId;
            set => SetProperty(ref _itemId, value);
        }

        public int QuantityToAdd
        {
            get => _quantityToAdd;
            set => SetProperty(ref _quantityToAdd, value);
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

        public ICommand AddQuantityCommand { get; }

        public AddQuantityFormViewModel(IQuantityService quantityService)
        {
            _quantityService = quantityService ?? throw new ArgumentNullException(nameof(quantityService));
            AddQuantityCommand = new RelayCommand(async () => await UpdateQuantity());
        }

        private async Task UpdateQuantity()
        {
            try
            {
                // Use the quantity service to add quantity, service handles validation and business logic
                Message = await _quantityService.AddQuantityAsync(EmployeeName, ItemId, QuantityToAdd, ItemPrice);
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
