using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class Doctor : IAuditableEntity
    {
        [Key]
        public int Id { set; get; }

        //[ForeignKey("user")]
        public int User_Id { set; get; } 

        public string Name { set; get; } = string.Empty;
        public string Specialization { set; get; } = string.Empty;

        public string LicenseNumber { set; get; } = string.Empty;

        public int ExperienceYears { set; get; }

        public string Bio {set; get; } = string.Empty;


        public string? profilePictureUrl { set; get; }

        public bool isActive { set; get; }

        public DateTime CreatedAt { set; get; }= DateTime.UtcNow;

        public DateTime? UpdatedAt { set; get; } = DateTime.UtcNow;


        // Relationships
        public User user { get; set; } = new User();
        public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();









    }
}
