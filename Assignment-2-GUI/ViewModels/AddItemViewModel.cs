using Assignment.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment_2_GUI.ViewModels.MainWindowViewModel;
using System.Windows.Input;
using Assignment.DTO;
using Assignment.Models;
using Assignment;
using System.Windows.Controls;
using System.Windows;
using System.Runtime.CompilerServices;
using Assignment_2_GUI.ApplicationServices.Interfaces;

namespace Assignment_2_GUI.ViewModels
{
    public class AddItemViewModel : INotifyPropertyChanged
    {
        private readonly IItemService _itemService;
        private string _employeeName;
        private string _itemName;
        private int _quantity;
        private double _price;
        private string _message;

        public string EmployeeName
        {
            get => _employeeName;
            set => SetProperty(ref _employeeName, value);
        }

        public string ItemName
        {
            get => _itemName;
            set => SetProperty(ref _itemName, value);
        }

        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        public double Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public event Action RequestClose;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddItemCommand { get; }

        public AddItemViewModel(IItemService itemService)
        {
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
            AddItemCommand = new RelayCommand(async () => await AddItemAsync());
        }

        private async Task AddItemAsync()
        {
            try
            {
                Message = await _itemService.AddItemAsync(EmployeeName, ItemName, Quantity, Price);
                if (Message == "Item Added.") 
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