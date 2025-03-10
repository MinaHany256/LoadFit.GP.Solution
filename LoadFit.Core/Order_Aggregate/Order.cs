using LoadFit.Core.Entities;
using LoadFit.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, /*OrderStatus status,*/ Address shippingAddress, Vehicle vehicle, ICollection<OrderItem> items, decimal volumePricePerCubicMeter, string paymentIntentId, string clientSecret)
        {
            BuyerEmail = buyerEmail;
            // Status = status;
            ShippingAddress = shippingAddress;
            Vehicle = vehicle;
            //DeliveryMethod = deliveryMethod;
            Items = items;
            VolumePricePerCubicMeter = volumePricePerCubicMeter;
            PaymentIntentId = paymentIntentId;
            ClientSecret = clientSecret;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; }

        public Address ShippingAddress { get; set; }   // One to One Total 2 Sides

        
        public Vehicle Vehicle { get; set; }           // relation 1 [Order][Mandatory] => [Vehicle][Optional] 1

        
        //public DeliveryMethod DeliveryMethod { get; set; } // relation 1 [Order][Mandatory] => [DeliveryMethod][Optional] 1

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();


        // Price per cubic meter (Adjust based on business needs)
        public decimal VolumePricePerCubicMeter { get; set; } = 200;

        [NotMapped]
        public decimal TotalVolume
        {
            get => Items.Sum(item => item.Volume);
            private set { } // Ensure EF Core does not complain
        }

        [NotMapped]
        public decimal Subtotal
        {
            get => OrderHelper.CalculateSubtotal(Vehicle?.price ?? 0, TotalVolume / 1_000_000m, VolumePricePerCubicMeter);
            private set { }
        }

        [NotMapped]
        public decimal TotalPrice
        {
            get => Subtotal /*+ (DeliveryMethod?.Cost ?? 0)*/;
            private set { }
        }


        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

    }
}
