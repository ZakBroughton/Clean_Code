using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_GUI.ApplicationServices.Interfaces
{
    public interface IItemService
    {
        Task<string> AddItemAsync(string employeeName, string itemName, int quantity, double price);
    }

}