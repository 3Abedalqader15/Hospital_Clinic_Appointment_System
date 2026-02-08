namespace Hospital_Clinic_Appointment_System.Models
{
    public class CreateAppointmentDto
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } // Scheduled .. Completed .. Cancelled .. No-Show
        public string Notes { get; set; } // Post-appointment notes
        public bool ReminderSent { get; set; } = false;
    }
}
