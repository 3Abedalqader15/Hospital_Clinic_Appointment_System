using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : AuthenticatedPageModel
    {
        public UsersModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public List<UserDto> Users { get; private set; } = new();

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

            var result = await ApiClient.PutAsync($"/api/User/{UpdateUserId}", UpdateUser);
            StatusMessage = result.Success ? "User updated successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to update user.";

            await LoadUsersAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {

            var result = await ApiClient.DeleteAsync($"/api/User/{DeleteUserId}");
            StatusMessage = result.Success ? "User deleted successfully." : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Unable to delete user.";

            await LoadUsersAsync();
            return Page();
        }

        private async Task LoadUsersAsync()
        {
            var result = await ApiClient.GetAsync<List<UserDto>>("/api/User/All");
            if (result.Success && result.Data != null)
            {
                Users = result.Data;
            }
            else
            {
                ErrorMessage = result.Error ?? "Unable to load users.";
            }
        }
    }
}
