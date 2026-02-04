
using System;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class AppointmentShortDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Status { get; set; }
        public PatientShortDto Patient { get; set; }
    }
}