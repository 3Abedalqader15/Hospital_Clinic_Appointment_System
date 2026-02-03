
using System.Collections.Generic;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class DoctorAppointmentShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Specialization { get; set; }
        public bool isActive { get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }
        public List<AppointmentShortDto> Appointments { get; set; } = new();
    }
}