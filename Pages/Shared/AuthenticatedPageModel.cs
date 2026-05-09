using Hospital_Clinic_Appointment_System.Helpers;
using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital_Clinic_Appointment_System.Pages.Shared
{
    public abstract class AuthenticatedPageModel : PageModel
    {
        protected AuthenticatedPageModel(IApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        protected IApiClient ApiClient { get; }
        protected UserInfoDto? CurrentUser { get; private set; }

        protected IActionResult? RequireAuthentication(params string[] roles)
        {
            var token = HttpContext.Session.GetString(SessionKeys.AuthToken);
            if (string.IsNullOrWhiteSpace(token))
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUser = HttpContext.Session.GetObject<UserInfoDto>(SessionKeys.UserInfo);
            if (roles.Length == 0)
            {
                return null;
            }

            var userRoles = CurrentUser?.Roles ?? new List<string>();
            var roleSet = new HashSet<string>(roles, StringComparer.OrdinalIgnoreCase);
            if (!userRoles.Any(role => roleSet.Contains(role)))
            {
                return RedirectToPage("/Account/AccessDenied");
            }

            return null;
        }
    }
}
