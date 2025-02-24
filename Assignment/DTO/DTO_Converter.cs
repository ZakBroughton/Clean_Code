using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Assignment.DTO
{
    public class DTO_Converter
    {
        public TransactionLogEntry Convert(TransactionDTO transactionDTO)
        {
            return new TransactionLogEntry(
                transactionDTO.TypeOfTransaction,
                transactionDTO.ItemID,
                transactionDTO.ItemName,
                transactionDTO.ItemPrice,
                transactionDTO.Quantity,
                transactionDTO.EmployeeName,

                transactionDTO.DateAdded
                
            );
        }

        public TransactionDTO Convert(TransactionLogEntry transaction)
        {
            return new TransactionDTO(
                transaction.TypeOfTransaction,
                transaction.ItemID,
                transaction.ItemName,
                transaction.ItemPrice,
                transaction.Quantity,
                transaction.DateAdded
            );
        }
    }
}