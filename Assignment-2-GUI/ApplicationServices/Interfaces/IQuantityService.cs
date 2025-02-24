using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_GUI.ApplicationServices.Interfaces
{
    public interface IQuantityService
    {
        Task<string> AddQuantityAsync(string employeeName, int itemId, int quantityToAdd, double itemPrice);
    }
}