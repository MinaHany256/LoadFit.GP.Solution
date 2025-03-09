using LoadFit.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LoadFit.APIs.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Length { get; set; }

        [Required]
        public decimal Width { get; set; }

        [Required]
        public decimal Height { get; set; }

        [Required]
        public TypeOfMaterial MaterialType { get; set; }

        [Required]
        public TypeOfFragility FragilityType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one!")]
        public int Quantity { get; set; }
    }
}