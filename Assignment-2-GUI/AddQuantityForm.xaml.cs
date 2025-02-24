using Assignment;
using Assignment.DataAccess;
using Assignment.DTO;
using Assignment.Models;
using Assignment_2_GUI.ApplicationServices.Interfaces;
using Assignment_2_GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for AddQuantityForm.xaml
    /// </summary>
    public partial class AddQuantityForm : Window
    {
        public AddQuantityForm(IQuantityService quantityService)
        {
            InitializeComponent();
            var viewModel = new AddQuantityFormViewModel(quantityService); // Updated to use IQuantityService
            viewModel.RequestClose += Close;
            this.DataContext = viewModel;
        }
    }
}