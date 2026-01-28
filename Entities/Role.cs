

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class Role
    {
        public int Id { get; set; }

        public string Description { get; set; }

        // Relashionships
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    




}
}
