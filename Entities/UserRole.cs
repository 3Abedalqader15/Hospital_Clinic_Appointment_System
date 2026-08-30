namespace Hospital_Clinic_Appointment_System.Entities
{
    public class UserRole
    {
        public int User_Id { get; set; }

        // EF Core navigation property - loaded via Include()
        public User user { get; set; } = null!;

        public int Role_Id { get; set; }

        // EF Core navigation property - loaded via Include()
        public Role role { get; set; } = null!;
    }
}








