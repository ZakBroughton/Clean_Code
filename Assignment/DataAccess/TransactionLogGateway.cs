using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public class TransactionLogGateway : DatabaseGateWay
    {
        protected override string InsertionSQL { get; } = "INSERT INTO TransactionLogs (TypeOfTransaction,ItemID," +
            " ItemName, Quantity,ItemPrice , EmployeeName,  DateAdded) VALUES (@typeOfTransaction, @itemID, " +
            "@itemName,@itemPrice ,@quantity, @employeeName, @dateAdded)";

        public async Task AddTransactionLogAsync(TransactionLogEntry logEntry, CancellationToken cancellationToken = default)
        {
            using (MySqlConnection conn = GetMySQLConnection())
            {
                MySqlCommand command = new MySqlCommand(InsertionSQL, conn);
                command.Parameters.AddWithValue("@typeOfTransaction", logEntry.TypeOfTransaction);
                command.Parameters.AddWithValue("@itemID", logEntry.ItemID);
                command.Parameters.AddWithValue("@itemName", logEntry.ItemName);
                command.Parameters.AddWithValue("@quantity", logEntry.Quantity);
                command.Parameters.AddWithValue("@itemPrice", logEntry.ItemPrice);
                command.Parameters.AddWithValue("@employeeName", logEntry.EmployeeName);
                command.Parameters.AddWithValue("@dateAdded", logEntry.DateAdded);

                // Open connection asynchronously
                await conn.OpenAsync(cancellationToken).ConfigureAwait(false);

                // Use the async method for insertion
                await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }
        }

      

        protected override async Task DoInsertionAsync(MySqlCommand command, object objectToInsert, CancellationToken cancellationToken)
        {
            if (objectToInsert is TransactionLogEntry logEntry)
            {
                command.Parameters.AddWithValue("@typeOfTransaction", logEntry.TypeOfTransaction);
                command.Parameters.AddWithValue("@itemID", logEntry.ItemID);
                command.Parameters.AddWithValue("@itemName", logEntry.ItemName);
                command.Parameters.AddWithValue("@quantity", logEntry.Quantity);
                command.Parameters.AddWithValue("@itemPrice", logEntry.ItemPrice);
                command.Parameters.AddWithValue("@employeeName", logEntry.EmployeeName);
                command.Parameters.AddWithValue("@dateAdded", logEntry.DateAdded);

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
                throw new ArgumentException("Object to insert must be of type TransactionLogEntry", nameof(objectToInsert));
            }
        }
    }
}
