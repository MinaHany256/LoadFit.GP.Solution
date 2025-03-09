using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public Vehicle()
        {
            
        }

        public Vehicle(string name, string description, string pictureUrl, decimal price, int maxWeight, int brandId, VehicleBrand brand, int typeId, VehicleType type, int driverId, Driver driver)
        {
            Name = name;
            Description = description;
            PictureUrl = pictureUrl;
            this.price = price;
            MaxWeight = maxWeight;
            BrandId = brandId;
            Brand = brand;
            TypeId = typeId;
            Type = type;
            DriverId = driverId;
            Driver = driver;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal price { get; set; }     // Cost per trip or per km
        public int MaxWeight { get; set; }

        
        public decimal? Length { get; set; }  // in meters
        public decimal? Width { get; set; }   // in meters
        public decimal? Height { get; set; }  // in meters


        public int BrandId { get; set; }                 // Forgien Key
        public VehicleBrand Brand { get; set; }          // Navigation Property [ONE]


        public int TypeId { get; set; }                 // Forgien Key
        public VehicleType Type { get; set; }            // Navigation Property [ONE]



        // Foreign Key to Driver
        public int? DriverId { get; set; }

        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }

    }
}
