using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class updateDoctorDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid user ID")]
        public int? User_Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string? Name { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Specialization must be between 3 and 100 characters")]
        public string? Specialization { get; set; }

        [RegularExpression(@"^DOC-\d{3,6}$", ErrorMessage = "License number must be in format DOC-XXX")]
        public string? LicenseNumber { get; set; }

        [Range(0, 50, ErrorMessage = "Experience years must be between 0 and 50")]
        public int? ExperienceYears { get; set; }

        [StringLength(500, ErrorMessage = "Bio must not exceed 500 characters")]
        public string? Bio { get; set; }

        [Url(ErrorMessage = "Invalid profile picture URL")]
        public string? profilePictureUrl { get; set; }

        public bool? isActive { get; set; }
    }
}
