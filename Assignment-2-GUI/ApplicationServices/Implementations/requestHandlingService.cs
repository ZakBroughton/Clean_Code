using Assignment.DTO;
using Assignment;
using Assignment_2_GUI.ApplicationServices.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.DataAccess;

namespace Assignment_2_GUI.ApplicationServices.Implementations
{
    // Implements IRequestHandlingService to manage and process asynchronous service requests.
    // This service enqueues requests and processes them sequentially to ensure thread safety and data integrity.
    // It interfaces with a data gateway facade to handle operations like adding, updating, or removing items based on the request type.
    // The class supports different types of requests and ensures that each one is handled appropriately, logging operations and managing exceptions.
    public class RequestHandlingService : IRequestHandlingService
    {
        private readonly ConcurrentQueue<ServiceRequest> _requestQueue = new ConcurrentQueue<ServiceRequest>();
        private readonly object _lock = new object(); 
        private readonly IDataGatewayFacade _dataGatewayFacade; 

        public RequestHandlingService(IDataGatewayFacade dataGatewayFacade)
        {
            _dataGatewayFacade = dataGatewayFacade ?? throw new ArgumentNullException(nameof(dataGatewayFacade));
        }
        public async Task EnqueueRequestAsync(ServiceRequest request)
        {
            _requestQueue.Enqueue(request);
            await ProcessQueueAsync();
        }

        private async Task ProcessQueueAsync()
        {
            // Ensure only one processing thread is active at a time
            ServiceRequest request;
            while (_requestQueue.TryDequeue(out request))
            {
                // Process each request outside of the lock
                await ProcessRequestAsync(request);
            }
        }

        private async Task ProcessRequestAsync(ServiceRequest request)
        {
            switch (request.Type)
            {
                case RequestType.AddItem:
                    var addItemRequest = (AddItemRequest)request.Data;
                    await AddItemAsync(addItemRequest);
                    break;
                case RequestType.AddQuantity:
                    var addQuantityRequest = (AddQuantityRequest)request.Data;
                    await AddQuantityAsync(addQuantityRequest);
                    break;
                case RequestType.RemoveQuantity:
                    var removeQuantityRequest = (RemoveQuantityRequest)request.Data;
                    await RemoveQuantityAsync(removeQuantityRequest);
                    break;
                default:
                    // Handle unknown or unsupported requests
                    Console.WriteLine($"Unsupported request type: {request.Type}");
                    break;
            }
        }

        // Placeholder methods for handling each type of request
        private async Task<string> AddItemAsync(AddItemRequest request)
        {
            try
            {
                // Perform validation if needed
                if (string.IsNullOrWhiteSpace(request.EmployeeName))
                {
                    return "ERROR: Employee name is required.";
                }

                var employee = await _dataGatewayFacade.FindEmployee(request.EmployeeName);
                if (employee == null)
                {
                    return "ERROR: Employee not found.";
                }

                if (request.Price < 0)
                {
                    return "ERROR: Price must be positive.";
                }

              
                var itemToAdd = new Item(request.ItemName, request.Price, request.Quantity, DateTime.Now);

                var addedItemId = await _dataGatewayFacade.AddItem(itemToAdd);

            
                await _dataGatewayFacade.AddTransactionLog(new TransactionDTO(
                    "Item Added", addedItemId, request.ItemName, request.Price, request.Quantity, request.EmployeeName, DateTime.Now));


                return "Item added successfully.";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }

        }

        private async Task<string> AddQuantityAsync(AddQuantityRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.EmployeeName))
                {
                    return "ERROR: Employee name is required.";
                }

                var employee = await _dataGatewayFacade.FindEmployee(request.EmployeeName);
                if (employee == null)
                {
                    return "ERROR: Employee not found.";
                }

                if (request.ItemId <= 0 || request.QuantityToAdd <= 0)
                {
                    return "ERROR: Invalid item ID or quantity.";
                }

                if (request.Price < 0)
                {
                    return "ERROR: Price must be non-negative.";
                }

                var item = await _dataGatewayFacade.FindItemById(request.ItemId);
                if (item == null)
                {
                    return "ERROR: Item not found.";
                }

              
                item.Quantity += request.QuantityToAdd;

              
                await _dataGatewayFacade.AddQuantity(request.ItemId, request.QuantityToAdd);

                var transactionLog = new TransactionDTO("Quantity Added", request.ItemId, item.ItemName, request.Price, request.QuantityToAdd, request.EmployeeName, DateTime.Now);
                await _dataGatewayFacade.AddTransactionLog(transactionLog);

                return "Quantity updated successfully.";
            }

            catch (Exception ex)
            {
                return "Error adding Qunantity item: " + ex.Message;
            }
        }

        private async Task<string> RemoveQuantityAsync(RemoveQuantityRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.EmployeeName))
                {
                    return "ERROR: Employee name is required.";
                }

                var employee = await _dataGatewayFacade.FindEmployee(request.EmployeeName);
                if (employee == null)
                {
                    return "ERROR: Employee not found.";
                }

                if (request.ItemId <= 0 || request.QuantityToRemove <= 0)
                {
                    return "ERROR: Invalid item ID or quantity.";
                }

                if (request.ItemPrice < 0)
                {
                    return "ERROR: Price must be non-negative.";
                }

                var item = await _dataGatewayFacade.FindItemById(request.ItemId);
                if (item == null)
                {
                    return "ERROR: Item not found.";
                }

                
                if (item.Quantity < request.QuantityToRemove)
                {
                    return "ERROR: Insufficient quantity in stock.";
                }
                item.Quantity -= request.QuantityToRemove;

                await _dataGatewayFacade.RemoveQuantity(request.ItemId, request.QuantityToRemove);

                var transactionLog = new TransactionDTO("Quantity Removed", request.ItemId, item.ItemName, request.ItemPrice, request.QuantityToRemove, request.EmployeeName, DateTime.Now);
                await _dataGatewayFacade.AddTransactionLog(transactionLog);

                return "Quantity updated successfully.";
            }
            catch (Exception ex)
            {
                return "Error removing Qunantity item: " + ex.Message;
            }


        }
       

    }
}
