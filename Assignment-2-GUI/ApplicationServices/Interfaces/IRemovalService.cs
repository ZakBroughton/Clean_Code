using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_GUI.ApplicationServices.Interfaces
{
    public interface IRemovalService
    {
        Task<string> RemoveQuantityAsync(string employeeName, int itemId, int quantityToRemove, double itemPrice);
    }
}