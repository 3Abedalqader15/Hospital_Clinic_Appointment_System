
using System.ComponentModel.DataAnnotations;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class User
    {
        [Key]
        public int Id { set; get; }

        public string Name { set; get; }

        public string Email { set; get; }

        public string Password { set; get; }

        public string Phone_Numper { set; get; }

        public DateTime BirthDay { set; get; }

        public bool IsEmailConfirmed { set; get; }

        public bool isActive { set; get; }

        public DateTime Created_At { set; get; }

        public DateTime Updated_At { set; get; }

        // Relationships
        public ICollection<UserRole> UserRoles { get; set; }

        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }

    }
}