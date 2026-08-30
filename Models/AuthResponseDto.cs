namespace Hospital_Clinic_Appointment_System.Models
{
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}