using Assignment.Models;
using MySqlConnector;
using System;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class FindEmployeeByName : DatabaseSelector<Employee>
    {
        private readonly string employeeName;

        public FindEmployeeByName(string employeeName)
        {
            this.employeeName = employeeName;
        }

        protected override string GetSQL()
        {
            return "SELECT EmployeeName FROM Employee WHERE EmployeeName = @EmployeeName";
        }

        protected override async Task<Employee> DoSelectAsync(MySqlCommand command)
        {
            Employee employee = null;

            try
            {
                command.Parameters.AddWithValue("@EmployeeName", employeeName);
                command.Prepare();
                MySqlDataReader dr = await command.ExecuteReaderAsync();

                if (dr.Read())
                {
                    employee = new Employee(dr.GetString("EmployeeName"));
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: Retrieval of Employee failed", e);
            }

            return employee;
        }

        
    }
}