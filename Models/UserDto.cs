using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^07[3-9][0-9]{8}$", ErrorMessage = "Invalid phone number")]
        public string? Phone_Number { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}