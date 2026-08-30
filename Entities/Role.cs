namespace Hospital_Clinic_Appointment_System.Entities
{
    public class Role : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Relationships
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
