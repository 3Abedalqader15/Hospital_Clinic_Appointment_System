using Hospital_Clinic_Appointment_System.Entities;

namespace Hospital_Clinic_Appointment_System.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user, List<string> roles);
         int? ValidateToken(string token);



    }
}
