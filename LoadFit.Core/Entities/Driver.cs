using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Entities
{
    public class Driver : BaseEntity
    {
        public Driver()
        {
            
        }

        public Driver(string userId, string licenseNumber, Vehicle vehicle)
        {
            UserId = userId;
            LicenseNumber = licenseNumber;
            Vehicle = vehicle;
        }


        [Required]
        public string UserId { get; set; }

        [Required]
        public string LicenseNumber { get; set; }

        
        public Vehicle Vehicle { get; set; }
    }
}
