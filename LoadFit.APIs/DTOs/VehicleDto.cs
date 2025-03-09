using LoadFit.Core.Entities;

namespace LoadFit.APIs.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }          // Navigation Property [ONE]
        public string Type { get; set; }            // Navigation Property [ONE]
        public string Model { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal price { get; set; }     // Cost per trip or per km
        public int MaxWeight { get; set; }

        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public string DriverName { get; set; }  // Ensure this exists
        public bool IsRecommended { get; set; } = false;

        public int BrandId { get; set; }                 // Forgien Key
        
        public int TypeId { get; set; }                 // Forgien Key
        

        public int? DriverId { get; set; }  // Ensure this exists
        

        
        


    }
}
