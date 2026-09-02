using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Hospital_Clinic_Appointment_System.Pages.Doctor
{
    [Authorize(Roles = "Doctor")]
    public class IndexModel : AuthenticatedPageModel
    {
        public IndexModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public DoctorDetailsDto? Doctor { get; private set; }
        public List<AppointmentShortDto> Appointments { get; private set; } = new();
        public string? ErrorMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {

            var doctorResult = await ApiClient.GetAsync<DoctorDetailsDto>("/api/Doctor/Me");
            if (!doctorResult.Success || doctorResult.Data == null)
            {
                ErrorMessage = doctorResult.Error ?? "Unable to load doctor profile.";
                return Page();
            }

            Doctor = doctorResult.Data;
            var appointmentsResult = await ApiClient.GetAsync<DoctorAppointmentShortDto>($"/api/Doctor/{Doctor.Id}/Appointments");
            if (appointmentsResult.Success && appointmentsResult.Data?.Appointments != null)
            {
                Appointments = appointmentsResult.Data.Appointments
                    .OrderBy(a => a.AppointmentDate)
                    .Take(10)
                    .ToList();
            }
            else if (!appointmentsResult.Success)
            {
                ErrorMessage = appointmentsResult.Error ?? "Unable to load appointments.";
            }

            return Page();
        }
    }
}
