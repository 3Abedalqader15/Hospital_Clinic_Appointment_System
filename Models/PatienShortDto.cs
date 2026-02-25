using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class PatientShortDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^07[3-9][0-9]{8}$",ErrorMessage = "Phone number must start with 07 and be 10 digits")]
        public string? Phone_Number { get; set; }

        [StringLength(1000, ErrorMessage = "Medical history is too long")]
        public string? MedicalHistory { get; set; }

        [Required(ErrorMessage = "Emergency number is required")]
        [RegularExpression(@"^07[3-9][0-9]{8}$", ErrorMessage = "Invalid emergency number")]
        public string? EmergencyNumber { get; set; }

        public List<AppointmentShortDto>? Appointments { get; set; }
    }
}