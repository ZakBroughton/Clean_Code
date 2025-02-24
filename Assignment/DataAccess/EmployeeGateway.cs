using Assignment.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class EmployeeGateway : DatabaseGateWay
    {
        private EmployeeManager employeeManager;

        public EmployeeGateway()
        {
            employeeManager = new EmployeeManager();
        }

        protected override string InsertionSQL { get; } = "INSERT INTO Employees (EmployeeName) VALUES (@employeeName)";

        public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            employeeManager.AddEmployee(employee); // Assuming this is still synchronous. Adapt if necessary.

            using (MySqlConnection conn = GetMySQLConnection())
            {
                MySqlCommand command = new MySqlCommand(InsertionSQL, conn);
                command.Parameters.AddWithValue("@employeeName", employee.EmployeeName);

                // Ensure the connection is open
                if (command.Connection.State != System.Data.ConnectionState.Open)
                {
                    await command.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                }

                try
                {
                    await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    throw new Exception("ERROR: insertion of employee failed", e);
                }
            }
        }


        protected override async Task DoInsertionAsync(MySqlCommand command, object objectToInsert, CancellationToken cancellationToken)
        {
            if (objectToInsert is Employee employee)
            {
                command.Parameters.AddWithValue("@employeeName", employee.EmployeeName);

                // Ensure the connection is open
                if (command.Connection.State != System.Data.ConnectionState.Open)
                {
                    await command.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                }

                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentException("Object to insert must be of type Employee", nameof(objectToInsert));
            }
        }
    }
}
