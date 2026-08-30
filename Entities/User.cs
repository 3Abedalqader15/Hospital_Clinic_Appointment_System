
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class User : IAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public string Name { set; get; } = string.Empty;

        public string Email { set; get; } = string.Empty;

        public string Password { set; get; } = string.Empty;

        public string Phone_Number { set; get; } = string.Empty;

        public DateTime BirthDay { set; get; }

        public bool IsEmailConfirmed { set; get; }

        public bool isActive { set; get; }

        public DateTime CreatedAt { set; get; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { set; get; } = DateTime.UtcNow;

        // Relationships
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        // EF Core navigation properties - nullable because not always loaded
        public Doctor? Doctor { get; set; }

        public Patient? Patient { get; set; }
    }
}