using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Name { set; get; }

        [Required]
        [MaxLength(100)]
        public string Email { set; get; }

        [Required]
        [MaxLength(13)]
        public string Phone_Number { set; get; }

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

        [Required]
        public bool IsActive { get; set; }


        public ICollection<TimeSloteShortDto> TimeSlots { get; set; }

        public ICollection<AppointmentShortDto> Appointments { get; set; }


    }
}
