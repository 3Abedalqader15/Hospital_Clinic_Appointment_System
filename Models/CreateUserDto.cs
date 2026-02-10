using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class CreateUserDto
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        [RegularExpression(@"^[\u0621-\u064Aa-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email is too long")]

        public string Email { set; get; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
           ErrorMessage = "Password must contain uppercase, lowercase, number, and special character")] // Minimum 8 characters, at least one uppercase letter, one lowercase letter, one number and one special character
        [DataType(DataType.Password)]

        public string Password { set; get; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^07[3-9][0-9]{8}$", ErrorMessage = "Phone number must start with 07 and contain 10 digits")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]
        public string Phone_Number { set; get; }
        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { set; get; }


        public bool IsEmailConfirmed { set; get; }

        [Required(ErrorMessage = "Active status is required")]
        public bool isActive { set; get; }
    }
}
