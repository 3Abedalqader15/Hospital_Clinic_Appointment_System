using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Pages.Admin;

[Authorize(Roles = "Admin")]
public class UsersModel(IApiClient apiClient, IUserRepository userRepository) : AuthenticatedPageModel(apiClient)
{
    public List<UserDto> Users { get; private set; } = [];

        [BindProperty]
        public UpdateUserDto UpdateUser { get; set; } = new();

        [BindProperty]
        public int UpdateUserId { get; set; }

        [BindProperty]
        public int DeleteUserId { get; set; }

        public string? ErrorMessage { get; private set; }
        public string? StatusMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {

            await LoadUsersAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please review the user details and try again.";
                await LoadUsersAsync();
                return Page();
            }

            try
            {
                var user = await userRepository.GetByIdAsync(UpdateUserId);
                if (user != null)
                {
                    user.Name = UpdateUser.Name!;
                    var existingUser = await userRepository.GetUserByEmailAsync(UpdateUser.Email!);
                    if (existingUser != null && existingUser.Id != UpdateUserId)
                    {
                        ErrorMessage = "Email already exists.";
                    }
                    else
                    {
                        user.Email = UpdateUser.Email!;
                        user.Phone_Number = UpdateUser.Phone_Number!;
                        userRepository.Update(user);
                        await userRepository.SaveChangesAsync();
                        StatusMessage = "User updated successfully.";
                    }
                }
                else
                {
                    ErrorMessage = "Unable to find user.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Unable to update user: " + ex.Message;
            }

            await LoadUsersAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            try
            {
                var user = await userRepository.GetByIdAsync(DeleteUserId);
                if (user != null)
                {
                    userRepository.Delete(user);
                    await userRepository.SaveChangesAsync();
                    StatusMessage = "User deleted successfully.";
                }
                else
                {
                    ErrorMessage = "Unable to find user.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Unable to delete user: " + ex.Message;
            }

            await LoadUsersAsync();
            return Page();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var allUsers = await userRepository.GetAllWithIncludesAsync();
                Users = [.. allUsers.Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Phone_Number = u.Phone_Number
                })];
            }
            catch (Exception ex)
            {
                ErrorMessage = "Unable to load users: " + ex.Message;
            }
        }
    }
