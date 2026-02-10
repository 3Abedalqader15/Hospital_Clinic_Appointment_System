using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class CreateAppointmentDto
    {
        [Required(ErrorMessage = "Doctor ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid doctor ID")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Patient ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid patient ID")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Appointment date and time is required")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }
        [Required(ErrorMessage = "Reason for appointment is required")]
        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
        public string Reason { get; set; }
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string Status { get; set; } // Scheduled .. Completed .. Cancelled .. No-Show 
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string Notes { get; set; } 
        
    }
}
