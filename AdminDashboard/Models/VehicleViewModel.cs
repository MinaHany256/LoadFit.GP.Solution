using LoadFit.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class VehicleViewModel
    {
        [Required(ErrorMessage = "Id is Required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public string Description { get; set; }
		public IFormFile Image { get; set; }
		public string? PictureUrl { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(1,100000)]
        public decimal price { get; set; }     
        public int MaxWeight { get; set; }

        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }


        public int BrandId { get; set; }                 // Forgien Key
        public VehicleBrand? Brand { get; set; }          // Navigation Property [ONE]


        public int TypeId { get; set; }                 // Forgien Key
        public VehicleType? Type { get; set; }            // Navigation Property [ONE]

        public int? DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
