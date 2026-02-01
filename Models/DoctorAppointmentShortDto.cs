using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class DoctorAppointmentShortDto
    {
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public string PatientName { get; set; } = null!;

        [Required]
        public string Status { get; set; } = null!;
    }

}
