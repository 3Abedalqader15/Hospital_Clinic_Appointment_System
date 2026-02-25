using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^07[0-9]{8}$",ErrorMessage = "Phone number must start with 07 and be 10 digits")]
        public string Phone_Number { get; set; }


        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [RegularExpression(@"^(Patient|Doctor|Admin)$", ErrorMessage = "Role must be either Patient, Doctor, or Admin")]
        public string Role { get; set; }





    }
}
