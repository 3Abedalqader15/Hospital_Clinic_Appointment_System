namespace Hospital_Clinic_Appointment_System.Entities
{
    public class AuditLog : IEntity , IAuditableEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; } 
        public string EntityName { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string Action { get; set; } = string.Empty; // "Created", "Updated", "Deleted"
        public string? OldValues { get; set; }  // JSON
        public string? NewValues { get; set; } // JSON 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public User? User { get; set; }
    }
}
