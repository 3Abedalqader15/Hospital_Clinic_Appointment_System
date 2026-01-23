using Hospital_Clinic_Appointment_System.Model;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } 
        public string Status { get; set; } // Scheduled .. Completed .. Cancelled
        public string Notes { get; set; }
        public bool ReminderSent { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relationship
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }

}
