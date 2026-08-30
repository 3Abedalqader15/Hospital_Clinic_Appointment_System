using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class DoctorDetailsDto
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public int User_Id { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { set; get; } = string.Empty;

        [Required]
        [MaxLength(13)]
        public string Phone_Number { set; get; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Specialization { set; get; } = string.Empty;

        [MaxLength(50)]
        public string LicenseNumber { set; get; } = string.Empty;

        [Required]
        public int ExperienceYears { set; get; }

        [MaxLength(150)]
        public string? Bio { set; get; }

        public string? profilePictureUrl { set; get; }

        [Required]
        public bool IsActive { get; set; }

        public ICollection<TimeSloteShortDto>? TimeSlots { get; set; }
    }
}
