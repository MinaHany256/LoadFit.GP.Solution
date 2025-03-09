using LoadFit.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LoadFit.APIs.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
