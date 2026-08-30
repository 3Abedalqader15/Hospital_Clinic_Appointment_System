using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class Appointment : IAuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public int DoctorId { get; set; }
        // EF Core navigation property - loaded via Include()
        public Doctor doctor { get; set; } = null!;

        public int PatientId { get; set; }
        // EF Core navigation property - loaded via Include()
        public Patient patient { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }

        public string? Reason { get; set; }

        public string Status { get; set; } = "Scheduled"; // Scheduled, Completed, Cancelled, No-Show

        public string? Notes { get; set; } // Post-appointment notes

        public bool ReminderSent { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        DateTime IAuditableEntity.CreatedAt
        {
            get => CreatedAt;
            set => CreatedAt = value;
        }

        DateTime? IAuditableEntity.UpdatedAt
        {
            get => UpdatedAt;
            set => UpdatedAt = value;
        }
    }
}

