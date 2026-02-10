using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class CreateDoctorDto
    {


        [Required(ErrorMessage = "User ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid user ID")]

        public int User_Id { set; get; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]

        public string Specialization { set; get; }

        [Required(ErrorMessage = "License number is required")]
        [RegularExpression(@"^DOC-\d{3,6}$", ErrorMessage = "License number must be in format DOC-XXX (e.g., DOC-001)")] 
        [StringLength(50, ErrorMessage = "License number is too long")]
        public string LicenseNumber { set; get; }

        [Required(ErrorMessage = "Experience years is required")]
        [Range(0, 50, ErrorMessage = "Experience years must be between 0 and 50")]
        public int ExperienceYears { set; get; }
       [StringLength(500, ErrorMessage = "Bio cannot exceed 500 characters")]
        public string? Bio { set; get; }
        [Url(ErrorMessage = "Invalid profile picture URL")]
        public string? profilePictureUrl { set; get; }

        [Required(ErrorMessage = "Active status is required")]
        public bool isActive { set; get; }




    }
}
