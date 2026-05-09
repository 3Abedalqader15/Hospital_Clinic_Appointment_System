using System.Text.Json;
using Hospital_Clinic_Appointment_System.Helpers;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital_Clinic_Appointment_System.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IApiClient apiClient;

        public RegisterModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        [BindProperty]
        public RegisterDto Register { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            var token = HttpContext.Session.GetString(SessionKeys.AuthToken);
            if (!string.IsNullOrWhiteSpace(token))
            {
                var userInfo = HttpContext.Session.GetObject<UserInfoDto>(SessionKeys.UserInfo);
                return RedirectToDashboard(userInfo);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await apiClient.PostAsync<LoginResponseDto>("/api/Auth/Register", Register);
            if (!result.Success || result.Data == null || string.IsNullOrWhiteSpace(result.Data.Token))
            {
                ErrorMessage = GetErrorMessage(result);
                return Page();
            }

            SaveSession(result.Data);
            return RedirectToDashboard(result.Data.User);
        }

        private void SaveSession(LoginResponseDto response)
        {
            if (!string.IsNullOrWhiteSpace(response.Token))
            {
                HttpContext.Session.SetString(SessionKeys.AuthToken, response.Token);
            }

            if (response.User != null)
            {
                HttpContext.Session.SetObject(SessionKeys.UserInfo, response.User);
            }
        }

        private IActionResult RedirectToDashboard(UserInfoDto? userInfo)
        {
            var roles = userInfo?.Roles ?? new List<string>();
            if (roles.Contains("Admin", StringComparer.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Admin/Index");
            }

            if (roles.Contains("Doctor", StringComparer.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Doctor/Index");
            }

            if (roles.Contains("Patient", StringComparer.OrdinalIgnoreCase))
            {
                return RedirectToPage("/Patient/Index");
            }

            return RedirectToPage("/Index");
        }

        private string GetErrorMessage(ApiResult<LoginResponseDto> result)
        {
            if (!string.IsNullOrWhiteSpace(result.Data?.Message))
            {
                return result.Data.Message;
            }

            if (string.IsNullOrWhiteSpace(result.Error))
            {
                return "Registration failed. Please try again.";
            }

            try
            {
                var payload = JsonSerializer.Deserialize<LoginResponseDto>(result.Error, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (!string.IsNullOrWhiteSpace(payload?.Message))
                {
                    return payload.Message;
                }
            }
            catch (JsonException)
            {
                // ignore parsing errors
            }

            return result.Error;
        }
    }
}
