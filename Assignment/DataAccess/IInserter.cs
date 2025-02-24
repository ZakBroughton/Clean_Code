using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment.DataAccess
{
    // This interface illustrates the Interface Segregation Principle
    public interface IInserter<T>
    {
       

        Task<int> InsertAsync(T itemToInsert, CancellationToken cancellationToken = default);

    }
}