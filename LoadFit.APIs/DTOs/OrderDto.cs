using LoadFit.Core.Order_Aggregate;
using System.ComponentModel.DataAnnotations;

namespace LoadFit.APIs.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        public AddressDto shippingAddress { get; set; }
    }
}
