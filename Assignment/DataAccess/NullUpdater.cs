using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    // This interface illustrates the Interface Segregation Principle
    public class NullUpdater<T> : IUpdater<T>
    {
        

        public Task<int> UpdateAsync(T itemToUpdate)
        {
            throw new Exception("Update operation not supported");
        }
    }
}
