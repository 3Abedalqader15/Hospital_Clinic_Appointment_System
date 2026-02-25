using Hospital_Clinic_Appointment_System.Entities;

namespace Hospital_Clinic_Appointment_System.Repositories.IRepositories
{
    public interface IAuthRepository
    {
     
        // register a new user 
        Task<User> RegisterAsync(User user, string password );

        // login 
        Task<User?> LoginAsync(string email, string password);

        // get user with roles by user id
        Task<User?> GetUserWithRolesAsync(int userId);

        // check if email already exists
        Task<bool> EmailExistsAsync(string email);

        // verify password
        bool VerifyPassword(string password, string hashedPassword);

        // hash password
        string HashPassword(string password);

        // change password
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }
}