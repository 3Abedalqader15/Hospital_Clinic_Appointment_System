using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Clinic_Appointment_System.Entities
{
   public class TimeSlot : IEntity
    {
  
       [Key]
       public int Id { get; set; }

       //[Required]
       //[ForeignKey("doctor")]
       public int DoctorId { get; set; }
       public string DayOfWeek { get; set; } 
       public TimeSpan StartTime { get; set; }
       public TimeSpan EndTime { get; set; }
       public int SlotDuration { get; set; } // in minutes

        public bool IsActive { get; set; } = true;

       // Relationships
       public Doctor doctor { get; set; }

  }

}

