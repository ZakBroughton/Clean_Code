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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            // Code to add item to stock
        }

        private void AddQuantity_Click(object sender, RoutedEventArgs e)
        {
            // Code to add quantity to an item
        }

        // Add handlers for the rest of the buttons
        private void TakeQuantityFromItemClick(object sender, RoutedEventArgs e)
        {
            // Code to add quantity to an item
        }
        private void ViewInventoryReportClick(object sender, RoutedEventArgs e)
        {

        }
        private void ViewFinacialReportClick(object sender, RoutedEventArgs e)
        {
            // Code to add quantity to an item
        }

        
      
        private void ViewTransactionLogClick(object sender, RoutedEventArgs e)
        {
            // Code to add quantity to an item
        }

        private void ViewPersonalUsageReportClick(object sender, RoutedEventArgs e)
        {
            // Code to add quantity to an item
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

       
    }
}
