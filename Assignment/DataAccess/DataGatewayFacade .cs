using Assignment.DataAccess;
using Assignment.DTO;
using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DataAccess
{

    /* 
    This class, DataGatewayFacade, showcases adherence to SOLID principles:
    - Liskov Substitution Principle (LSP) by utilizing different factories to create specific objects without detailed knowledge of their implementations.
    - Dependency Inversion Principle (DIP) by relying on abstractions (interfaces) to define behavior and method return types.
    
    
    */
    public class DataGatewayFacade : IDataGatewayFacade
    {
        public async Task<int> AddEmployee(Employee e)
        {
            return await new DatabaseOperationFactoryForEmployee().CreateInserter().InsertAsync(e);
        }

        public async Task<int> AddItem(Item i)
        {
            return await new DatabaseOperationFactoryForItem().AddItem().InsertAsync(i);
        }

        public Task<Item> FindItemByName(string itemName)
        {
            return new DatabaseOperationFactoryForItem().CreateSelector(DatabaseOperationFactoryForItem.SELECT_BY_NAME, itemName).SelectAsync();
        }

        public Task<List<Item>> GetAllItems()
        {
            return new DatabaseOperationFactoryForItem().CreateSelector(DatabaseOperationFactoryForItem.SELECT_ALL).SelectAsync();
        }

        public Task<List<Employee>> GetAllEmployee()
        {
            return new DatabaseOperationFactoryForEmployee().CreateSelector(DatabaseOperationFactoryForEmployee.SELECT_ALL).SelectAsync();
        }

        public void  InitialiseMySqlDatabase()
        {
            new DatabaseInitialiser().Initialise();
        }

        public async Task<int> AddTransactionLog(TransactionDTO transactionDTO)
        {
            return await new DatabaseOperationFactoryForTransactionLog().CreateInserter().InsertAsync(transactionDTO);
        }

        public Task<Employee> FindEmployee(string employeeName)
        {
            return new DatabaseOperationFactoryForEmployee().CreateSelector(DatabaseOperationFactoryForEmployee.SELECT_BY_NAME, employeeName).SelectAsync();
        }

        public Task<Item> FindItemById(int id)
        {
            return new DatabaseOperationFactoryForItem().CreateSelector(DatabaseOperationFactoryForItem.SELECT_BY_ID, id).SelectAsync();
        }

        Task<List<TransactionDTO>> IDataGatewayFacade.GetAllTransactionLog()
        {
            return new DatabaseOperationFactoryForTransactionLog().CreateSelector(DatabaseOperationFactoryForTransactionLog.TRANSACTION_LOG).SelectAsync();
        }

        Task<List<TransactionDTO>> IDataGatewayFacade.FindPersonalUsageReport(string employeeName)
        {
            return new DatabaseOperationFactoryForTransactionLog().CreatePersonalUsageReportSelector(DatabaseOperationFactoryForTransactionLog.PERSONAL_USAGE_REPORT, employeeName).SelectAsync();
        }

        Task<TransactionDTO> IDataGatewayFacade.FindInventoryReport()
        {
            return new DatabaseOperationFactoryForTransactionLog().CreateInventoryReportSelector(DatabaseOperationFactoryForTransactionLog.INVENTORY_REPORT).SelectAsync();
        }

        Task<List<TransactionDTO>> IDataGatewayFacade.FindFinancialReport()
        {
            return new DatabaseOperationFactoryForTransactionLog().CreateFinancialReportSelector(DatabaseOperationFactoryForTransactionLog.FINANCIAL_REPORT);
        }

        public async Task<int> AddQuantity(int itemId, int quantityToAdded)
        {
            Item itemAddQuantity = await FindItemById(itemId);
            return await new DatabaseOperationFactoryForItem().CreateAddQuantityUpdater(quantityToAdded).UpdateAsync(itemAddQuantity);
        }

        public async Task<int> RemoveQuantity(int itemId, int quantityToRemove)
        {
            Item itemRemoveQuantity = await FindItemById(itemId);
            return await new  DatabaseOperationFactoryForItem().CreateRemoveQuantityUpdater(quantityToRemove).UpdateAsync(itemRemoveQuantity);
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await new DatabaseOperationFactoryForItem().CreateSelectorAsync(DatabaseOperationFactoryForItem.SELECT_ALL_ASYNC);
        }

        public async Task<List<TransactionDTO>> GetAllTransactionLogServerAsync()
        {
            return await new DatabaseOperationFactoryForTransactionLog().GetAllTransactionLogServerAsync(DatabaseOperationFactoryForTransactionLog.TRANSACTION_LOG_SERVER);
        }
    }
}