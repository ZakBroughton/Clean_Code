using Assignment.DTO;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{
    public interface IDataGatewayFacade
    {
        Task<int> AddEmployee(Employee e);
        Task<int> AddItem(Item i);

        public Task<Item> FindItemById(int id);
        public Task<int> AddTransactionLog(TransactionDTO transactionDTO);

        public void InitialiseMySqlDatabase();


        public Task<int> AddQuantity(int itemId, int quantityToAddedd);
        public Task<int> RemoveQuantity(int itemId, int quantityToRemove);

        public Task<Employee> FindEmployee(string employeeName);

        Task<List<Item>> GetAllItems();
        Task<List<Item>> GetAllItemsAsync();

        public Task<List<Employee>> GetAllEmployee();
        Task<List<TransactionDTO>> GetAllTransactionLog();


        Task<List<TransactionDTO>> FindPersonalUsageReport(string employeeName);

        Task<TransactionDTO> FindInventoryReport();
      
        Task<List<TransactionDTO>> FindFinancialReport();
        Task<Item> FindItemByName(string itemName);

        Task<List<TransactionDTO>> GetAllTransactionLogServerAsync();
       
    }
}