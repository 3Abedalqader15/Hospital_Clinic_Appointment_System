namespace Hospital_Clinic_Appointment_System.Models
{
    public class UserDto
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Email { set; get; }

        public string Password { set; get; }

        public string Phone_Number { set; get; }

        public DateTime BirthDay { set; get; }

        public bool IsEmailConfirmed { set; get; }

        public bool isActive { set; get; }

        public DateTime Created_At { set; get; } = DateTime.UtcNow;

        public DateTime Updated_At { set; get; } = DateTime.UtcNow;
    }
}
