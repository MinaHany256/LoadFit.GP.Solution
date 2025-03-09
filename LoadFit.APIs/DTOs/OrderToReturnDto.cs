using LoadFit.Core.Entities;
using LoadFit.Core.Order_Aggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadFit.APIs.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public string Status { get; set; }

        public Address ShippingAddress { get; set; }   

        public string Vehicle { get; set; }         
        
        public decimal VehicleCost { get; set; }           

        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();


        public decimal Subtotal { get; set; }

        public decimal TotalPrice { get; set; }


        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
