using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class UserRole
    {
        

        //[ForeignKey("user")]
        public int User_Id { get; set; }

        public User user { get; set; }

        //[ForeignKey("role")]
        public int Role_Id { get; set; }

        public Role role { get; set; }
        









    }
}
