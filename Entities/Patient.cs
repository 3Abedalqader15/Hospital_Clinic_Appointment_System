using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("user")]  
        public int User_Id { get; set; }

        public string? Name {  get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }

        public string MedicalHistory { get; set; }

        public string EmergencyNumber { get; set; }
        public bool IsActive { get; set; } = true;

        // Relationships
        public User? user { get; set; }
        public ICollection<Appointment?> Appointments { get; set; }







    }
}
