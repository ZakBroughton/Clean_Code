using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment_2_GUI.ViewModels
{
    public partial class MainWindowViewModel
    {
        private void ExecuteExitCommand()
        {
            Application.Current.Shutdown();
        }
    }
}