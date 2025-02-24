using Assignment.DataAccess;
using Assignment.DTO;
using Assignment_2_GUI.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_GUI.ApplicationServices.Implementations
{
    public class RemovalService : IRemovalService
    {
        private readonly IDataGatewayFacade _dataGatewayFacade;
        private readonly IRequestHandlingService _requestHandlingService;

        public RemovalService(IDataGatewayFacade dataGatewayFacade, IRequestHandlingService requestHandlingService) // Inject IRequestHandlingService
        {
            _dataGatewayFacade = dataGatewayFacade ?? throw new ArgumentNullException(nameof(dataGatewayFacade));
            _requestHandlingService = requestHandlingService ?? throw new ArgumentNullException(nameof(requestHandlingService)); // Initialize _requestHandlingService
        }


        public async Task<string> RemoveQuantityAsync(string employeeName, int itemId, int quantityToRemove, double itemPrice)
        {
            try
            {


                var removeQuantityRequest = new RemoveQuantityRequest
                {
                    EmployeeName = employeeName,
                    ItemId = itemId,
                    QuantityToRemove = quantityToRemove,
                    ItemPrice = itemPrice


                };


                var serviceRequest = new ServiceRequest(RequestType.RemoveQuantity, removeQuantityRequest);
                await _requestHandlingService.EnqueueRequestAsync(serviceRequest);

                // Return a success message
                return "Quantity updated successfully.";  // Make sure this message matches exactly what the ViewModel checks
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";  // Proper error handling
            }
        }
    }
}