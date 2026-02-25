namespace Hospital_Clinic_Appointment_System.Models
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? Token { get; set; }
        public string? TokenType { get; set; } = "Bearer";
        public DateTime? ExpiresAt { get; set; }
        public UserInfoDto? User { get; set; }
    }

    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public List<string> Roles { get; set; }
    }
}