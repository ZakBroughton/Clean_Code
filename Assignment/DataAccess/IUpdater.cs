﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    // This interface illustrates the Interface Segregation Principle
    public interface IUpdater<T>
    {
        
        Task<int> UpdateAsync(T itemToUpdate);
    }
}
