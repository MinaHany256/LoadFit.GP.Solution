using System.ComponentModel.DataAnnotations;

namespace LoadFit.APIs.DTOs
{
    public class AddressDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PickupLocation { get; set; }
        [Required]
        public string DestinationLocation { get; set; }
    }
}