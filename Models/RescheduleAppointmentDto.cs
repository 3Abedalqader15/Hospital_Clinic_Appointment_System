using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Models
{
    public class RescheduleAppointmentDto
    {
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "New appointment date is required")]
        [DataType(DataType.DateTime)]
        public DateTime NewAppointmentDate { get; set; }

        [StringLength(500, ErrorMessage = "Reason must not exceed 500 characters")]
        public string? Reason { get; set; }
    }
}
