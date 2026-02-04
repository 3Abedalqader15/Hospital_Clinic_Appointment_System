
using Hospital_Clinic_Appointment_System.Entities;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class PatientShortDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }
        public string MedicalHistory { get; set; }

        public string EmergencyNumber { get; set; }
        public bool IsActive { get; set; } = true;


        public ICollection<AppointmentShortDto>  Appointments { get; set; }

        public UserDto? user { get; set; }
    }
}