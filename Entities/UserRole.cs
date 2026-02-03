using Hospital_Clinic_Appointment_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Clinic_Appointment_System.Entities
{
    public class UserRole
    {
        

        //[ForeignKey("user")]
        public int User_Id { get; set; }

        //[ForeignKey("role")]
        public int Role_Id { get; set; }


        // Relationships

        public User user { get; set; }

        public Role role { get; set; }





    }
}
