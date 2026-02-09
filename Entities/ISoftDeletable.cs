namespace Hospital_Clinic_Appointment_System.Entities
{
    public interface ISoftDeletable : IEntity
    {
        bool IsDeleted { get; set; } // Flag to indicate if the entity is soft-deleted
        DateTime? DeletedAt { get; set; }

    }
}
