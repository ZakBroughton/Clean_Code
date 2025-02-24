using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_GUI.ApplicationServices.Interfaces
{
    // Defines an interface and supporting types for handling asynchronous service requests in a GUI application.
    // IRequestHandlingService requires implementation of a method to enqueue and process different types of service requests.
    // Supported request types include adding items, adding quantity to existing items, and removing quantity from items,
    // each encapsulated with necessary data in specific request classes (AddItemRequest, AddQuantityRequest, RemoveQuantityRequest).

    public interface IRequestHandlingService
    {
        Task EnqueueRequestAsync(ServiceRequest request);
    }

    public enum RequestType
    {
        AddItem,
        AddQuantity,
        RemoveQuantity
    }

    public class ServiceRequest
    {
        public RequestType Type { get; set; }
        public object Data { get; set; }


        public ServiceRequest(RequestType type, object data)
        {
            Type = type;
            Data = data;
        }
    }

    public class AddItemRequest
    {
        public string EmployeeName { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ItemId { get; internal set; }
    }

    public class AddQuantityRequest
    {
        public string EmployeeName { get; set; }
        public int ItemId { get; set; }
        public int QuantityToAdd { get; set; }
        public double Price { get; set; }
    }

    public class RemoveQuantityRequest
    {
        public string EmployeeName { get; set; }
        public int ItemId { get; set; }
        public int QuantityToRemove { get; set; }
        public double ItemPrice { get; set; }
    }
}