using LoadFit.Core.Entities;

namespace LoadFit.APIs.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }         // Id For OrderItem
        public int ProductId { get; set; }  // Id For Product
        public string ProductName { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public TypeOfMaterial MaterialType { get; set; }
        public TypeOfFragility FragilityType { get; set; }
        public int Quantity { get; set; }
    }
}