using System.Security.Claims;
using System.Text.Json;
using Hospital_Clinic_Appointment_System.Helpers;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital_Clinic_Appointment_System.Pages.Account
{
    public class LoginModel : PageModel
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
        private readonly IApiClient apiClient;

        public LoginModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        [BindProperty]
        public LoginDto Login { get; set; } = new();

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

            var result = await apiClient.PostAsync<LoginResponseDto>("/api/Auth/Login", Login);
            if (!result.Success || result.Data == null || string.IsNullOrWhiteSpace(result.Data.Token))
            {
                ErrorMessage = GetErrorMessage(result);
                return Page();
            }

            await SaveSessionAsync(result.Data);
            return RedirectToDashboard(result.Data.User);
        }

        private async Task SaveSessionAsync(LoginResponseDto response)
        {
            if (!string.IsNullOrWhiteSpace(response.Token))
            {
                HttpContext.Session.SetString(SessionKeys.AuthToken, response.Token);
            }

            if (response.User != null)
            {
                HttpContext.Session.SetObject(SessionKeys.UserInfo, response.User);

                // Create Native Cookie Authentication
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, response.User.Name ?? response.User.Email ?? "User"),
                    new Claim(ClaimTypes.NameIdentifier, response.User.Id.ToString()),
                    new Claim(ClaimTypes.Email, response.User.Email ?? "")
                };

                if (response.User.Roles != null)
                {
                    foreach (var role in response.User.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
        }

        private IActionResult RedirectToDashboard(UserInfoDto? userInfo)
        {
            var roles = userInfo?.Roles ?? [];
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

        private static string GetErrorMessage(ApiResult<LoginResponseDto> result)
        {
            if (!string.IsNullOrWhiteSpace(result.Data?.Message))
            {
                return result.Data.Message;
            }

            if (string.IsNullOrWhiteSpace(result.Error))
            {
                return "Login failed. Please try again.";
            }

            try
            {
                var payload = JsonSerializer.Deserialize<LoginResponseDto>(result.Error, JsonOptions);

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
