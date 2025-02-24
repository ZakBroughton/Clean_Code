using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class NullSelector<T> : ISelector<T>
    {
       

        public Task<T> SelectAsync()
        {
            throw new Exception("Selection not supported");
        }
    }
}
