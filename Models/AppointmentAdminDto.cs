namespace Hospital_Clinic_Appointment_System.Models
{
    public class AppointmentAdminDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool ReminderSent { get; set; }
    }
}
