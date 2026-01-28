using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class DoctorDto
    {
        [Key]
        public int Id { set; get; }

        [Required]
        public int User_Id { set; get; }

        [Required]
        [MaxLength(50)]
        public int Name { set; get; }

        [Required]
        [MaxLength(50)]
        public string Specialization { set; get; }

        [MaxLength(50)]
        public string LicenseNumber { set; get; }

        [Required]
        public int ExperienceYears { set; get; }

        [MaxLength(150)]
        public string? Bio { set; get; }

        public string? profilePictureUrl { set; get; }


    }
}
