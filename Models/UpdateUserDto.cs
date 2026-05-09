using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class UpdateUserDto
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        [RegularExpression(@"^[\u0621-\u064Aa-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email is too long")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^07[3-9][0-9]{7}$", ErrorMessage = "Phone number must start with 07 and be 10 digits")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]
        public string? Phone_Number { get; set; }
    }
}
