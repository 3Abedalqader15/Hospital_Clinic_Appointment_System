using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class CreatePatientDto
    {
        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid user ID")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Medical history must not exceed 1000 characters")]
        public string MedicalHistory { get; set; }

        [Required(ErrorMessage = "Emergency number is required")]
        [Phone(ErrorMessage = "Invalid emergency number")]
        [RegularExpression(@"^07[3-9][0-9]{8}$", ErrorMessage = "Emergency number must start with 07 and contain 10 digits")] 
        public string EmergencyNumber { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
