namespace Hospital_Clinic_Appointment_System.Entities
{
    public class Notification : IAuditableEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        /// <summary>AppointmentReminder, AppointmentConfirmation, Cancellation</summary>
        public string Type { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // EF Core navigation property - loaded via Include()
        public User User { get; set; } = null!;
    }
}
