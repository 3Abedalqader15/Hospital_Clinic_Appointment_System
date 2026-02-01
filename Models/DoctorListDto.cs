using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class DoctorListDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(13)]
        public string Phone_Number { get; set; }

        [Required]
        [MaxLength(50)]
        public string Specialization { get; set; }


        [Required]
        public int ExperienceYears { get; set; }

        public bool isActive { get; set; }


        public User user { get;  set; }

    }
}
