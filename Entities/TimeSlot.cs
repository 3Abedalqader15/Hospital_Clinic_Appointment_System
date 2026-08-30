using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class TimeSlot : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public string DayOfWeek { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int SlotDuration { get; set; } // in minutes

        public bool IsActive { get; set; } = true;

        // EF Core navigation property - loaded via Include()
        public Doctor doctor { get; set; } = null!;
    }
}
