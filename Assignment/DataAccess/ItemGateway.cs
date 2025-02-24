using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class ItemGateway : DatabaseGateWay
    {

        protected override string InsertionSQL { get; } = "INSERT INTO Items (ItemName, Quantity, ItemPrice ,EmployeeName) VALUES (@name, @quantity,@itemPrice ,@employeeName)";


        protected override async Task DoInsertionAsync(MySqlCommand command, object objectToInsert, CancellationToken cancellationToken)
        {
            if (objectToInsert is Item item)
            {
                command.Parameters.AddWithValue("@name", item.ItemName);
                command.Parameters.AddWithValue("@quantity", item.Quantity);
                command.Parameters.AddWithValue("@itemPrice", item.ItemPrice);

                // Ensure the connection is open
                if (command.Connection.State != System.Data.ConnectionState.Open)
                {
                    await command.Connection.OpenAsync(cancellationToken).ConfigureAwait(false);
                }

                // Execute the command asynchronously
                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentException("Object to insert must be of type Item", nameof(objectToInsert));
            }
        }
    }
}