using Hospital_Clinic_Appointment_System.Helpers;
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

        public IActionResult OnPost()
        {
            HttpContext.Session.Remove(SessionKeys.AuthToken);
            HttpContext.Session.Remove(SessionKeys.UserInfo);
            return RedirectToPage("/Account/Login");
        }
    }
}
