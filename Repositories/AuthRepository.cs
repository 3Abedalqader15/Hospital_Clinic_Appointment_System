using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DBContext context;

        public AuthRepository(DBContext context)
        {
            this.context = context;
        }
        // register a new user 
        public async Task<User> RegisterAsync(User user, string password )
        {
            user.Password = HashPassword(password);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            
            context.Users.Add(user);
            
            await context.SaveChangesAsync();
            return user;


        }

        // login 
        public async Task<User?> LoginAsync(string email, string password) 
        {
            var user = await context.Users.Include(u => u.UserRoles)
                                .ThenInclude(ur => ur.role)
                                .FirstOrDefaultAsync(u => u.Email == email);
            if(user == null) 
            {
                return null;
            }
            if(!VerifyPassword(password, user.Password) || !user.isActive) 
            {
                return null;
            }
            return user;


        }

        // get user with roles by user id
        public  async Task<User?> GetUserWithRolesAsync(int userId)
        {
            return await context.Users.Include(u => u.UserRoles)
                                .ThenInclude(ur => ur.role)
                                .FirstOrDefaultAsync(u => u.Id == userId);

        }

        // check if email already exists
        public async Task<bool> EmailExistsAsync(string email) 
        {
            return await context.Users.AnyAsync(u => u.Email == email);

        }

        // verify password
        public  bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        // hash password
        public  string HashPassword(string password) 
        {
            return BCrypt.Net.BCrypt.HashPassword(password);

        }

        // change password
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword) 
        {
            var user = await context.Users.FindAsync(userId);

            if (user == null || !VerifyPassword(currentPassword, user.Password))
            {
                return false; // User not found or current password is incorrect
            }

            user.Password = HashPassword(newPassword);

            user.UpdatedAt = DateTime.UtcNow;

            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true; 


        }





    }
}
