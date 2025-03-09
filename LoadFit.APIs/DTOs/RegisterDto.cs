using System.ComponentModel.DataAnnotations;

namespace LoadFit.APIs.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Role { get; set; }  // "Driver" or "User"
        public string? LicenseNumber { get; set; }  // Only required for drivers
        public RegisterVehicleDto? Vehicle { get; set; }  // Only required for drivers


    }
}
