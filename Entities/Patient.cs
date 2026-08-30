using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class Patient : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int User_Id { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }

        public string? MedicalHistory { get; set; }

        public string? EmergencyNumber { get; set; }

        public bool IsActive { get; set; } = true;

        // EF Core navigation property - nullable because not always loaded
        public User? user { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}






