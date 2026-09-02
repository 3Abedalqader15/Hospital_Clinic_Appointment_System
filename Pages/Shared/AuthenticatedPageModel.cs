using Hospital_Clinic_Appointment_System.Services;
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
    }
}
