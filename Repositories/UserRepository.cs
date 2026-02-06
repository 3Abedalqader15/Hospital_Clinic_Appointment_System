using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Clinic_Appointment_System.Repositories
{
    public class UserRepository : GenericReository<User>, IUserRepository
    {

        private readonly DBContext context;
        public UserRepository(DBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> GetUserByNameAsync(string name)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Name == name);
        }
        public async Task<User> GetUserWithRolesByIdAsync(int id)
        {
            return await context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdatePhoneNumperOfUser(int userId, string newPhoneNumber)
        {
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Phone_Number = newPhoneNumber;
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            return user;  
        }

        public async Task<User> UpdatePasswordOfUser(int userId, string newPassword)
        {
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Password = newPassword;
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> UpdateEmailOfUser(int userId, string newEmail)
        {
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Email = newEmail;
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            return user;
            }
        public async Task<User> UpdateNameOfUser(int userId, string newName)
        {
            var user = await context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Name = newName;
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            return user;
        }


    }
}
