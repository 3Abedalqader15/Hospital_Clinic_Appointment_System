namespace Hospital_Clinic_Appointment_System.Model
{
    public class Doctor
    {
        public int Id { set; get; }

        public int User_Id { set; get; }

        public string Specialization { set; get; }

        public string LicenseNumber { set; get; }

        public int ExperienceYears { set; get; }

        public string Bio {set; get; }

        public string profilePictureUrl { set; get; }

        public bool isActive { set; get; }

        public DateTime Created_At { set; get; }












    }
}
