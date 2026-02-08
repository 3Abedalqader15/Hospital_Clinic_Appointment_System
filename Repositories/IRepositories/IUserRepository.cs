using Hospital_Clinic_Appointment_System.Entities;

namespace Hospital_Clinic_Appointment_System.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<User> GetUserByEmailAsync(string email); 

        Task<User> GetUserByNameAsync(string name);

        Task<User> GetUserWithRolesByIdAsync(int id);

        Task<User> UpdatePhoneNumperOfUser (int userId, string newPhoneNumber);

        Task<User> UpdatePasswordOfUser (int userId, string newPassword);

        Task<User> UpdateEmailOfUser (int userId, string newEmail);

        Task<User> UpdateNameOfUser(int userId, string newName);



    }
}
