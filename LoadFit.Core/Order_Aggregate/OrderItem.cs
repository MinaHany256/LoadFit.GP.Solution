using LoadFit.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }

        public OrderItem(int productId, string productName, decimal length, decimal width, decimal height, TypeOfMaterial materialType, TypeOfFragility fragilityType, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            Length = length;
            Width = width;
            Height = height;
            MaterialType = materialType;
            FragilityType = fragilityType;
            Quantity = quantity;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public TypeOfMaterial MaterialType { get; set; }
        public TypeOfFragility FragilityType { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public decimal Volume
        {
            get => Length * Width * Height * Quantity;
            private set { }  // Prevents EF Core migration errors
        }

        [NotMapped]
        public decimal Weight
        {
            get => Volume * (decimal)MaterialType;
            private set { }  // Prevents EF Core migration errors
        }
    }
}
