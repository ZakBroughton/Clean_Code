using Assignment.Models;
using MySqlConnector;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment.DataAccess
{
    public class InsertEmployee : DatabaseInserter<Employee>
    {
        protected override string GetSQL()
        {
            return
                "INSERT INTO Employee (EmployeeName) " +
                "VALUES (@employee_name)";
        }

      
        protected override async Task<int> DoInsertAsync(MySqlCommand command, Employee employeeToInsert, CancellationToken cancellationToken)
        {
            command.Parameters.AddWithValue("@employee_name", employeeToInsert.EmployeeName);
            await command.PrepareAsync(cancellationToken);

            int numRowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);

            if (numRowsAffected != 1)
            {
                throw new Exception("ERROR: Employee not inserted");
            }
            return numRowsAffected;
        }
    }
}