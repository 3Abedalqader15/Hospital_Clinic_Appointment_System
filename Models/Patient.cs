using Hospital_Clinic_Appointment_System.Model;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class Patient
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string MedicalHistory { get; set; }

        public string EmergencyNumber { get; set; }
        public bool IsActive { get; set; } = true;

        // Relationships
        public User User { get; set; }
        public ICollection<Appointment> Appointments { get; set; }







    }
}
