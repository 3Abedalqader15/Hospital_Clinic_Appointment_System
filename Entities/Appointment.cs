using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class Appointment : IAuditableEntity
    {
        [Key]
        public int Id { get; set; }
        //[Required]
        //[ForeignKey("doctor")]
        public int DoctorId { get; set; }
        public Doctor doctor { get; set; }
        //[Required]
        //[ForeignKey("patient")]
        public int PatientId { get; set; }

        public Patient patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }  
        public string Status { get; set; } // Scheduled .. Completed .. Cancelled .. No-Show
        public string Notes { get; set; } // Post-appointment notes
        public bool ReminderSent { get; set; } = false; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        DateTime IAuditableEntity.CreatedAt
        {
            get => CreatedAt;
            set => CreatedAt = value;
        }

        DateTime? IAuditableEntity.UpdatedAt 
        {
            get => UpdatedAt;
            set => UpdatedAt = value;
        }

      

       
    }

}
