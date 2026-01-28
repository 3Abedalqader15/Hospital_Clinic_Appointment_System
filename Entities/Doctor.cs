using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class Doctor
    {
        [Key]
        public int Id { set; get; }

        //[ForeignKey("user")]
        public int User_Id { set; get; }

        public int Name { set; get; }

        public string Specialization { set; get; }

        public string LicenseNumber { set; get; }

        public int ExperienceYears { set; get; }

        public string Bio {set; get; }

        public string profilePictureUrl { set; get; }

        public bool isActive { set; get; }

        public DateTime Created_At { set; get; }


        // Relationships
        public User user { get; set; }
        public ICollection<TimeSlot> TimeSlots { get; set; }
        public ICollection<Appointment> Appointments { get; set; }









    }
}
