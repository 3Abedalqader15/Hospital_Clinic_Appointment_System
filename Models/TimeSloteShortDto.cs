namespace Hospital_Clinic_Appointment_System.Models
{
    public class TimeSloteShortDto
    {
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotDuration { get; set; } // in minutes







    }
}
