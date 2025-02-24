using Assignment.DataAccess;
using Assignment.DTO;
using Assignment_2_GUI.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_GUI.ApplicationServices.Implementations
{
    public class QuantityService : IQuantityService
    {

        private readonly IDataGatewayFacade _dataGatewayFacade;
        private readonly IRequestHandlingService _requestHandlingService; // Add this field

        public QuantityService(IDataGatewayFacade dataGatewayFacade, IRequestHandlingService requestHandlingService) // Inject IRequestHandlingService
        {
            _dataGatewayFacade = dataGatewayFacade ?? throw new ArgumentNullException(nameof(dataGatewayFacade));
            _requestHandlingService = requestHandlingService ?? throw new ArgumentNullException(nameof(requestHandlingService)); // Initialize _requestHandlingService
        }


        public async Task<string> AddQuantityAsync(string employeeName, int itemId, int quantityToAdd, double itemPrice)
        {
            try
            {
                var addQuantityRequest = new AddQuantityRequest
                {
                    EmployeeName = employeeName,
                    ItemId = itemId,
                    QuantityToAdd = quantityToAdd,
                    Price = itemPrice


                };


                var serviceRequest = new ServiceRequest(RequestType.AddQuantity, addQuantityRequest);
                await _requestHandlingService.EnqueueRequestAsync(serviceRequest);

          
                return "Quantity updated successfully.";  
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";  
            }
        }
    }
}
