namespace Hospital_Clinic_Appointment_System.Entities
{
    public interface IAuditableEntity : IEntity
    {
        DateTime CreatedAt { get; set; } // Timestamp for when the entity was created
        DateTime? UpdatedAt { get; set; } // Timestamp for when the entity was last updated (nullable for new entities)


    }
}
