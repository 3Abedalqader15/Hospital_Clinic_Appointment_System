using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class CreateTimeSlot
    {
        [Required(ErrorMessage = "Doctor ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid doctor ID")]
        public int DoctorId { get; set; }

        [RegularExpression(@"^(Sunday|Monday|Tuesday|Wednesday|Thursday|Saturday)$",
           ErrorMessage = "Day must be a weekday (except Friday)")]
        public string DayOfWeek { get; set; }
        [Required(ErrorMessage = "Start time is required")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }
        [Required(ErrorMessage = "End time is required")]

        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
        [Required(ErrorMessage = "Slot duration is required")]
        [Range(15, 120, ErrorMessage = "Slot duration must be between 15 and 120 minutes")]
        public int SlotDuration { get; set; } // in minutes

        public bool IsActive { get; set; } = true;
    }
}
