using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class UpdateTimeSlot
    {
        [RegularExpression(@"^(Sunday|Monday|Tuesday|Wednesday|Thursday|Saturday)$",
            ErrorMessage = "Day must be a weekday (except Friday)")]
        public string DayOfWeek { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [Range(15, 120, ErrorMessage = "Slot duration must be between 15 and 120 minutes")]
        public int SlotDuration { get; set; }

        public bool IsActive { get; set; }
    }
}