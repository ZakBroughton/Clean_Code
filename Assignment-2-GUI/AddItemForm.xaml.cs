using Assignment.DataAccess;
using Assignment;
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
using Assignment.DTO;
using Assignment.Models;
using Assignment_2_GUI.ViewModels;
using Assignment_2_GUI.ApplicationServices.Interfaces;

namespace Assignment_2_GUI
{
    /// <summary>
    /// Interaction logic for AddItemForm.xaml
    /// </summary>
    public partial class AddItemForm : Window
    {
        public AddItemForm(IItemService itemService)
        {
            InitializeComponent();
            var viewModel = new AddItemViewModel(itemService);
            viewModel.RequestClose += Close;
            this.DataContext = viewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Clean up the event subscription
            if (DataContext is AddItemViewModel viewModel)
            {
                viewModel.RequestClose -= Close;
            }
        }
    }
}