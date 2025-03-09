using System.ComponentModel.DataAnnotations;

namespace LoadFit.APIs.DTOs
{
    public class RegisterVehicleDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int MaxWeight { get; set; }
        
        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public int BrandId { get; set; }  // Foreign Key
        public int TypeId { get; set; }   // Foreign Key
    }
}
