using Assignment.DataAccess;
using Assignment.DTO;
using Assignment;
using Assignment_2_GUI.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_GUI.ApplicationServices.Implementations
{

    public class ItemService : IItemService
    {
        private readonly IDataGatewayFacade _dataGatewayFacade;
        private readonly IRequestHandlingService _requestHandlingService; 

        public ItemService(IDataGatewayFacade dataGatewayFacade, IRequestHandlingService requestHandlingService) 
        {
            _dataGatewayFacade = dataGatewayFacade ?? throw new ArgumentNullException(nameof(dataGatewayFacade));
            _requestHandlingService = requestHandlingService ?? throw new ArgumentNullException(nameof(requestHandlingService)); 
        }

        public async Task<string> AddItemAsync(string employeeName, string itemName, int quantity, double price)
        {
            try
            {
                // Create a request for adding an item
                var addItemRequest = new AddItemRequest
                {
                    EmployeeName = employeeName,
                    ItemName = itemName,
                    Quantity = quantity,
                    Price = price
                };

                // Enqueue the request
                var serviceRequest = new ServiceRequest(RequestType.AddItem, addItemRequest);
                await _requestHandlingService.EnqueueRequestAsync(serviceRequest);

              
                return "Item Added.";  
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}"; 
            }
        }
    }
}