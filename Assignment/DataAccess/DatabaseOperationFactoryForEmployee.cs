using Assignment.DataAccess;
using Assignment.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace Assignment.DataAccess
{
    /* 
    This class, DatabaseOperationFactoryForEmployee, showcases adherence to SOLID principles:
    - Single Responsibility Principle (SRP) by managing the creation of database operations (insertions and selections) for Employee entities.
    - Interface Segregation Principle (ISP) by offering various methods for creating different selectors and inserters.
    
    Note: The code structure aligns with SRP and ISP by focusing on creation methods for specific database operations;
    */

    public class DatabaseOperationFactoryForEmployee
    {
        public const int SELECT_ALL = 1;
        public const int SELECT_BY_NAME = 2;




        public DatabaseInserter<Employee> CreateInserter()
        {
            return new InsertEmployee();
        }

        public ISelector<List<Employee>> CreateSelector(int typeOfSelection)
        {
            if (typeOfSelection == SELECT_ALL)
            {
                return new GetAllEmployees();
            }
            return new NullSelector<List<Employee>>();
        }

        public ISelector<Employee> CreateSelector(int typeOfSelection, string i)
        {
            switch (typeOfSelection)
            {
                case SELECT_BY_NAME:
                    return new FindEmployeeByName(i);
                default:
                    return new NullSelector<Employee>();
            }


        }
    }
}