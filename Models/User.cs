namespace Hospital_Clinic_Appointment_System.Model
{
    public class User
    {
        public int  Id {set; get;  }

        public string Name { set; get; }

        public string Email { set; get; }

        public string Password { set; get; }

        public string Phone_Numper { set; get; } 

        public DateTime BirthDay { set; get; }

        public bool IsEmailConfirmed { set; get; }

        public bool isActive { set; get; }

        public DateTime Created_At { set; get; }

        public DateTime Updated_At { set; get; }



    }
}
