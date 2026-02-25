namespace Hospital_Clinic_Appointment_System.Models
{
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}