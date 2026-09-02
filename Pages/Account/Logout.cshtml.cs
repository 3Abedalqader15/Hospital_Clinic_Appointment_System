using Hospital_Clinic_Appointment_System.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hospital_Clinic_Appointment_System.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("/Account/Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            HttpContext.Session.Remove(SessionKeys.AuthToken);
            HttpContext.Session.Remove(SessionKeys.UserInfo);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
