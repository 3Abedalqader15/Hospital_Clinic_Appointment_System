using Hospital_Clinic_Appointment_System.Models;
using Hospital_Clinic_Appointment_System.Pages.Shared;
using Hospital_Clinic_Appointment_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Clinic_Appointment_System.Pages.Doctor
{
    public class AppointmentsModel : AuthenticatedPageModel
    {
        public AppointmentsModel(IApiClient apiClient) : base(apiClient)
        {
        }

        public List<AppointmentShortDto> Appointments { get; private set; } = new();
        public string? ErrorMessage { get; private set; }
        public string? StatusMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var guard = RequireAuthentication("Doctor");
            if (guard != null)
            {
                return guard;
            }

            await LoadAppointmentsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCancelAsync(int appointmentId)
        {
            return await RunActionAsync($"/api/Appointment/{appointmentId}/Cancel", "Appointment cancelled.");
        }

        public async Task<IActionResult> OnPostCompleteAsync(int appointmentId, string? notes)
        {
            var result = await ApiClient.PostAsync($"/api/Appointment/{appointmentId}/Complete", notes ?? string.Empty);
            return await HandleActionResultAsync(result, "Appointment completed.");
        }

        public async Task<IActionResult> OnPostNoShowAsync(int appointmentId)
        {
            return await RunActionAsync($"/api/Appointment/{appointmentId}/NoShow", "Appointment marked as no-show.");
        }

        public async Task<IActionResult> OnPostRescheduleAsync(int appointmentId, DateTime newAppointmentDate)
        {
            var guard = RequireAuthentication("Doctor");
            if (guard != null)
            {
                return guard;
            }

            var payload = new RescheduleAppointmentDto { NewAppointmentDate = newAppointmentDate };
            var result = await ApiClient.PostAsync($"/api/Appointment/{appointmentId}/Reschedule", payload);
            return await HandleActionResultAsync(result, "Appointment rescheduled.");
        }

        private async Task<IActionResult> RunActionAsync(string url, string successMessage)
        {
            var guard = RequireAuthentication("Doctor");
            if (guard != null)
            {
                return guard;
            }

            var result = await ApiClient.PostAsync(url, new { });
            return await HandleActionResultAsync(result, successMessage);
        }

        private async Task<IActionResult> HandleActionResultAsync(ApiResult result, string successMessage)
        {
            StatusMessage = result.Success ? successMessage : null;
            ErrorMessage = result.Success ? null : result.Error ?? "Action failed.";
            await LoadAppointmentsAsync();
            return Page();
        }

        private async Task LoadAppointmentsAsync()
        {
            var doctorResult = await ApiClient.GetAsync<DoctorDetailsDto>("/api/Doctor/Me");
            if (!doctorResult.Success || doctorResult.Data == null)
            {
                ErrorMessage = doctorResult.Error ?? "Unable to load doctor profile.";
                return;
            }

            var appointmentsResult = await ApiClient.GetAsync<DoctorAppointmentShortDto>($"/api/Doctor/{doctorResult.Data.Id}/Appointments");
            if (appointmentsResult.Success && appointmentsResult.Data?.Appointments != null)
            {
                Appointments = appointmentsResult.Data.Appointments.OrderBy(a => a.AppointmentDate).ToList();
            }
            else
            {
                ErrorMessage = appointmentsResult.Error ?? "Unable to load appointments.";
            }
        }
    }
}
